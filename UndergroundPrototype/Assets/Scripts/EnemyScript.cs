using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private float count;

    [SerializeField]
    private int unitHealth = 10;
    public int UnitHealth
    {
        get { return unitHealth; }
        set { unitHealth -= value; }
    }

    void Update()
    {
        if (unitHealth <= 1)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        //Debug.Log(LayerMask.LayerToName(other.gameObject.layer));
        if (LayerMask.LayerToName(other.gameObject.layer) == "Default" || LayerMask.LayerToName(other.gameObject.layer) == "IgnoreRayCast")
        {
            count += Time.deltaTime;
            if (count > .5f)
            {
                other.GetComponent<UnitBehavior>().UnitHealth = 1;
                count = 0;
            }
        }
    }
}
