﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScout : MonoBehaviour
{
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
        self.DoInvoke(new UnitDataEventArgs(this, "Hold", Vector3.zero)); // use this function to stop unit (when done for example)
    }
}