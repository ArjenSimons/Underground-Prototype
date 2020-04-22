using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnitPerformAction : UnityEvent<UnitDataEventArgs> { } // override to use type

public class UnitBehavior : MonoBehaviour
{
    //Dictionary<string, int> actionList = new Dictionary<string, int>();
    //public void DataTemp(Vector3 pos, GameObject gameObj)
    //{
    //    Vector3 posData = pos;
    //    GameObject gameObjData = gameObj;
    //}
    private Vector3 moveOrder = new Vector3(0,0,0);

    enum ActionList
    {
        Hold = 0,
        Move = 1,
        Attack = 2
    }

    public UnitPerformAction unitEvent;
    [SerializeField]
    private int currentAction = 0;
    private InputHandler inputHandler;

    // Start is called before the first frame update
    void Start()
    {
        //actionList.Add("Move", 0);
        //actionList.Add("Attack", 1);
        //actionList.Add("Hold", 2);
        inputHandler = Camera.main.GetComponent<InputHandler>();
        inputHandler.AllUnits = this.gameObject; // adds oneself to list of inputhandler

        if (unitEvent == null) { unitEvent = new UnitPerformAction(); }
        unitEvent.AddListener(CallForAction);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleActions();
        CheckForTerrain();
    }

    virtual protected void HandleActions()
    {
        if (currentAction == 0)
        {
            //unitEvent.Invoke("Move", );
            // holding postition
            return;
        }
        if (currentAction == 1)
        {
            MoveUnitToPosition(moveOrder);
        }
        if (currentAction == 2)
        {
            AttackSelectedUnit(new GameObject());
        }
    }

    public void DoInvoke(UnitDataEventArgs args)
    {
        unitEvent.Invoke(args);
    }

    virtual protected void CallForAction(UnitDataEventArgs data)
    {
        //Debug.Log(data.pos);
        //Debug.Log(data.pos);
        //Debug.Log("action is coming");
        switch (data.action)
        {
            case "Move":
                moveOrder = data.pos;
                currentAction = (int) ActionList.Move;
                break;
            case "Attack":
                currentAction = (int) ActionList.Attack;
                break;
            case "Hold":
                currentAction = (int) ActionList.Hold;
                break;
        }
    }

    protected void MoveUnitToPosition(Vector3 selectedPos)
    {
        float step = 5 * Time.deltaTime;

        if (CheckForTerrain())
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(selectedPos.x, transform.position.y, selectedPos.z), step);
        }
        Vector3 selectedPosition = GameObject.Find("UnitSelector").transform.position;
        RotateTowards(selectedPosition);

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
        //RaycastHit forwardRight = CastRay(this.transform.position, this.transform.forward);
        //RaycastHit forwardLeft = CastRay(this.transform.position, new Vector3(-1, 0, 1));
        RaycastHit target = CastRay(this.transform.position, this.transform.forward);
        if (target.collider != null)
        {
            return false;
        } else
        {
            return true;
        }
        //return false;
    }

    private RaycastHit CastRay(Vector3 startPos, Vector3 direction)
    {
        //Debug.Log(mousePos);
        //Ray ray = new 
        RaycastHit hit;
        float distance = .2f;

        //Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);
        //Debug.DrawRay(startPos, direction * .6f, Color.red);

        if (Physics.Raycast(startPos, direction, out hit, distance))
        {
            //Debug.Log(LayerMask.LayerToName(hit.collider.gameObject.layer));
            //Debug.DrawRay(startPos, direction * hit.distance, Color.red);
            //Debug.Log(hit.point.y);
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
    //public GameObject enemyUnit;

    public UnitDataEventArgs (object sender, string action, Vector3 pos)
    {
        this.sender = sender;
        this.action = action; 
        this.pos = pos; 
    }

    //public UnitDataEventArgs(object sender, string action, GameObject enemyUnit)
    //{
    //    this.sender = sender;
    //    this.action = action;
    //    this.enemyUnit = enemyUnit;
    //}
}
