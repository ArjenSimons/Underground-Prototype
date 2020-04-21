using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class InputHandler : MonoBehaviour
{

    private Camera mainCam;
    private GameObject unitSelector;
    private Vector3 dragStartPos = new Vector3(0,0,0);
    private BoxCollider tempShowBoxCollider;

    private List<GameObject> allUnits = new List<GameObject>();
    public GameObject AllUnits
    {
        //get { return selectedUnits; }
        set { allUnits.Add(value); }
    }
    private List<GameObject> selectedUnits = new List<GameObject>();
    private bool selectionStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = this.GetComponent<Camera>();
        unitSelector = GameObject.Find("UnitSelector"); // prefab?
        //Instantiate(g, transform.position, Quaternion.identity);
        
    }

    void OnDrawGizmos()
    {
        if (tempShowBoxCollider) { Gizmos.DrawCube(tempShowBoxCollider.center, tempShowBoxCollider.size); }
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

            Order(pos);

            unitSelector.transform.position = pos;
        }
        if (Input.GetAxis("Fire1") > 0)
        {
            //Debug.Log("selecting");
            CreateUnitSelection(Input.mousePosition);
        } else
        {
            // mouse was let go, reset
            if (selectionStarted)
            {
                CreateSelection();
            }
            selectionStarted = false;
            // CollectUnits(); store units in list?
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
           // Debug.Log(LayerMask.LayerToName(hit.collider.gameObject.layer));
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
            Debug.Log(hit.point.y);
            return hit.point;
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.white);
            Debug.Log("Failed to hit surface");
        }
        return new Vector3(0,0,0);
    }

    private void CreateUnitSelection(Vector2 mousePos)
    {
        Vector3 dragEndPos = SelectPositon(mousePos);
        if (!selectionStarted)
        {
            dragStartPos = SelectPositon(mousePos); // only reset upon mouse release
           // Debug.Log(dragStartPos);
            selectionStarted = true;
        }
        unitSelector.transform.position = new Vector3(dragEndPos.x, dragStartPos.y, dragEndPos.z);

        BoxCollider bc = unitSelector.GetComponent<BoxCollider>();
        tempShowBoxCollider = bc;

        

        float xDistance = dragStartPos.x - dragEndPos.x;
        float yPos = bc.size.y / 2;
        float zDistance = dragStartPos.z - dragEndPos.z;

        bc.size = new Vector3(Mathf.Abs(xDistance), bc.size.y, Mathf.Abs(zDistance));
        bc.center = new Vector3(xDistance/2, yPos, zDistance/2);

        //Debug.Log(Vector3.Distance(dragStartPos, dragEndPos));
        //Debug.Log(dragStartPos.z - dragEndPos.z);
    }

    private void CreateSelection()
    {
        BoxCollider bc = unitSelector.GetComponent<BoxCollider>();
        selectedUnits.Clear();
        for (int i = 0; i < allUnits.Count; i++)
        {
            if (bc.bounds.Contains(allUnits[i].transform.position))
            {
                allUnits[i].GetComponent<UnitBehavior>().Select();
                selectedUnits.Add(allUnits[i]);
            } else
            {
                allUnits[i].GetComponent<UnitBehavior>().Deselect();
            }
        }
    }

    private void Order(Vector3 pos /*, structureTypeEnum? structure.enemy, structure.build, structure.repair enz.*/ )
    {
        for (int i = 0; i < selectedUnits.Count; i++)
        {
            selectedUnits[i].GetComponent<UnitBehavior>().DoInvoke(new UnitDataEventArgs(this, "Move", pos));
        }
    }
}
