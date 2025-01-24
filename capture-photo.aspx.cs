using System;
using System.Data.SqlClient;
using System.IO;
using System.Web.Configuration;
using System.Diagnostics;

namespace Student.user
{
    public partial class capture_photo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["sid"] == null)
                {
                    Response.Redirect("../user/login.aspx");
                }
            }
        }

        protected void attendance_Click(object sender, EventArgs e)
        {
            string capturedImage = Request.Form["hiddenImage"];
            string studentId = Request.Form["studentId"];

            if (!string.IsNullOrEmpty(capturedImage) && !string.IsNullOrEmpty(studentId))
            {
                string cleanedImage = CleanBase64String(RemoveDataUrlPrefix(capturedImage));
                string result = MatchFace(studentId, cleanedImage);
                lblMessage.Text = result;
            }
            else
            {
                lblMessage.Text = "Error: Captured image or student ID is missing.";
            }
        }

        private string MatchFace(string studentId, string capturedImage)
        {
            try
            {
                string connectionString = WebConfigurationManager.ConnectionStrings["cs"].ConnectionString;
                string storedImagePath = "";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT capturedPhoto FROM students WHERE id = @StudentId";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@StudentId", studentId);

                    con.Open();
                    storedImagePath = cmd.ExecuteScalar()?.ToString();
                }

                if (string.IsNullOrEmpty(storedImagePath))
                {
                    return "Student not found.";
                }

                string mappedImagePath = Server.MapPath(storedImagePath);
                string capturedImagePath = SaveBase64Image(capturedImage);

                bool isMatch = CompareFaces(mappedImagePath, capturedImagePath);

                if (isMatch)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string query = "INSERT INTO attendance (stdid, status, date) VALUES (@StudentId, 'Present', GETDATE())";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@StudentId", studentId);

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }

                    return "Attendance marked for student: " + studentId;
                }
                else
                {
                    return "Face mismatch.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        private string SaveBase64Image(string base64Image)
        {
            byte[] imageBytes = Convert.FromBase64String(base64Image);
            string fileName = Guid.NewGuid().ToString() + ".png";
            string path = Server.MapPath($"~/admin/images/{fileName}");
            File.WriteAllBytes(path, imageBytes);
            return path;
        }

        private bool CompareFaces(string storedImagePath, string capturedImagePath)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "New folder",
                    Arguments = $"\"C:\\Users\\jay21\\Desktop\\New folder\\face_recognition.py\" \"{storedImagePath}\" \"{capturedImagePath}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(startInfo))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                    return output.Contains("Faces match");
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private string CleanBase64String(string base64String)
        {
            return base64String.Trim().Replace(" ", "").Replace("\r", "").Replace("\n", "");
        }

        private string RemoveDataUrlPrefix(string base64String)
        {
            if (base64String.StartsWith("data:image"))
            {
                int index = base64String.IndexOf("base64,") + 7;
                return base64String.Substring(index);
            }
            return base64String;
        }
    }
}
