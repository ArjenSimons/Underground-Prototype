using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnitPerformAction : UnityEvent<string>
{

}

public class UnitBehavior : MonoBehaviour
{
    //Dictionary<string, int> actionList = new Dictionary<string, int>();

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
            // holding postition
            return;
        }
        if (currentAction == 1)
        {
            //MoveToPosition();
        }
        if (currentAction == 2)
        {
            //AttackSelectedUnit();
        }
    }

    void CallForAction(string action)
    {
        switch (action)
        {
            case "Move":
                currentAction = (int)ActionList.Move;
                break;
            case "Attack":
                currentAction = (int)ActionList.Attack;
                break;
            case "Hold":
                currentAction = (int)ActionList.Hold;
                break;
        }
    }
}
