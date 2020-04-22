using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMovement : MonoBehaviour
{

    private Camera playerCamera;
    private Transform cameraTransform;
    private Vector3 tempTransform;
    private float cameraDistance = 0f;
    private float cameraAngle = 0f;
    private float cameraRotation = 0f;
    
    [Header("Camera Settings")]
    [SerializeField]
    private bool enableQERotation = true;
    [SerializeField]
    private bool enableMouse3Rotation = true;
    [SerializeField]
    private bool enableEdgePanning = true;

    [Header("Camera Zoom settings")]
    [SerializeField]
    [Range(0, 50)]
    private float cameraDistanceDefault = 6f;
    [SerializeField]
    [Range(0, 50)]
    private float cameraMinZoom = 0f;
    [Range(0, 50)]
    public float cameraMaxZoom = 50f;
    [Range(1, 10)]
    public float cameraZoomSpeed = 6f;

    [Header("Camera Movement Settings")]
    [Range(0f, 1f)]
    public float cameraSpeed = .3f;
    [Range(0f, .99f)]
    public float cameraSpeedDeadZone = .4f;
    [Range(1, 50)]
    public int cameraScreenPercentage = 10;

    [Header("Camera Rotational Settings")]
    [SerializeField]
    private float cameraAngleMin = 40f;
    [SerializeField]
    private float cameraAngleMax = 65f;
    [SerializeField]
    private float cameraAngleDefault = 60f;
    [SerializeField]
    private float camAngleSensitivity = 1f;
    [SerializeField]
    private float camRotationDelta = 0.5f;
    [SerializeField]
    private float camRotationSensitivity = 2f;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = this.GetComponentInChildren<Camera>();
        cameraTransform = playerCamera.transform;
        cameraDistance = cameraDistanceDefault;
        cameraAngle = cameraAngleDefault;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    // this class handle the input of the camera
    void HandleInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (Input.mouseScrollDelta.y > 0)
        {
            Scroll(-cameraZoomSpeed);
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            Scroll(cameraZoomSpeed);
        }

        if (horizontal > cameraSpeedDeadZone) { Pan(Vector3.right); }
        else if (horizontal < -cameraSpeedDeadZone) { Pan(Vector3.left); }

        if (vertical > cameraSpeedDeadZone) { Pan(Vector3.forward); }
        else if (vertical < -cameraSpeedDeadZone) { Pan(Vector3.back); }


        if (enableMouse3Rotation)
        {
            if (Input.GetMouseButton(2))
            {
                Rotate(Input.GetAxis("Mouse X") * camRotationSensitivity * (1));
                Tilt(Input.GetAxis("Mouse Y") * camAngleSensitivity * (-1));
            }
        }

        if (enableQERotation)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                Rotate(camRotationDelta * (1));
            }

            if (Input.GetKey(KeyCode.E))
            {
                Rotate(camRotationDelta * (-1));
            }
        }

        if (enableEdgePanning)
        {
            if (playerCamera.ScreenToViewportPoint(Input.mousePosition).x > 1)
            {
                Pan(Vector3.right);
            }

            if (playerCamera.ScreenToViewportPoint(Input.mousePosition).x < 0)
            {
                Pan(Vector3.left);
            }

            if (playerCamera.ScreenToViewportPoint(Input.mousePosition).y > 1)
            {
                Pan(Vector3.forward);
            }

            if (playerCamera.ScreenToViewportPoint(Input.mousePosition).y < 0)
            {
                Pan(Vector3.back);
            }
        }

        UpdateCamera();
    }

    private void UpdateCamera()
    {
        Vector3 camPos = new Vector3(0f, Mathf.Sin(cameraAngle * Mathf.PI / 180) * cameraDistance, -(Mathf.Cos(cameraAngle * Mathf.PI / 180) * cameraDistance));
        playerCamera.transform.localPosition = camPos;
        playerCamera.transform.LookAt(transform);
    }

    private void Scroll(float distanceDelta)
    {
        cameraDistance += distanceDelta;
        cameraDistance = Mathf.Clamp(cameraDistance, cameraMinZoom, cameraMaxZoom);
    }

    private void Rotate(float rotationDelta)
    {
        cameraRotation += rotationDelta;
        transform.rotation = Quaternion.Euler(0f, cameraRotation, 0f);
    }

    private void Tilt(float angleDelta)
    {
        cameraAngle += angleDelta;
        cameraAngle = Mathf.Clamp(cameraAngle, cameraAngleMin, cameraAngleMax);
    }

    private void Pan(Vector3 direction)
    {
        transform.Translate(direction*cameraSpeed, Space.Self);
    }
}
