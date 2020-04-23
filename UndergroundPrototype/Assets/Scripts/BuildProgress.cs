using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class BuildProgress : MonoBehaviour
{
    [SerializeField] private PlaceBuilding placeBuilding;
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI healthText;

    private void Start()
    {
        progressBar.minValue = 0;
        progressBar.maxValue = placeBuilding.maxProgress;
    }

    private void FixedUpdate()
    {
        progressBar.transform.rotation = Camera.main.transform.rotation;
        if (placeBuilding.progressAmount < placeBuilding.maxProgress)
        {
            progressBar.value = placeBuilding.progressAmount;
            healthText.SetText("{0}%", placeBuilding.progressAmount);
        }
        else{
            Destroy(this.gameObject);
        }
    }

}
