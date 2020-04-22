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
    private int currentAction = 0;

    // Start is called before the first frame update
    void Start()
    {
        //actionList.Add("Move", 0);
        //actionList.Add("Attack", 1);
        //actionList.Add("Hold", 2);
        InputHandler inputHandler = Camera.main.GetComponent<InputHandler>();
        inputHandler.AllUnits = this.gameObject; // adds oneself to list of inputhandler

        if (unitEvent == null) { unitEvent = new UnitPerformAction(); }
        unitEvent.AddListener(CallForAction);
    }

    // Update is called once per frame
    void Update()
    {
        HandleActions();
    }

    void HandleActions()
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

    void CallForAction(UnitDataEventArgs data)
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

    void MoveUnitToPosition(Vector3 selectedPos)
    {
        float step = 5 * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(selectedPos.x, transform.position.y, selectedPos.z), step);
        //Debug.Log(transform.position + " / " + selectedPos);
        if (Vector3.Distance(transform.position, selectedPos) < .5f)
        {
            currentAction = (int) ActionList.Hold;
            //Debug.Log("Resetting action to hold");
        }
    }

    void AttackSelectedUnit(GameObject enemy)
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
