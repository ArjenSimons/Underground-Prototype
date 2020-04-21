using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStation : MonoBehaviour
{
    [SerializeField] private Transform actualTransform;
    [SerializeField] private GameObject createUnitCanvas;
    [SerializeField] private RectTransform uiPanel;
    [SerializeField] private float yOffset = -10f;
    private LayerMask layerMask;

    private void Start()
    {
        layerMask = 1 << LayerMask.NameToLayer("PlayerBaseStation");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                OpenUnitCreateWindow();
            }
        }
    }

    private void FixedUpdate()
    {
        if (createUnitCanvas.activeSelf)
        {
            Vector3 offsetPos = new Vector3(actualTransform.position.x, actualTransform.position.y + yOffset, actualTransform.position.z);
            //Vector2 canvasPos;
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(offsetPos);

            //RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)createUnitCanvas.transform, screenPoint, null, out canvasPos);

            uiPanel.localPosition = screenPoint;

        }   
    }

    private void OpenUnitCreateWindow()
    {
        createUnitCanvas.gameObject.SetActive(true);
    }
}