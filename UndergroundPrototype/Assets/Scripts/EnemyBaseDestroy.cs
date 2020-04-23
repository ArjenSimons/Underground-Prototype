using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseDestroy : MonoBehaviour
{
    [SerializeField] private StructureHealth structureHealth;
    [SerializeField] private GameObject txtLose;

    private void Start()
    {

        structureHealth.buildingIsDestroyed.AddListener(OnStructureDestroyed);
    }

    private void OnStructureDestroyed()
    {
        txtLose.gameObject.SetActive(true);
    }
}
