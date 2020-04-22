using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridSnap : MonoBehaviour
{
    [SerializeField] private float gridSize = 1;
    [SerializeField] private Vector3 offset;
    private Vector3 truePos;

    private void Update()
    {
        truePos.x = Mathf.Floor(transform.parent.position.x / gridSize) * gridSize;
        truePos.y = Mathf.Floor(transform.parent.position.y / gridSize) * gridSize;
        truePos.z = Mathf.Floor(transform.parent.position.z / gridSize) * gridSize;

        transform.position = truePos + offset;
    }
}

