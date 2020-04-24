using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchGroundType : MonoBehaviour
{
    [SerializeField]
    private Transform actualTransform;

    private GroundScript groundScript;

    public GroundScript GroundScript => groundScript;

    private void Awake()
    {
        WallScript wall = GetComponent<WallScript>();

        LayerMask layerMask = 1 << LayerMask.NameToLayer("Ground");
        RaycastHit groundBlock;

        if (Physics.Raycast(actualTransform.transform.position, actualTransform.TransformDirection(Vector3.down), out groundBlock, layerMask))
        {
            groundScript = groundBlock.collider.GetComponentInParent<GroundScript>();

            groundScript.type = wall.type;
            groundScript.freeSocket = false;
        }
    }
}
