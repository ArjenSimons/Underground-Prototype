using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{

    private Camera playerCamera;
    private Transform cameraTransform;
    private Vector3 tempTransform;
    private float standardCameraHeight;
    [Range(0f,1f)]
    public float cameraSpeed = 1f;
    [Range(0f,.99f)]
    public float cameraSpeedDeadZone = .5f;
    [Range(0, 10)]
    public float cameraMinZoom = 0f;
    [Range(0, 10)]
    public float cameraMaxZoom = 2f;
    [Range(1,10)]
    public float sensitivity = 1f;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = this.GetComponent<Camera>();
        cameraTransform = playerCamera.transform;
        standardCameraHeight = cameraTransform.position.y;
        Debug.Log(standardCameraHeight);
    }

    // Update is called once per frame
    void Update()
    {

        HandleInput();
    }

    // this class handle the input of the camera
    void HandleInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        if (horizontal > cameraSpeedDeadZone) { cameraTransform.position += Vector3.right * cameraSpeed; }
        else if (horizontal < -cameraSpeedDeadZone) { cameraTransform.position += Vector3.left * cameraSpeed; }

        if (vertical > cameraSpeedDeadZone) { cameraTransform.position += Vector3.forward * cameraSpeed; }
        else if (vertical < -cameraSpeedDeadZone) { cameraTransform.position += Vector3.back * cameraSpeed; }

        if (scrollWheel != 0)
        {
            float distance = Vector3.Distance(tempTransform, cameraTransform.position);
            Debug.Log(distance);

            if (distance < cameraMinZoom && scrollWheel < 0f) { return; }
            if (distance > cameraMaxZoom && scrollWheel > 0f) { return; }

            if (cameraTransform.position.y > standardCameraHeight)
            {
                cameraTransform.position = tempTransform;
                return;
            }

            cameraTransform.position += cameraTransform.forward * sensitivity * scrollWheel;
            //float fov = playerCamera.fieldOfView;
            //fov += -scrollWheel * sensitivity;
            //fov = Mathf.Clamp(fov, minFov, maxFov);
            //playerCamera.fieldOfView = fov;
        }
        else if (cameraTransform.position.y == standardCameraHeight)
        {
            Debug.Log("resetting");
            tempTransform = cameraTransform.position;
        }
    }
}
