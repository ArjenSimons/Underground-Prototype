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

    [Header("Camera Settings")]
    private float cameraMinZoom = 0f;
    [Range(0, 10)]
    public float cameraMaxZoom = 10f;
    [Range(1,10)]
    public float cameraSensitivity = 1f;

    [Header("Camera Movement Settings")]
    [Range(0f, 1f)]
    public float cameraSpeed = .3f;
    [Range(0f, .99f)]
    public float cameraSpeedDeadZone = .4f;
    [Range(1, 50)]
    public int cameraScreenPercentage = 10;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = this.GetComponent<Camera>();
        cameraTransform = playerCamera.transform;
        standardCameraHeight = cameraTransform.position.y;
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
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        cameraTransform.position += GetMouseDirection();

        if (horizontal > cameraSpeedDeadZone) { cameraTransform.position += Vector3.right * cameraSpeed; }
        else if (horizontal < -cameraSpeedDeadZone) { cameraTransform.position += Vector3.left * cameraSpeed; }

        if (vertical > cameraSpeedDeadZone) { cameraTransform.position += Vector3.forward * cameraSpeed; }
        else if (vertical < -cameraSpeedDeadZone) { cameraTransform.position += Vector3.back * cameraSpeed; }

        if (scrollWheel != 0f)
        {
            float distance = Vector3.Distance(tempTransform, cameraTransform.position);
            //Debug.Log(distance);

            if (distance < cameraMinZoom && scrollWheel < 0f) { return; }
            if (distance > cameraMaxZoom && scrollWheel > 0f) { return; }

            Vector3 nextCameraPos = cameraTransform.forward * cameraSensitivity * scrollWheel;
            cameraTransform.position += nextCameraPos;

            if (cameraTransform.position.y > standardCameraHeight) { cameraTransform.position = new Vector3(cameraTransform.position.x,tempTransform.y,cameraTransform.position.z); }
            //float fov = playerCamera.fieldOfView;
            //fov += -scrollWheel * sensitivity;
            //fov = Mathf.Clamp(fov, minFov, maxFov);
            //playerCamera.fieldOfView = fov;
        }
        else if (cameraTransform.position.y == standardCameraHeight)
        {
            tempTransform = cameraTransform.position;
        }
    }

    private Vector3 GetMouseDirection()
    {

        // some screen percentage
        float widthPercentage = (float)Screen.width * (cameraScreenPercentage/100f);
        float heightPercentage = (float)Screen.height * (cameraScreenPercentage/100f);
        //Debug.Log(widthPercentage + " "+ heightPercentage);

        Vector3 dirTransform = new Vector3(0,0,0);
        Vector2 mousePos = Input.mousePosition;

        // within some percentage to edge of screen
        if (mousePos.x > 0f && mousePos.x < widthPercentage) { dirTransform += Vector3.left * cameraSpeed; }
        if (mousePos.x < Screen.width && mousePos.x > Screen.width - widthPercentage) { dirTransform += Vector3.right * cameraSpeed; }
        if (mousePos.y > 0f && mousePos.y < heightPercentage) { dirTransform += Vector3.back * cameraSpeed; }
        if (mousePos.y < Screen.height && mousePos.y > Screen.height - heightPercentage) { dirTransform += Vector3.forward * cameraSpeed; }

        return dirTransform;
    }
}
