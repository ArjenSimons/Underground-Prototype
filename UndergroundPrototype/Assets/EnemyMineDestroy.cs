using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMineDestroy : MonoBehaviour
{
    [SerializeField] private StructureHealth structureHealth;

    private void Start()
    {
        structureHealth.buildingIsDestroyed.AddListener(() => { Debug.Log("destroy"); Destroy(gameObject); });
    }
}
