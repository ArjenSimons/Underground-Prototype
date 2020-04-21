using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchGroundType : MonoBehaviour
{
    private void Awake()
    {
        WallScript wall = GetComponent<WallScript>();

        if (wall.type != BlockType.Regular)
        {
            LayerMask layerMask = 1 << LayerMask.NameToLayer("Ground");
            RaycastHit groundBlock;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out groundBlock, layerMask))
            {
                GroundScript ground = groundBlock.collider.GetComponentInParent<GroundScript>();

                ground.type = wall.type;
            }
        }
    }
}
