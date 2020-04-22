using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogPlaneScript : MonoBehaviour
{

    [SerializeField] private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer.enabled = true;
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
