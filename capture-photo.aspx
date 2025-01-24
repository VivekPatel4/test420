<%@ Page Title="" Language="C#" MasterPageFile="~/user/user.Master" AutoEventWireup="true" CodeBehind="capture-photo.aspx.cs" Inherits="Student.user.capture_photo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/@mediapipe/camera_utils/camera_utils.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@mediapipe/control_utils/control_utils.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@mediapipe/drawing_utils/drawing_utils.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@mediapipe/face_mesh/face_mesh.js"></script>

    <script>
        let capturedImage = null;
        let faceMesh = null;

        async function startWebcam() {
            try {
                const video = document.getElementById('video');
                const stream = await navigator.mediaDevices.getUserMedia({ video: true });
                video.srcObject = stream;
            } catch (error) {
                console.error('Error accessing webcam:', error);
                alert('Error accessing webcam. Please ensure your camera is connected and permissions are granted.');
            }
        }

        async function capturePhoto() {
            try {
                const video = document.getElementById('video');
                const canvas = document.getElementById('canvas');
                const context = canvas.getContext('2d');

                canvas.width = video.videoWidth;
                canvas.height = video.videoHeight;
                context.drawImage(video, 0, 0, canvas.width, canvas.height);

                capturedImage = canvas.toDataURL('image/png');
                document.getElementById('capturedImage').src = capturedImage;

                document.getElementById('hiddenImage').value = capturedImage;
                document.getElementById('studentId').value = '<%= Session["sid"] %>';
            } catch (error) {
                console.error('Error capturing photo:', error);
                alert('An error occurred. Please try again.');
            }
        }

        window.onload = async () => {
            await startWebcam();
        };
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />

    <div>
        <h2>Capture Photo for Attendance</h2>
        <video id="video" width="320" height="240" autoplay></video>
        <asp:Button ID="captureButton" runat="server" Text="Capture" CausesValidation="false" OnClientClick="capturePhoto(); return false;" />
        <canvas id="canvas" width="320" height="240" style="display: none;"></canvas>
        <img id="capturedImage" alt="Captured Image" />

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Button ID="attendanceButton" runat="server" Text="Mark Attendance" OnClick="attendance_Click" CausesValidation="false" />
                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                <asp:Label ID="vv" runat="server"></asp:Label>
                <asp:Label ID="vv2" runat="server"></asp:Label>

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="attendanceButton" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <input type="hidden" id="hiddenImage" name="hiddenImage" />
    <input type="hidden" id="studentId" name="studentId" />
</asp:Content>
