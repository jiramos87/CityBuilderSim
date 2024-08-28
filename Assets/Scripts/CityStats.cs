using UnityEngine;
using System.Collections.Generic;

public class CityStats : MonoBehaviour
{
    public TimeManager timeManager;

    public System.DateTime currentDate;

    public int population;
    public int money;

    public int happiness;
    public int residentialZoneCount;

    public int residentialBuildingCount;
    public int commercialZoneCount;
    public int commercialBuildingCount;
    public int industrialZoneCount;
    public int industrialBuildingCount;

    public int residentialLightBuildingCount;
    public int residentialLightZoningCount;
    public int residentialMediumBuildingCount;
    public int residentialMediumZoningCount;
    public int residentialHeavyBuildingCount;
    public int residentialHeavyZoningCount;

    public int commercialLightBuildingCount;
    public int commercialLightZoningCount;
    public int commercialMediumBuildingCount;
    public int commercialMediumZoningCount;
    public int commercialHeavyBuildingCount;
    public int commercialHeavyZoningCount;

    public int industrialLightBuildingCount;
    public int industrialLightZoningCount;
    public int industrialMediumBuildingCount;
    public int industrialMediumZoningCount;
    public int industrialHeavyBuildingCount;
    public int industrialHeavyZoningCount;

    public int roadCount;
    public int grassCount;

    public int cityPowerConsumption;
    public int cityPowerOutput;

    private List<PowerPlant> powerPlants = new List<PowerPlant>();

    void Start()
    {
        population = 0;
        money = 20000;
        residentialZoneCount = 0;
        commercialZoneCount = 0;
        industrialZoneCount = 0;
        roadCount = 0;
        grassCount = 0;
        cityPowerConsumption = 0;
        cityPowerOutput = 0;
    }

    public void AddPopulation(int value)
    {
        population += value;
    }

    public void AddMoney(int value)
    {
        money += value;
    }

    public void RemoveMoney(int value)
    {
        money -= value;
    }

    public void AddHappiness(int value)
    {
        happiness += value;
    }

    public void AddResidentialZoneCount()
    {
        residentialZoneCount++;
    }

    public void AddResidentialBuildingCount()
    {
        residentialBuildingCount++;
    }

    public void AddCommercialZoneCount()
    {
        commercialZoneCount++;
    }

    public void AddCommercialBuildingCount()
    {
        commercialBuildingCount++;
    }

    public void AddIndustrialZoneCount()
    {
        industrialZoneCount++;
    }

    public void AddIndustrialBuildingCount()
    {
        industrialBuildingCount++;
    }

    public void AddResidentialLightBuildingCount()
    {
        residentialLightBuildingCount++;
        AddResidentialBuildingCount();
    }

    public void AddResidentialLightZoningCount()
    {
        residentialLightZoningCount++;
        AddResidentialZoneCount();
    }

    public void AddResidentialMediumBuildingCount()
    {
        residentialMediumBuildingCount++;
        AddResidentialBuildingCount();
    }

    public void AddResidentialMediumZoningCount()
    {
        residentialMediumZoningCount++;
        AddResidentialZoneCount();
    }

    public void AddResidentialHeavyBuildingCount()
    {
        residentialHeavyBuildingCount++;
        AddResidentialBuildingCount();
    }

    public void AddResidentialHeavyZoningCount()
    {
        residentialHeavyZoningCount++;
        AddResidentialZoneCount();
    }

    public void AddCommercialLightBuildingCount()
    {
        commercialLightBuildingCount++;
        AddCommercialBuildingCount();
    }

    public void AddCommercialLightZoningCount()
    {
        commercialLightZoningCount++;
        AddCommercialZoneCount();
    }

    public void AddCommercialMediumBuildingCount()
    {
        commercialMediumBuildingCount++;
        AddCommercialBuildingCount();
    }

    public void AddCommercialMediumZoningCount()
    {
        commercialMediumZoningCount++;
        AddCommercialZoneCount();
    }

    public void AddCommercialHeavyBuildingCount()
    {
        commercialHeavyBuildingCount++;
        AddCommercialBuildingCount();
    }

    public void AddCommercialHeavyZoningCount()
    {
        commercialHeavyZoningCount++;
        AddCommercialZoneCount();
    }

    public void AddIndustrialLightBuildingCount()
    {
        industrialLightBuildingCount++;
        AddIndustrialBuildingCount();
    }

