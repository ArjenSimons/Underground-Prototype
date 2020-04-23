using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    [SerializeField] private StructureHealth structureHealth;
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI healthText;

    private void Start()
    {
        progressBar.minValue = 0;
        progressBar.maxValue = structureHealth.Health;
    }

    private void FixedUpdate()
    {
        progressBar.transform.rotation = Camera.main.transform.rotation;
        progressBar.value = structureHealth.Health;
        healthText.SetText("{0}", structureHealth.Health);
    }
    
}
