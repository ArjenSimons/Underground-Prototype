using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitFighter : MonoBehaviour
{
    private UnitBehavior self;
    [SerializeField]
    private float count = 0;
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
        GameObject target = self.SelectionData.selectedObject;
        this.self = self;
        //count = 0;


        //if (self.isEnemy) { return; }
        if (target != null)
        {
            if (LayerMask.LayerToName(target.gameObject.layer) == "Enemy" /*add tag support*/)
            {
                this.self.MoveUnitToPosition(target.transform.position);
            }
        }
        //else
        //{
        //    self.DoInvoke(new UnitDataEventArgs(this, "Hold", Vector3.zero)); // use this function to stop unit (when done for example)
        //}

    }

    void OnTriggerStay(Collider other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Enemy")
        {
            //Debug.Log("Attacking" + other.gameObject.name);
            AttackTarget(other.gameObject);
        } else if (LayerMask.LayerToName(other.gameObject.layer) == "EnemyBaseStation")
        {
            AttackBuilding(other.gameObject);
        }
    }

    void AttackTarget(GameObject enemy)
    {
        count += Time.deltaTime;
        if (count > .5f)
        {
            //Debug.Log("dealing 1 damage");
            enemy.GetComponent<EnemyScript>().UnitHealth = 1;
            count = 0;
        }

    }

    private void AttackBuilding(GameObject enemy)
    {
        count += Time.deltaTime;
        if (count > 1.0f)
        {
            enemy.GetComponentInParent<StructureHealth>().DamageBuilding(1);
            count = 0;
        }
    }
}