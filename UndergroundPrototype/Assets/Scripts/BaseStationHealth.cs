using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StructureHealth : MonoBehaviour
{
    [SerializeField] private int health;
    public UnityEvent buildingIsDestroyed;

    public void DamageBuilding(int damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            buildingIsDestroyed.Invoke();
        }
    }
}