    public void AddIndustrialLightZoningCount()
    {
        industrialLightZoningCount++;
        AddIndustrialZoneCount();
    }

    public void AddIndustrialMediumBuildingCount()
    {
        industrialMediumBuildingCount++;
        AddIndustrialBuildingCount();
    }

    public void AddIndustrialMediumZoningCount()
    {
        industrialMediumZoningCount++;
        AddIndustrialZoneCount();
    }

    public void AddIndustrialHeavyBuildingCount()
    {
        industrialHeavyBuildingCount++;
        AddIndustrialBuildingCount();
    }

    public void AddIndustrialHeavyZoningCount()
    {
        industrialHeavyZoningCount++;
        AddIndustrialZoneCount();
    }
    public void AddRoadCount()
    {
        roadCount++;
    }

    public void AddGrassCount()
    {
        grassCount++;
    }


    public void AddZoneBuildingCount(Zone.ZoneType zoneType)
    {
        switch (zoneType)
        {
            case Zone.ZoneType.ResidentialLightBuilding:
                AddResidentialLightBuildingCount();
                break;
            case Zone.ZoneType.ResidentialLightZoning:
                AddResidentialLightZoningCount();
                break;
            case Zone.ZoneType.ResidentialMediumBuilding:
                AddResidentialMediumBuildingCount();
                break;
            case Zone.ZoneType.ResidentialMediumZoning:
                AddResidentialMediumZoningCount();
                break;
            case Zone.ZoneType.ResidentialHeavyBuilding:
                AddResidentialHeavyBuildingCount();
                break;
            case Zone.ZoneType.ResidentialHeavyZoning:
                AddResidentialHeavyZoningCount();
                break;
            case Zone.ZoneType.CommercialLightBuilding:
                AddCommercialLightBuildingCount();
                break;
            case Zone.ZoneType.CommercialLightZoning:
                AddCommercialLightZoningCount();
                break;
            case Zone.ZoneType.CommercialMediumBuilding:
                AddCommercialMediumBuildingCount();
                break;
            case Zone.ZoneType.CommercialMediumZoning:
                AddCommercialMediumZoningCount();
                break;
            case Zone.ZoneType.CommercialHeavyBuilding:
                AddCommercialHeavyBuildingCount();
                break;
            case Zone.ZoneType.CommercialHeavyZoning:
                AddCommercialHeavyZoningCount();
                break;
            case Zone.ZoneType.IndustrialLightBuilding:
                AddIndustrialLightBuildingCount();
                break;
            case Zone.ZoneType.IndustrialLightZoning:
                AddIndustrialLightZoningCount();
                break;
            case Zone.ZoneType.IndustrialMediumBuilding:
                AddIndustrialMediumBuildingCount();
                break;
            case Zone.ZoneType.IndustrialMediumZoning:
                AddIndustrialMediumZoningCount();
                break;
            case Zone.ZoneType.IndustrialHeavyBuilding:
                AddIndustrialHeavyBuildingCount();
                break;
            case Zone.ZoneType.IndustrialHeavyZoning:
                AddIndustrialHeavyZoningCount();
                break;
            case Zone.ZoneType.Road:
                AddRoadCount();
                break;
            case Zone.ZoneType.Grass:
                AddGrassCount();
                break;
        }
    }

    public bool CanAfford(int cost)
    {
        return money >= cost;
    }

    public void RegisterPowerPlant(PowerPlant powerPlant)
    {
        powerPlants.Add(powerPlant);

        int totalPowerOutput = 0;
        foreach (var plant in powerPlants)
        {
            totalPowerOutput += plant.PowerOutput;
        }
        
        cityPowerOutput = totalPowerOutput;
    }

    public int GetTotalPowerOutput()
    {
        return cityPowerOutput;
    }

    public void AddPowerConsumption(int value)
    {
        cityPowerConsumption += value;
    }

    public void RemovePowerConsumption(int value)
    {
        cityPowerConsumption -= value;
    }

    public int GetTotalPowerConsumption()
    {
        return cityPowerConsumption;
    }

    public void PerformMonthlyUpdates()
    {
        
    }

    public void PerformDailyUpdates()
    {
        currentDate = timeManager.GetCurrentDate();
    }

    public bool GetCityPowerAvailability()
    {
        return cityPowerOutput >= cityPowerConsumption;
    }
}