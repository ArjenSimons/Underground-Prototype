using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWarScript : MonoBehaviour
{

    public GameObject m_fogOfWarPlane;
    public Transform m_ground;
    public LayerMask fogLayer;
    public float visionRadius = 5f;
    public float visionSqrt { get { return visionRadius * visionRadius; } }

    private Mesh fogMesh;
    private Vector3[] fogVertices;
    private Color[] fogColors;


    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        Ray r = new Ray(transform.position, m_ground.position - transform.position);
        RaycastHit hit;
        Debug.DrawRay(transform.position, m_ground.position - transform.position);
        if(Physics.Raycast(r,out hit, 1000,fogLayer, QueryTriggerInteraction.Collide))
        {
            for(int i = 0; i < fogVertices.Length; i++)
            {
                Vector3 v = m_fogOfWarPlane.transform.TransformPoint(fogVertices[i]);
                float dist = Vector3.SqrMagnitude(v - hit.point);
                if(dist < visionSqrt)
                {
                    float alpha = Mathf.Min(fogColors[i].a, dist / visionSqrt);
                    fogColors[i].a = alpha;
                }
            }
            UpdateColor();
        }
    }

    void Initialize()
    {
        fogMesh = m_fogOfWarPlane.GetComponent<MeshFilter>().mesh;
        fogVertices = fogMesh.vertices;
        fogColors = new Color[fogVertices.Length];
        for(int i = 0; i < fogColors.Length; i++)
        {
            fogColors[i] = Color.black;
        }
        UpdateColor();
    }

    void UpdateColor()
    {
        fogMesh.colors = fogColors;
    }
}
