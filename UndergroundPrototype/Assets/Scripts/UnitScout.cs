using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScout : MonoBehaviour
{
    public Vector3 relativePos;
    public GameObject scoutField;
    int rotationCalc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        relativePos = Input.mousePosition - transform.position;
    }

    public void DoAction(UnitBehavior self)
    {
        Debug.Log("UnitScout");
        Instantiate(scoutField, transform.position, Quaternion.Euler(0,0,0));
        self.DoInvoke(new UnitDataEventArgs(this, "Hold", Vector3.zero)); // use this function to stop unit (when done for example)
    }
}
