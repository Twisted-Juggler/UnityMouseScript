using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    [SerializeField] float sensX = 100f;
    [SerializeField] float sensY = 100f;

    [SerializeField] Transform cam = null;
    [SerializeField] Transform orientation = null;

    [SerializeField] Vector2 cameraTurnSpeed;

    private float upClampAngle = 60f;
    private float downClampAngle = -90f;
    private float CameraTilt = 10f;
    private float ySmoothRotation;
    private float xSmoothRotation;

    float mouseX;
    float mouseY;

    float multiplier = 0.01f;

    float xRotation;
    float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        CalcRotation();
        SmoothRotation();
        ApplyRotation();
    }

    void CalcRotation()
    {
        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;

        xRotation = Mathf.Clamp(xRotation, -upClampAngle, downClampAngle);
    }

    void SmoothRotation()
    {
        ySmoothRotation = Mathf.Lerp(ySmoothRotation, yRotation, cameraTurnSpeed.y * Time.smoothDeltaTime);
        xSmoothRotation = Mathf.Lerp(xSmoothRotation, xRotation, cameraTurnSpeed.x * Time.smoothDeltaTime);
    }

    void ApplyRotation()
    {
        cam.localRotation = Quaternion.Euler(xSmoothRotation, ySmoothRotation, 0);
        orientation.transform.rotation = Quaternion.Euler(0, ySmoothRotation, 0);
    }
}
