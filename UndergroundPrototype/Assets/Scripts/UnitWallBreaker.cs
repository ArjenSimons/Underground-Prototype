using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitWallBreaker : MonoBehaviour
{
    [SerializeField] private float range;


    public void DoAction(UnitBehavior self)
    {
        if (self.SelectionData.selectedObject.tag == "Wall")
        {
            if (Vector3.Distance(transform.position, self.SelectionData.selectedObject.transform.position) > range)
            {
                //Walk towards point
                Debug.Log("walking");
                self.MoveUnitToPosition(self.SelectionData.pos);
            }
            else
            {
                //knock down wall
                Debug.Log("destenation reached");
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
        self.DoInvoke(new UnitDataEventArgs(this, "Hold", Vector3.zero)); // use this function to stop unit (when done for example)
    }
}
