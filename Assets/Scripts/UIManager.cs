using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public CityStats cityStats;
    public CursorManager cursorManager;
    public GridManager gridManager;

    public Text populationText;
    public Text moneyText;
    public Text happinessText;
    public Text gridCoordinatesText;
    public Text cityPowerOutputText;
    public Text cityPowerConsumptionText;

    private Zone.ZoneType selectedZoneType;

    private IBuilding selectedBuilding;

    public GameObject powerPlantAPrefab;

    public bool bulldozeMode;


    void Start()
    {
        if (cityStats == null)
        {
            Debug.LogError("CityStats component not found.");
        }

        selectedZoneType = Zone.ZoneType.Grass;
        bulldozeMode = false;
    }

    void Update()
    {
        if (cityStats != null)
        {
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        populationText.text = "Population: " + cityStats.population;
        moneyText.text = "Money: $" + cityStats.money;
        happinessText.text = "Happiness: " + cityStats.happiness;
        cityPowerOutputText.text = "City Power Output: " + cityStats.cityPowerOutput + " MW";
        cityPowerConsumptionText.text = "City Power Consumption: " + cityStats.cityPowerConsumption + " MW";
        gridCoordinatesText.text = "Grid Coordinates: " + "x: " + gridManager.mouseGridPosition.x + ", y: " + gridManager.mouseGridPosition.y;
    }

    public void OnTileClicked(Zone.ZoneType zoneType, ZoneAttributes zoneAttributes)
    {
        if (cityStats.CanAfford(zoneAttributes.ConstructionCost))
        {
            cityStats.RemoveMoney(zoneAttributes.ConstructionCost);
            cityStats.AddPopulation(zoneAttributes.Population);
            cityStats.AddHappiness(zoneAttributes.Happiness);
            cityStats.AddZoneCount(zoneType);
            cityStats.AddPowerConsumption(zoneAttributes.PowerConsumption);
        }
        else
        {
            Debug.LogError("Not enough money to place this zone.");
        }
    }

    public void OnResidentialButtonClicked()
    {
        selectedZoneType = Zone.ZoneType.Residential;
        cursorManager.SetDefaultCursor();
        bulldozeMode = false;
        ClearSelectedBuilding();
    }

    public void OnCommercialButtonClicked()
    {
        selectedZoneType = Zone.ZoneType.Commercial;
        cursorManager.SetDefaultCursor();
        bulldozeMode = false;
        ClearSelectedBuilding();
    }

    public void OnIndustrialButtonClicked()
    {
        selectedZoneType = Zone.ZoneType.Industrial;
        cursorManager.SetDefaultCursor();
        bulldozeMode = false;
        ClearSelectedBuilding();
    }

    public void OnRoadButtonClicked()
    {
        selectedZoneType = Zone.ZoneType.Road;
        // cursorManager.SetRoadCursor();

        ClearSelectedBuilding();
        cursorManager.SetDefaultCursor();
        bulldozeMode = false;
    }

    public void OnGrassButtonClicked()
    {
        selectedZoneType = Zone.ZoneType.Grass;
        cursorManager.SetDefaultCursor();
        bulldozeMode = false;
        ClearSelectedBuilding();
    }

    public void OnPowerPlantAButtonClicked()
    {
        ClearSelectedZoneType();
        
        GameObject powerPlantObject = Instantiate(powerPlantAPrefab);

        PowerPlant powerPlant = powerPlantObject.AddComponent<PowerPlant>();

        powerPlant.Initialize("Power Plant A", 10000, 100, 50, 25, 3, 10000, powerPlantAPrefab);

        selectedBuilding = powerPlant;
        Debug.Log("Power Plant A button clicked. Selected building: " + selectedBuilding);

        cursorManager.SetDefaultCursor();
        bulldozeMode = false;
    }

    public Zone.ZoneType GetSelectedZoneType()
    {
        return selectedZoneType;
    }

    public IBuilding GetSelectedBuilding()
    {
        return selectedBuilding;
    }

    void ClearSelectedBuilding()
    {
        selectedBuilding = null;
    }

    void ClearSelectedZoneType()
    {
        selectedZoneType = Zone.ZoneType.Grass;
    }

    public void OnBulldozeButtonClicked()
    {
        ClearSelectedBuilding();
        ClearSelectedZoneType();
        cursorManager.SetBullDozerCursor();
        bulldozeMode = true;
        Debug.Log("Bulldoze button clicked. Bulldoze mode: " + bulldozeMode);
    }

    public bool isBulldozeMode()
    {
      return bulldozeMode;
    }
}