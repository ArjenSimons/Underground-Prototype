using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCorection : MonoBehaviour
{
    Quaternion rotation;

    private void Start()
    {
        rotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        transform.rotation = rotation;
    }
}
