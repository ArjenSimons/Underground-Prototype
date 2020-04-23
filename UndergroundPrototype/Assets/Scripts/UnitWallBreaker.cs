using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitWallBreaker : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float hitTime = 2;
    private float timer = 2;


    public void DoAction(UnitBehavior self)
    {
        if (self.SelectionData.selectedObject != null && self.SelectionData.selectedObject.tag == "Wall")
        {
            
            if (Vector3.Distance(transform.position, self.SelectionData.selectedObject.transform.position) > range)
            {
                //Walk towards point
                self.MoveUnitToPosition(self.SelectionData.pos);
            }
            else
            {

                //knock down wall
                timer += Time.deltaTime;

                if (timer >= 2)
                {

                    WallScript wallScript = self.SelectionData.selectedObject.GetComponentInParent<WallScript>();

                    if (wallScript.health == 1)
                    {
                        StopAction(self);
                    }
                    wallScript.health -= 1;
                    
                    timer = 0;
                }
            }
        }
        else
        {
            //Debug.Log(self.SelectionData.selectedObject.tag);
            StopAction(self);
        }
    }

    private void StopAction(UnitBehavior self)
    {
        timer = 0;
        self.DoInvoke(new UnitDataEventArgs(this, "Hold", Vector3.zero)); // use this function to stop unit (when done for example)
    }
}
