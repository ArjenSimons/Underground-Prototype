using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class InputHandler : MonoBehaviour
{
    [Header("Material for lines of drawn box")]
    public Material mat;

    private Camera mainCam;
    private GameObject unitSelector;
    private Vector3 dragStartPos = new Vector3(0,0,0);
    private Vector3 dragEndPos = new Vector3(1,1,1);
    private BoxCollider tempShowBoxCollider;

    //Building Placement
    public Transform minePrefab;
    private Transform mineBuilding;
    private bool buildingAction;

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
            
            unitSelector.transform.position = pos;
            Order(pos, "Move");
        }
        if (Input.GetAxis("Fire1") > 0)
        {
            RaycastHit rayHit = SelectPositon(Input.mousePosition, true);
            //unitSelector.transform.position = rayHit.point;

            //Debug.Log("selecting");
            if (rayHit.collider.gameObject.layer == 9)
            { //layer 9 is ground
                CreateUnitSelection(Input.mousePosition);
            }else
            {
                Order(rayHit.point, "Action", rayHit.collider.gameObject);
            }
            if (buildingAction == true)
            {
                //Place building
                PlaceBuilding placeBuilding = mineBuilding.gameObject.GetComponent<PlaceBuilding>();
                CheckGround checkGround = mineBuilding.gameObject.GetComponent<CheckGround>();
                //Check if building can be placed
                if (checkGround.CheckSocket() == true && checkGround.CheckType() != BlockType.Regular)
                {
                    placeBuilding.placingBuilding = false;
                    buildingAction = false;
                    checkGround.SetSocket(false);
                }
            }
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

        if (Input.GetKeyUp("b"))
        {
            //If no building action is in progress, enable placing mode
            if (buildingAction == false)
            {
                GameObject buildings = GameObject.Find("Buildings");
                mineBuilding = Instantiate(minePrefab.transform, buildings.transform);
                buildingAction = true;
            }
        }
    }
    
    public Vector3 SelectPositon(Vector2 mousePos)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
            return hit.point;
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.white);
        }
        return new Vector3();
    }

    public RaycastHit SelectPositon(Vector2 mousePos, bool returnHit)
    {
        //Debug.Log(mousePos);
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            return hit;
        }
        return new RaycastHit();
    }

    private void CreateUnitSelection(Vector2 mousePos)
    {
        dragEndPos = new Vector3(SelectPositon(mousePos).x, dragStartPos.y, SelectPositon(mousePos).z);
        if (!selectionStarted)
        {
            dragStartPos = SelectPositon(mousePos); // only reset upon mouse release
           // Debug.Log(dragStartPos);
            selectionStarted = true;
        }
        unitSelector.transform.position = new Vector3(dragEndPos.x, dragStartPos.y, dragEndPos.z);

        BoxCollider bc = unitSelector.GetComponent<BoxCollider>();
        tempShowBoxCollider = bc; // instead draw cube or smth?

        

        float xDistance = dragStartPos.x - dragEndPos.x;
        float yPos = 0;
        float zDistance = dragStartPos.z - dragEndPos.z;

        bc.size = new Vector3(Mathf.Abs(xDistance), bc.size.y, Mathf.Abs(zDistance));
        bc.center = new Vector3(xDistance/2, yPos, zDistance/2);
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

    private void Order(Vector3 pos, string order, GameObject selectedObject = null /*, structureTypeEnum? structure.enemy, structure.build, structure.repair enz.*/ )
    {
        for (int i = 0; i < selectedUnits.Count; i++)
        {
            selectedUnits[i].GetComponent<UnitBehavior>().DoInvoke(new UnitDataEventArgs(this, order, pos, selectedObject));
        }
    }

    void OnPostRender()
    {
        if (!mat)
        {
            Debug.LogError("Please Assign a material on the inspector");
            return;
        }
        GL.PushMatrix();
        mat.SetPass(0);

        GL.Begin(GL.LINES);
        GL.Color(Color.red);
        GL.Vertex(new Vector3(dragStartPos.x,dragStartPos.y,dragEndPos.z));
        GL.Vertex(dragEndPos);
        GL.Vertex(dragEndPos);
        GL.Vertex(new Vector3(dragEndPos.x, dragEndPos.y, dragStartPos.z));
        GL.Vertex(new Vector3(dragEndPos.x, dragEndPos.y, dragStartPos.z));
        GL.Vertex(new Vector3(dragStartPos.x, dragEndPos.y, dragStartPos.z));
        GL.Vertex(new Vector3(dragStartPos.x, dragEndPos.y, dragStartPos.z));
        GL.Vertex(new Vector3(dragStartPos.x, dragStartPos.y, dragEndPos.z));
        GL.End();
        
        GL.PopMatrix();
    }
}
