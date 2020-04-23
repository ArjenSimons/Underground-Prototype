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
        //self.DoInvoke(new UnitDataEventArgs(this, "Hold", Vector3.zero)); // use this function to stop unit (when done for example)
        GameObject target = self.SelectionData.selectedObject;

        //if (LayerMask.LayerToName(target.layer) == "Enemy" /*add tag support*/)
        //{
            AttackTarget(target);
        //} //

        //self.Move
    }

    void OnTriggerEnter(Collider other)
    {
        //if (LayerMask.LayerToName(other.gameObject.layer) == "Enemy")
        //{
        if (other.gameObject.name == "UnitFighter(clone)")
        {
            Debug.Log(other.gameObject.name);
        }
        //}
    }

    private void AttackTarget(GameObject enemy)
    {
        
        MoveToTarget(enemy.transform.position);

    }

    private void OnInvoke()
    {



        // TODO:
        // MOVE TO VICINITY OF UNIT
        // HOLD POSITION
        // ATTACK UNIT
        // DEALSOMEDAMAGE();
    }

    private void MoveToTarget(Vector3 enemyPos)
    {
        
        float step = 5 * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(enemyPos.x, transform.position.y, enemyPos.z), step);
        
    }
}