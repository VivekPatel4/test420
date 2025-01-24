import cv2
import numpy as np
import tkinter as tk
from tkinter import messagebox
from scipy.spatial.distance import cosine


face_cascade = cv2.CascadeClassifier(cv2.data.haarcascades + "haarcascade_frontalface_default.xml")


face_recognizer = cv2.dnn.readNetFromTorch("nn4.small2.v1.t7")

def extract_face(image, faces, index=0):
    """Extract and resize the face region."""
    if len(faces) == 0:
        return None
    (x, y, w, h) = faces[index]
    face_roi = image[y:y+h, x:x+w]
    face_roi = cv2.resize(face_roi, (96, 96))  
    return face_roi

def get_face_embedding(face_image):
    """Get the face embedding using a pre-trained deep learning model."""
   
    blob = cv2.dnn.blobFromImage(
        face_image, 
        scalefactor=1.0 / 255,  
        size=(96, 96),         
        mean=(0, 0, 0),        
        swapRB=True            
    )
    face_recognizer.setInput(blob)
    embedding = face_recognizer.forward()
    return embedding.flatten()

def compare_faces(embedding1, embedding2, threshold=0.6):
    """Compare two faces using cosine similarity."""
    similarity = 1 - cosine(embedding1, embedding2)
    return similarity > threshold

def process_image(image_path):
    """Load and process the reference image."""
    image = cv2.imread(image_path)
    if image is None:
        raise ValueError("Error: Could not load the reference image.")
    gray_image = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
    faces = face_cascade.detectMultiScale(gray_image, scaleFactor=1.1, minNeighbors=5, minSize=(30, 30))
    if len(faces) == 0:
        raise ValueError("No faces found in the reference image.")
    face_roi = extract_face(image, faces)  
    return get_face_embedding(face_roi)

def start_face_comparison(image_path, threshold=0.6):
    """Start the face comparison process."""
    try:
        reference_embedding = process_image(image_path)
    except ValueError as e:
        messagebox.showerror("Error", str(e))
        return

    cap = cv2.VideoCapture(0)
    if not cap.isOpened():
        messagebox.showerror("Error", "Could not open webcam.")
        return

    face_matched = False
    attempt_count = 0
    max_attempts = 100

    while attempt_count < max_attempts:
        ret, frame = cap.read()
        if not ret:
            print("Error: Failed to capture image.")
            break

        gray_frame = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
        faces = face_cascade.detectMultiScale(gray_frame, scaleFactor=1.1, minNeighbors=5, minSize=(30, 30))

        if len(faces) > 0:
            current_face = extract_face(frame, faces)
            current_embedding = get_face_embedding(current_face)
            similarity = 1 - cosine(reference_embedding, current_embedding)

            (x, y, w, h) = faces[0]
            if similarity > threshold:
                cv2.rectangle(frame, (x, y), (x + w, y + h), (0, 255, 0), 2)
                cv2.putText(frame, f"Match: {similarity:.2f}", (x, y - 10), cv2.FONT_HERSHEY_SIMPLEX, 0.9, (0, 255, 0), 2)
                face_matched = True
            else:
                cv2.rectangle(frame, (x, y), (x + w, y + h), (0, 0, 255), 2)
                cv2.putText(frame, f"No Match: {similarity:.2f}", (x, y - 10), cv2.FONT_HERSHEY_SIMPLEX, 0.9, (0, 0, 255), 2)

        cv2.imshow("Live Webcam Feed", frame)
        attempt_count += 1

        if cv2.waitKey(1) & 0xFF == ord('q') or face_matched:
            break

    cap.release()
    cv2.destroyAllWindows()

    if face_matched:
        messagebox.showinfo("Success", "Face match found!")
    else:
        messagebox.showinfo("Failure", f"No face match found after {max_attempts} attempts.")

def on_compare_button_click():
    image_path = entry_image_path.get()
    threshold = float(entry_threshold.get())
    if not image_path:
        messagebox.showerror("Error", "Please provide an image path.")
        return
    start_face_comparison(image_path, threshold)

# GUI setup
root = tk.Tk()
root.title("Face Comparison")

label_image_path = tk.Label(root, text="Enter image path:")
label_image_path.pack(padx=10, pady=5)

entry_image_path = tk.Entry(root, width=50)
entry_image_path.pack(padx=10, pady=5)

label_threshold = tk.Label(root, text="Similarity Threshold (0.0 to 1.0):")
label_threshold.pack(padx=10, pady=5)

entry_threshold = tk.Entry(root, width=10)
entry_threshold.insert(0, "0.6")  # Default threshold
entry_threshold.pack(padx=10, pady=5)

compare_button = tk.Button(root, text="Compare Faces", command=on_compare_button_click)
compare_button.pack(padx=10, pady=20)

root.mainloop()