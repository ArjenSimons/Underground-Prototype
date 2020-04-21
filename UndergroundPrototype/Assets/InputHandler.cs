using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class InputHandler : MonoBehaviour
{
    //Building Placement
    public Transform minePrefab;
    private Transform mineBuilding;
    private bool buildingAction;

    Camera mainCam;
    GameObject g;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = this.GetComponent<Camera>();
        g = new GameObject();
        Instantiate(g, transform.position, Quaternion.identity);
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
            Vector3 pos = SelectPositon(Input.mousePosition);
            // instantiate event?

            g.transform.position = pos;
        }

        if (Input.GetKeyUp("b"))
        {
            //If no building action is being done, start placing.
            if (buildingAction == false)
            {
                GameObject buildings = GameObject.Find("Buildings");
                mineBuilding = Instantiate(minePrefab.transform, buildings.transform);
                buildingAction = true;
            }
        }

        if(Input.GetAxis("Fire1") > 0)
        {
            if (buildingAction == true)
            {
                PlaceBuilding placeBuilding = mineBuilding.gameObject.GetComponent<PlaceBuilding>();
                placeBuilding.placingBuilding = false;
                buildingAction = false;
            }
        }
    }

    public Vector3 SelectPositon(Vector2 mousePos)
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
