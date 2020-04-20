using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CustomGrid : MonoBehaviour
{
    //[SerializeField] private GameObject target;
    [SerializeField] private float gridSize = 1;
    private Vector3 truePos;

    private void Update()
    {
        truePos.x = Mathf.Floor(transform.parent.position.x / gridSize) * gridSize;
        truePos.y = Mathf.Floor(transform.parent.position.y / gridSize) * gridSize;
        truePos.z = Mathf.Floor(transform.parent.position.z / gridSize) * gridSize;

        transform.position = truePos;
    }
}
