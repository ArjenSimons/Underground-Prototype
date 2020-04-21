using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseStation : MonoBehaviour
{
    public enum Units
    {
        BUILDER,
        WALL_BREAKER,
        FIGHTER,
        SCOUT
    }

    [SerializeField] private ResourceManager resourceManager;

    [Header("Units")]
    [SerializeField] private float builderCost = 10;
    [SerializeField] private float wallBreakerCost = 10;
    [SerializeField] private float fighterFuelCost = 20;
    [SerializeField] private float fighterCrystalCost = 5;
    [SerializeField] private float scoutCost = 20;

    [Header("ui")]
    [SerializeField] private GameObject createUnitCanvas;
    [SerializeField] private Slider progressBar;
    [SerializeField] private Button btnBuilder;
    [SerializeField] private Button btnWallBreaker;
    [SerializeField] private Button btnFighter;
    [SerializeField] private Button btnScout;
    [SerializeField] private TextMeshProUGUI builderCostDisplay;
    [SerializeField] private TextMeshProUGUI wallBreakerCostDisplay;
    [SerializeField] private TextMeshProUGUI fighterCostFuelDisplay;
    [SerializeField] private TextMeshProUGUI fighterCostCrystalDisplay;
    [SerializeField] private TextMeshProUGUI scoutCostDisplay;

    private LayerMask layerMask;

    private void Start()
    {
        layerMask = 1 << LayerMask.NameToLayer("PlayerBaseStation");

        //Set cost displays
        builderCostDisplay.text = builderCost.ToString();
        wallBreakerCostDisplay.text = wallBreakerCost.ToString();
        fighterCostFuelDisplay.text = fighterFuelCost.ToString();
        fighterCostCrystalDisplay.text = fighterCrystalCost.ToString();
        scoutCostDisplay.text = scoutCost.ToString();

        //SetListeners
        btnBuilder.onClick.AddListener(OnBtnBuilderClicked);
        btnWallBreaker.onClick.AddListener(OnBtnWallBreakerClicked);
        btnFighter.onClick.AddListener(OnBtnFighterClicked);
        btnScout.onClick.AddListener(OnBtnScoutClicked);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject(-1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("PlayerBaseStation"))
                {
                    OpenUnitCreateWindow();
                }
                else if (hit.collider.gameObject.layer != LayerMask.NameToLayer("UI"))
                {
                    CloseUnitCreateWindow();
                }
            }
        }
    }

    private void OnBtnBuilderClicked()
    {
        if (resourceManager.Fuel >= builderCost)
        {
            Debug.Log("Making Builder...");

            //TODO: Create builder
        }
        else { Debug.Log("Not enough recouses"); }
    }

    private void OnBtnWallBreakerClicked()
    {
        if (resourceManager.Fuel >= wallBreakerCost)
        {
            Debug.Log("Making Wall Breaker...");
        }
        else { Debug.Log("Not enough recouses"); }
    }

    private void OnBtnFighterClicked()
    {
        if (resourceManager.Fuel >= fighterFuelCost && resourceManager.Crystal >= fighterCrystalCost)
        {
            Debug.Log("Making Fighter...");
        }
        else { Debug.Log("Not enough recouses"); }
    }

    private void OnBtnScoutClicked()
    {
        if(resourceManager.Fuel >= scoutCost)
        {
            Debug.Log("Making Scout...");
        }
        else { Debug.Log("Not enough recouses"); }
    }

    private void OpenUnitCreateWindow()
    {
        createUnitCanvas.gameObject.SetActive(true);
    }

    private void CloseUnitCreateWindow()
    {
        createUnitCanvas.gameObject.SetActive(false);
    }
}