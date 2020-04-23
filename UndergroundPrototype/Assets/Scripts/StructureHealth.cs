using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class StructureHealth : MonoBehaviour
{
    [SerializeField] private int health;
    public int Health => health;
    
    [HideInInspector] public UnityEvent buildingIsDestroyed;

    public void DamageBuilding(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            buildingIsDestroyed.Invoke();
        }
    }
}
