using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnitPerformAction : UnityEvent<UnitDataEventArgs> { } // override to use type

public class UnitBehavior : MonoBehaviour
{
    [SerializeField]
    private Vector3 moveOrder = new Vector3(0,0,0);

    private UnitDataEventArgs selectionData;
    public UnitDataEventArgs SelectionData
    {
        get { return selectionData; }
        set { selectionData = value; }
    }


    enum UnitType
    {
        UnitBuilder = 0,
        UnitWallBreaker = 1,
        UnitFighter = 2,
        UnitScout = 3
    }

    [SerializeField]
    private UnitType currentUnitType = UnitType.UnitBuilder;

    enum ActionList
    {
        Hold = 0,
        Move = 1,
        Action = 2
    }

    public UnitPerformAction unitEvent;
    private int currentAction = 0;
    protected InputHandler inputHandler;

    // Start is called before the first frame update
    virtual protected void Start()
    {
        inputHandler = Camera.main.GetComponent<InputHandler>();
        inputHandler.AllUnits = this.gameObject; // adds oneself to list of inputhandler

        //if (unitEvent == null) { unitEvent = new UnitPerformAction(); }
        unitEvent = new UnitPerformAction();
        unitEvent.AddListener(CallForAction);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleActions();
    }

    virtual protected void HandleActions()
    {
        if (currentAction == 0)
        {
            // holding postition
            return;
        }
        if (currentAction == 1)
        {
            MoveUnitToPosition(moveOrder);
        }
        if (currentAction == 2)
        {
            switch (currentUnitType)
            {
                case UnitType.UnitBuilder:
                    if (GetComponent<UnitBuilder>()) { GetComponent<UnitBuilder>().DoAction(this); }
                    else { Debug.Log("either unit doesnt have unitscript or is not assigned correct unittype"); }
                    break;
                case UnitType.UnitScout:
                    if (GetComponent<UnitScout>()) { GetComponent<UnitScout>().DoAction(this); }
                    else { Debug.Log("either unit doesnt have unitscript or is not assigned correct unittype"); }
                    break;
                case UnitType.UnitFighter:
                    if (GetComponent<UnitFighter>()) { GetComponent<UnitFighter>().DoAction(this); }
                    else { Debug.Log("either unit doesnt have unitscript or is not assigned correct unittype"); }
                    break;
                case UnitType.UnitWallBreaker:
                    if (GetComponent<UnitWallBreaker>()) { GetComponent<UnitWallBreaker>().DoAction(this); }
                    else { Debug.Log("either unit doesnt have unitscript or is not assigned correct unittype"); }
                    break;
            }
        }
    }

    virtual public void DoInvoke(UnitDataEventArgs args)
    {
        
        unitEvent.Invoke(args);
    }

    virtual protected void CallForAction(UnitDataEventArgs data)
    {
        //Debug.Log(data.pos);
        //Debug.Log("action is coming");
        switch (data.action)
        {
            case "Move":
                moveOrder = data.pos;
                currentAction = (int) ActionList.Move;
                break;
            case "Action":
                SelectionData = data;
                currentAction = (int) ActionList.Action;
                break;
            case "Hold":
                currentAction = (int) ActionList.Hold;
                break;
        }
    }

    public void MoveUnitToPosition(Vector3 selectedPos)
    {
        float step = 5 * Time.deltaTime;

        if (CheckForTerrain())
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(selectedPos.x, transform.position.y, selectedPos.z), step);
        }
        
        RotateTowards(moveOrder);

        //Debug.Log(transform.position + " / " + selectedPos);
        if (Vector3.Distance(transform.position, selectedPos) < .5f)
        {
            currentAction = (int) ActionList.Hold;
            //Debug.Log("Resetting action to hold");
        }
    }

    protected void AttackSelectedUnit(GameObject enemy)
    {

    }

    public void Select()
    {
        this.GetComponent<Renderer>().material.color = Color.green;
    }

    public void Deselect()
    {
        this.GetComponent<Renderer>().material.color = Color.white;
    }

    private void RotateTowards(Vector3 pos)
    {
        this.transform.LookAt(new Vector3(pos.x,this.transform.position.y, pos.z));
    }

    private bool CheckForTerrain()
    {
        RaycastHit target = CastRay(this.transform.position, this.transform.forward);
        if (target.collider != null)
        {
            return false;
        } else
        {
            return true;
        }
    }

    private RaycastHit CastRay(Vector3 startPos, Vector3 direction)
    {
        //Debug.Log(mousePos);
        RaycastHit hit;
        float distance = .2f;
        
        //Debug.DrawRay(startPos, direction * .6f, Color.red);

        if (Physics.Raycast(startPos, direction, out hit, distance))
        {
            //Debug.DrawRay(startPos, direction * hit.distance, Color.red);
            return hit;
        }
        else
        {
            //Debug.DrawRay(ray.origin, ray.direction * 100, Color.white);
            //Debug.Log("Failed to hit surface");
        }
        return new RaycastHit();
    }
}

public struct UnitDataEventArgs
{
    public object sender;
    public string action;
    public Vector3 pos;
    public GameObject selectedObject;

    public UnitDataEventArgs(object sender, string action, Vector3 pos, GameObject selectedObject = null)
    {
        this.sender = sender;
        this.action = action;
        this.pos = pos;
        this.selectedObject = selectedObject;
    }
}
