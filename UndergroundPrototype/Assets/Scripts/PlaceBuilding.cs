﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBuilding : MonoBehaviour
{
    public bool placingBuilding;
    InputHandler inputHandler;
    ResourceManager resourceManager;
    Material opaqueMaterial;
    public Material transparentMaterial;
    BlockType currentResource;

    private bool buildingDone;
    [SerializeField] private int progressTime = 2;
    public float progressAmount;
    private int progressCounter = 0;
    public int maxProgress = 100;

    private bool allowMaterialChange = true;
    private bool gatherResources;
    private int gatherCounter;
    private int gatherTime;

    private bool buildingPlaced;

    // Start is called before the first frame update
    void Start()
    {
        gatherTime = 15 * 50;
        gatherCounter = gatherTime;
        //Grab transparent material from resource folder
        transparentMaterial = Resources.Load<Material>("Materials/BuildingTransparent");
        opaqueMaterial = Resources.Load<Material>("Materials/BuildingOpaque");
        placingBuilding = true;
        inputHandler = GameObject.Find("Main Camera").GetComponent<InputHandler>();
        resourceManager = GameObject.Find("Resource Panel").GetComponent<ResourceManager>();

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
                if (!buildingPlaced)
                {
                    resourceManager.ChangeIronAmount(-20);
                }
                buildingPlaced = true;

                //Enable FoW visibility
                gameObject.transform.Find("Mine/ApertureMask").GetComponent<MeshRenderer>().enabled = true;
                //Set resource where the mine is currently located
                currentResource = this.gameObject.GetComponent<CheckGround>().CheckType();
                //Remove transparancy on placement
                if (buildingDone == true)
                {
                    print("Changing back material");
                    this.gameObject.GetComponentInChildren<Renderer>().material = opaqueMaterial;
                    
                    inputHandler.builderArrived = false;
                    inputHandler.moveBuilder = false;
                    //Make sure the material is set once
                    allowMaterialChange = false;
                    //Start gathering resources
                    gatherResources = true;

                }
            }
        }


    }

    private void FixedUpdate()
    {
        if (gatherResources)
        {
            gatherCounter++;
            if (gatherCounter > gatherTime)
                switch (currentResource)
                {
                    case BlockType.Crystal:
                        //Add crystals
                        resourceManager.ChangeCrystalAmount(3);
                        gatherCounter = 0;
                        break;
                    case BlockType.Fuel:
                        //Add fuel
                        resourceManager.ChangeFuelAmount(5);
                        gatherCounter = 0;
                        break;
                    case BlockType.Iron:
                        //Add iron
                        resourceManager.ChangeIronAmount(5);
                        gatherCounter = 0;
                        break;
                }
        }
        if (inputHandler.builderArrived == true)
        {
            if (placingBuilding == false)
            {
                if (buildingDone == false)
                {
                    progressCounter++;
                    if (progressCounter > progressTime)
                    {
                        progressAmount++;
                        progressCounter = 0;
                    }
                    if (progressAmount >= maxProgress)
                    {
                        buildingDone = true;
                    }
                }
            }
        }

    }
}
