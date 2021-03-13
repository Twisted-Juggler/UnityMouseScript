using UnityEngine;

public class Mouse : MonoBehaviour
{
    public float xSensitivity;
    public float ySensitivity;
    float xRotation;
    float yRotation;
    float multiplier = 0.01f;
    float mouseX;
    float mouseY;

    Transform orientation;
    Camera cam;

    void Start()
    {

        cam = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Look();

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.localRotation = Quaternion.Euler(yRotation, 0, 0);
    }

    // Mouse Control
    public void Look()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * xSensitivity * multiplier;
        xRotation -= mouseY * ySensitivity * multiplier;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
