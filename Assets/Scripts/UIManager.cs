using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public CityStats cityStats;
    public GridManager gridManager;

    public Text populationText;
    public Text moneyText;
    public Text happinessText;

    public Text gridCoordinatesText;

    private Zone.ZoneType selectedZoneType;


    void Start()
    {
        if (cityStats == null)
        {
            Debug.LogError("CityStats component not found.");
        }

        selectedZoneType = Zone.ZoneType.Grass;
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
        gridCoordinatesText.text = "Grid Coordinates: " + "x: " + gridManager.mouseGridPosition.x + ", y: " + gridManager.mouseGridPosition.y;
    }

    public void OnTileClicked(Zone.ZoneType zoneType, ZoneAttributes zoneAttributes)
    {
        if (cityStats.CanAfford(zoneAttributes.Cost))
        {
            cityStats.AddMoney(zoneAttributes.Money);
            cityStats.AddPopulation(zoneAttributes.Population);
            cityStats.AddHappiness(zoneAttributes.Happiness);
            cityStats.AddZoneCount(zoneType);
            cityStats.RemoveMoney(zoneAttributes.Cost);
        }
        else
        {
            Debug.LogError("Not enough money to place this zone.");
        }
    }

    public void OnResidentialButtonClicked()
    {
        selectedZoneType = Zone.ZoneType.Residential;
        var zoneAttributes = new ZoneAttributes(-100, 10, 5, 100);
    }

    public void OnCommercialButtonClicked()
    {
        selectedZoneType = Zone.ZoneType.Commercial;
        var zoneAttributes = new ZoneAttributes(-200, 20, 10, 200);
    }

    public void OnIndustrialButtonClicked()
    {
        selectedZoneType = Zone.ZoneType.Industrial;
        var zoneAttributes = new ZoneAttributes(-300, 30, 15, 300);
    }

    public void OnRoadButtonClicked()
    {
        selectedZoneType = Zone.ZoneType.Road;
        var zoneAttributes = new ZoneAttributes(-50, 0, 0, 50);
    }

    public void OnGrassButtonClicked()
    {
        selectedZoneType = Zone.ZoneType.Grass;
        var zoneAttributes = new ZoneAttributes(1, 0, 1, 0);
    }

    public Zone.ZoneType GetSelectedZoneType()
    {
        return selectedZoneType;
    } 
}