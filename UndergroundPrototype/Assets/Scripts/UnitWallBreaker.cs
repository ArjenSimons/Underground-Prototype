using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitWallBreaker : MonoBehaviour
{

    public void DoAction(UnitBehavior self)
    {
        Debug.Log("UnitWallbreaker");
    }

    private void StopAction(UnitBehavior self)
    {
        self.DoInvoke(new UnitDataEventArgs(this, "Hold", Vector3.zero)); // use this function to stop unit (when done for example)
    }
}
