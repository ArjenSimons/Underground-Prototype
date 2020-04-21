using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBuilding : MonoBehaviour
{

    public bool placingBuilding;
    InputHandler inputHandler;
    Material opaqueMaterial;
    Material transparentMaterial;

    private bool allowMaterialChange = true;

    // Start is called before the first frame update
    void Start()
    {
        //Grab transparent material from resource folder
        transparentMaterial = Resources.Load<Material>("Materials/BuildingTransparent");
        opaqueMaterial = Resources.Load<Material>("Materials/BuildingOpaque");
        placingBuilding = true;
        inputHandler = GameObject.Find("Main Camera").GetComponent<InputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (placingBuilding == true)
        {
            //Make transparant in placement mode
            print(this.gameObject.GetComponentInChildren<Renderer>().gameObject.name);
            this.gameObject.GetComponentInChildren<Renderer>().material = transparentMaterial;

            Vector3 raycastPlacePos = inputHandler.SelectPositon(Input.mousePosition);
            //Force height at ground level
            raycastPlacePos.y = 1;
            this.transform.position = raycastPlacePos;
        }
        else
        {
            if (allowMaterialChange == true)
            {
                //Remove transparancy on placement
                print("Changing back material");
                this.gameObject.GetComponentInChildren<Renderer>().material = opaqueMaterial;
                //Make sure the material is set once
                allowMaterialChange = false;
            }
        }
    }

}
