using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitWallBreaker : MonoBehaviour
{

    public void DoAction(UnitBehavior self)
    {
        if (self.SelectionData.selectedObject.tag == "Wall")
        {
            Debug.Log("action on wall");
        }
        else
        {
            Debug.Log(self.SelectionData.selectedObject.tag);
            Debug.Log("cancel action");
            StopAction(self);
        }
    }

    private void StopAction(UnitBehavior self)
    {
        self.DoInvoke(new UnitDataEventArgs(this, "Hold", Vector3.zero)); // use this function to stop unit (when done for example)
    }
}
