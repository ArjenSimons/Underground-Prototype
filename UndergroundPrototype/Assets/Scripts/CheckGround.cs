using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    RaycastHit groundBlock;
    LayerMask layerMask;

    private void Awake()
    {
        layerMask = 1 << LayerMask.NameToLayer("Ground");
    }

    public bool CheckSocket()
    {
        if (Physics.Raycast(transform.GetChild(0).position, transform.GetChild(0).TransformDirection(Vector3.down), out groundBlock, layerMask))
        {
            GroundScript ground = groundBlock.collider.GetComponentInParent<GroundScript>();
            return ground.freeSocket;
        }
        return false;
    }

    public void SetSocket(bool freeSocket)
    {
        GroundScript ground = groundBlock.collider.GetComponentInParent<GroundScript>();
        if (freeSocket)
        {
            ground.freeSocket = true;
        }
        if (!freeSocket)
        {
            ground.freeSocket = false;
        }
    }

    public BlockType CheckType()
    {
        if (Physics.Raycast(transform.GetChild(0).position, transform.GetChild(0).TransformDirection(Vector3.down), out groundBlock, layerMask))
        {
            GroundScript ground = groundBlock.collider.GetComponentInParent<GroundScript>();
            return ground.GetType();
        }
        return BlockType.Regular;
    }
}
