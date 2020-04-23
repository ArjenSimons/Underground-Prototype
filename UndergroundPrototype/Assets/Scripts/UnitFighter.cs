using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitFighter : MonoBehaviour
{
    private UnitBehavior ub;
    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("UnitFighter");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DoAction(UnitBehavior self)
    {
        self.DoInvoke(new UnitDataEventArgs(this, "Hold", Vector3.zero)); // use this function to stop unit (when done for example)

        this.
    }

    private void OnInvoke()
    {



        // TODO:
        // MOVE TO VICINITY OF UNIT
        // HOLD POSITION
        // ATTACK UNIT
        // DEALSOMEDAMAGE();
    }
}