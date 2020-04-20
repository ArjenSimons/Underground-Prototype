using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class InputHandler : MonoBehaviour
{

    Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = this.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //SelectPositon();
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetAxis("Fire2") > 0)
        {
            SelectPositon(Input.mousePosition);
            // instantiate event?
        }
    }

    private Vector3 SelectPositon(Vector2 mousePos)
    {
        //Debug.Log(mousePos);
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        //Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.Log(LayerMask.LayerToName(hit.collider.gameObject.layer));
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
            return hit.point;
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.white);
        }
        return new Vector3(0,0,0);
    }
}
