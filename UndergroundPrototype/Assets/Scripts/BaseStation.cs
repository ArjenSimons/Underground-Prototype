using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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
    [SerializeField] private StructureHealth structureHealth;

    [Header("Units")]
    [SerializeField] private GameObject builderPrefab;
    [SerializeField] private GameObject wallBreakerPrefab;
    [SerializeField] private GameObject fighterPrefab;
    [SerializeField] private GameObject scoutPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int builderCost = 10;
    [SerializeField] private int wallBreakerCost = 10;
    [SerializeField] private int fighterFuelCost = 20;
    [SerializeField] private int fighterCrystalCost = 5;
    [SerializeField] private int scoutCost = 20;
    [SerializeField] private int builderCreationTime = 8;
    [SerializeField] private int wallBreakerCreationTime = 8;
    [SerializeField] private int fighterCreationTime = 14;
    [SerializeField] private int scoutCreationTime = 5;

    [Header("ui")]
    [SerializeField] private GameObject createUnitCanvas;
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI creationText;
    [SerializeField] private Button btnBuilder;
    [SerializeField] private Button btnWallBreaker;
    [SerializeField] private Button btnFighter;
    [SerializeField] private Button btnScout;
    [SerializeField] private TextMeshProUGUI builderCostDisplay;
    [SerializeField] private TextMeshProUGUI wallBreakerCostDisplay;
    [SerializeField] private TextMeshProUGUI fighterCostFuelDisplay;
    [SerializeField] private TextMeshProUGUI fighterCostCrystalDisplay;
    [SerializeField] private TextMeshProUGUI scoutCostDisplay;
    [SerializeField] private GameObject txtWin;
    [SerializeField] private GameObject txtLose;

    private LayerMask layerMask;
    private bool isCreatingUnit;

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

        structureHealth.buildingIsDestroyed.AddListener(OnStructureDestroyed);
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

    private void OnStructureDestroyed()
    {
        if (tag == "PlayerBase")
        {
            txtLose.SetActive(true);
        }
        else if (tag == "EnemyBase")
        {
            txtWin.SetActive(true);
        }
    }

    private void OnBtnBuilderClicked()
    {
        if (isCreatingUnit)
        {
            Debug.Log("Can't make unit, already creating one");
            return;
        }
        if (resourceManager.Fuel >= builderCost)
        {
            Debug.Log("Start Making Builder...");
            resourceManager.ChangeFuelAmount(-builderCost);
            StartCoroutine(CreateBuilder());
        }
        else { Debug.Log("Not enough recouses"); }
    }

    private IEnumerator CreateBuilder()
    {
        isCreatingUnit = true;
        progressBar.value = 0;
        SetCreatingText("Builder");
        int secondsPassed = 0;
        while (secondsPassed < builderCreationTime)
        {
            yield return new WaitForSeconds(1);
            secondsPassed += 1;

            progressBar.value = (float)secondsPassed / builderCreationTime;
        }
        Debug.Log("Builder created");
        CreatingFinished(Units.BUILDER);
    }

    private void OnBtnWallBreakerClicked()
    {
        if (isCreatingUnit)
        {
            Debug.Log("Can't make unit, already creating one");
            return;
        }
        if (resourceManager.Fuel >= wallBreakerCost)
        {
            resourceManager.ChangeFuelAmount(-wallBreakerCost);
            StartCoroutine(CreateWallBreaker());
        }
        else { Debug.Log("Not enough recouses"); }
    }

    private IEnumerator CreateWallBreaker()
    {
        isCreatingUnit = true;
        progressBar.value = 0;
        SetCreatingText("Wall Breaker");
        int secondsPassed = 0;
        while (secondsPassed < wallBreakerCreationTime)
        {
            yield return new WaitForSeconds(1);
            secondsPassed += 1;
            progressBar.value = (float)secondsPassed / wallBreakerCreationTime;
        }
        Debug.Log("Wall breaker created");
        CreatingFinished(Units.WALL_BREAKER);
    }

    private void OnBtnFighterClicked()
    {
        if (isCreatingUnit)
        {
            Debug.Log("Can't make unit, already creating one");
            return;
        }
        if (resourceManager.Fuel >= fighterFuelCost && resourceManager.Crystal >= fighterCrystalCost)
        {
            resourceManager.ChangeFuelAmount(-fighterFuelCost);
            resourceManager.ChangeCrystalAmount(fighterCrystalCost);
            StartCoroutine(CreateFighter());
        }
        else { Debug.Log("Not enough recouses"); }
    }

    private IEnumerator CreateFighter()
    {
        isCreatingUnit = true;
        progressBar.value = 0;
        SetCreatingText("Fighter");
        int secondsPassed = 0;
        while (secondsPassed < fighterCreationTime)
        {
            yield return new WaitForSeconds(1);
            secondsPassed += 1;
            progressBar.value = (float)secondsPassed / fighterCreationTime;
        }
        Debug.Log("Fighter created");
        CreatingFinished(Units.FIGHTER);
    }

    private void OnBtnScoutClicked()
    {
        if (isCreatingUnit)
        {
            Debug.Log("Can't make unit, already creating one");
            return;
        }
        if (resourceManager.Fuel >= scoutCost)
        {
            resourceManager.ChangeFuelAmount(-scoutCost);
            StartCoroutine(CreateScout());
        }
        else { Debug.Log("Not enough recouses"); }
    }

    private IEnumerator CreateScout()
    {
        isCreatingUnit = true;
        progressBar.value = 0;
        SetCreatingText("Scout");
        int secondsPassed = 0;
        while (secondsPassed < scoutCreationTime)
        {
            yield return new WaitForSeconds(1);
            secondsPassed += 1;
            progressBar.value = (float)secondsPassed / scoutCreationTime;
        }
        Debug.Log("Scout created");
        CreatingFinished(Units.SCOUT);
    }

    private void SpawnUnit(Units unit)
    {
        switch (unit)
        {
            case Units.BUILDER:
                Instantiate(builderPrefab, spawnPoint);
                break;
            case Units.WALL_BREAKER:
                Instantiate(wallBreakerPrefab, spawnPoint);
                break;
            case Units.FIGHTER:
                Instantiate(fighterPrefab, spawnPoint);
                break;
            case Units.SCOUT:
                Instantiate(scoutPrefab, spawnPoint);
                break;
        }
    }

    private void CreatingFinished(Units unit)
    {
        SpawnUnit(unit);
        isCreatingUnit = false;
        SetCreatingText(creating: false);
    }

    private void SetCreatingText(string unit = "", bool creating = true)
    {
        if (creating)
        {
            creationText.text = string.Format("Creating {0}...", unit);
        }
        else { creationText.SetText("Select a unit to create!"); }
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