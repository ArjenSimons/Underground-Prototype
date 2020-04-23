using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBuilder : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float hitTime = 2;
    private float timer = 0;
    UnitBehavior self;
    InputHandler inputHandler;

    private void Start()
    {
        inputHandler = Camera.main.GetComponent<InputHandler>();
    }

    public void DoAction(UnitBehavior self)
    {
        this.self = self;

        if (inputHandler.moveBuilder == true)
        {
            print(Vector3.Distance(transform.position, inputHandler.buildPos));
            if (Vector3.Distance(transform.position, inputHandler.buildPos) > range)
            {
                //Walk towards point
                Debug.Log(inputHandler.buildPos);
                self.MoveUnitToPosition(inputHandler.buildPos);
            }
            else
            {
                //Collect resource
                Debug.Log("destination reached");
                inputHandler.builderArrived = true;
            }
            Debug.Log("action on wall");
        }
        else
        {
            //Debug.Log(self.SelectionData.selectedObject.tag);
            Debug.Log("cancel action");
            StopAction(self);
        }


    }

    private bool CheckBuilders()
    {
        for (int i = 0; i < inputHandler.SelectedUnits.Count; i++)
        {
            if (inputHandler.SelectedUnits[i].GetComponent<UnitBehavior>().CurrentUnitType != 0)
            {
                return false;
            }
        }
        return true;
    }



    private void StopAction(UnitBehavior self)
    {
        timer = 0;
        self.DoInvoke(new UnitDataEventArgs(this, "Hold", Vector3.zero)); // use this function to stop unit (when done for example)
    }
}
