using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBuilding : MonoBehaviour
{

    public bool placingBuilding;
    InputHandler inputHandler;

    // Start is called before the first frame update
    void Start()
    {
        placingBuilding = true;
        inputHandler = GameObject.Find("Main Camera").GetComponent<InputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (placingBuilding == true) {
            Vector3 raycastPlacePos = inputHandler.SelectPositon(Input.mousePosition);
            //Force height at ground level
            raycastPlacePos.y = 1;
            this.transform.position = raycastPlacePos;
            }
    }
}
