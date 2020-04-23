using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScout : MonoBehaviour
{
    public Vector3 relativePos;
    public GameObject scoutField;
    public GameObject targetObject;
    public Vector3 direction;
    public Vector3 directionNorm;
    int rotationCalc;
    int range = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoAction(UnitBehavior self)
    {
        Debug.Log("UnitScout");
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
                targetObject = self.SelectionData.selectedObject;
                direction = targetObject.transform.position - transform.position;
                directionNorm = direction.normalized;
                rotationCalc = CalcRot(directionNorm);
                Instantiate(scoutField, transform.position, Quaternion.Euler(0, rotationCalc, 0));
                self.UnitHealth = 999; // destroys scout
                //Destroy(this.gameObject);
            }
        }
        else
        {
            //Debug.Log(self.SelectionData.selectedObject.tag);
            Debug.Log("cancel action");
            //StopAction(self);
        }

        /*
        targetObject = self.SelectionData.selectedObject;
        direction = targetObject.transform.position - transform.position;
        directionNorm = direction.normalized;
        rotationCalc = CalcRot(directionNorm);
        Instantiate(scoutField, transform.position, Quaternion.Euler(0, rotationCalc, 0));
        self.DoInvoke(new UnitDataEventArgs(this, "Hold", Vector3.zero)); // use this function to stop unit (when done for example)*/
        
    }

    int CalcRot(Vector3 direction)
    {
        int rot = 0;
        

        if( direction.x >= -1  && direction.x < 0.5 && -0.5 < direction.z && direction.z < 0.5)
        {
            rot = 180;
        }
        else if(direction.x <= 1 && direction.x > 0.5 && -0.5 < direction.z && direction.z < 0.5)
        {
            rot = 0;
        }
        else if (direction.z >= - 1 &&  direction.z < 0.5 && -0.5 < direction.x && direction.x < 0.5)
        {
            rot = 90;
        }
        else if (direction.z <= 1 && direction.z > 0.5 && -0.5 < direction.x && direction.x < 0.5)
        {
            rot = 270;
        }
        return rot;

    }
}
