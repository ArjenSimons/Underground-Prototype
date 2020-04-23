using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBuilder : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float hitTime = 2;
    private float timer = 0;

    private int iron, fuel, crystal;

    public void DoAction(UnitBehavior self)
    {

        print(self.SelectionData.selectedObject.tag);
        if (self.SelectionData.selectedObject != null && self.SelectionData.selectedObject.tag == "Resource")
        {
            if (Vector3.Distance(transform.position, self.SelectionData.selectedObject.transform.position) > range)
            {
                //Walk towards point
                Debug.Log("walking");
                self.MoveUnitToPosition(self.SelectionData.pos);
            }
            else
            {
                //Collect resource
                Debug.Log("destination reached");
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

    private void StopAction(UnitBehavior self)
    {
        timer = 0;
        self.DoInvoke(new UnitDataEventArgs(this, "Hold", Vector3.zero)); // use this function to stop unit (when done for example)
    }
}
