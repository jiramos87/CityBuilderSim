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

    public string cityName;

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
        cityName = "City";
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

    public void RemoveResidentialZoneCount()
    {
        residentialZoneCount--;
    }

    public void AddResidentialBuildingCount()
    {
        residentialBuildingCount++;
    }

    public void RemoveResidentialBuildingCount()
    {
        residentialBuildingCount--;
    }

    public void AddCommercialZoneCount()
    {
        commercialZoneCount++;
    }

    public void RemoveCommercialZoneCount()
    {
        commercialZoneCount--;
    }

    public void AddCommercialBuildingCount()
    {
        commercialBuildingCount++;
    }

    public void RemoveCommercialBuildingCount()
    {
        commercialBuildingCount--;
    }

    public void AddIndustrialZoneCount()
    {
        industrialZoneCount++;
    }

    public void RemoveIndustrialZoneCount()
    {
        industrialZoneCount--;
    }

    public void AddIndustrialBuildingCount()
    {
        industrialBuildingCount++;
    }

    public void RemoveIndustrialBuildingCount()
    {
        industrialBuildingCount--;
    }

    public void AddResidentialLightBuildingCount()
    {
        residentialLightBuildingCount++;
        AddResidentialBuildingCount();
    }

    public void RemoveResidentialLightBuildingCount()
    {
        residentialLightBuildingCount--;
        RemoveResidentialBuildingCount();
    }

    public void AddResidentialLightZoningCount()
    {
        residentialLightZoningCount++;
        AddResidentialZoneCount();
    }

    public void RemoveResidentialLightZoningCount()
    {
        residentialLightZoningCount--;
        RemoveResidentialZoneCount();
    }

    public void AddResidentialMediumBuildingCount()
    {
        residentialMediumBuildingCount++;
        AddResidentialBuildingCount();
    }

    public void RemoveResidentialMediumBuildingCount()
    {
        residentialMediumBuildingCount--;
        RemoveResidentialBuildingCount();
    }

    public void AddResidentialMediumZoningCount()
    {
        residentialMediumZoningCount++;
        AddResidentialZoneCount();
    }

    public void RemoveResidentialMediumZoningCount()
    {
        residentialMediumZoningCount--;
        RemoveResidentialZoneCount();
    }

    public void AddResidentialHeavyBuildingCount()
    {
        residentialHeavyBuildingCount++;
        AddResidentialBuildingCount();
    }

    public void RemoveResidentialHeavyBuildingCount()
    {
        residentialHeavyBuildingCount--;
        RemoveResidentialBuildingCount();
    }

    public void AddResidentialHeavyZoningCount()
    {
        residentialHeavyZoningCount++;
        AddResidentialZoneCount();
    }

    public void RemoveResidentialHeavyZoningCount()
    {
        residentialHeavyZoningCount--;
        RemoveResidentialZoneCount();
    }

    public void AddCommercialLightBuildingCount()
    {
        commercialLightBuildingCount++;
        AddCommercialBuildingCount();
    }

    public void RemoveCommercialLightBuildingCount()
    {
        commercialLightBuildingCount--;
        RemoveCommercialBuildingCount();
    }

    public void AddCommercialLightZoningCount()
    {
        commercialLightZoningCount++;
        AddCommercialZoneCount();
    }

    public void RemoveCommercialLightZoningCount()
    {
        commercialLightZoningCount--;
        RemoveCommercialZoneCount();
    }

    public void AddCommercialMediumBuildingCount()
    {
        commercialMediumBuildingCount++;
        AddCommercialBuildingCount();
    }

    public void RemoveCommercialMediumBuildingCount()
    {
        commercialMediumBuildingCount--;
        RemoveCommercialBuildingCount();
    }

    public void AddCommercialMediumZoningCount()
    {
        commercialMediumZoningCount++;
        AddCommercialZoneCount();
    }

    public void RemoveCommercialMediumZoningCount()
    {
        commercialMediumZoningCount--;
        RemoveCommercialZoneCount();
    }

    public void AddCommercialHeavyBuildingCount()
    {
        commercialHeavyBuildingCount++;
        AddCommercialBuildingCount();
    }

    public void RemoveCommercialHeavyBuildingCount()
    {
        commercialHeavyBuildingCount--;
        RemoveCommercialBuildingCount();
    }

    public void AddCommercialHeavyZoningCount()
    {
        commercialHeavyZoningCount++;
        AddCommercialZoneCount();
    }

    public void RemoveCommercialHeavyZoningCount()
    {
        commercialHeavyZoningCount--;
        RemoveCommercialZoneCount();
    }

    public void AddIndustrialLightBuildingCount()
    {
        industrialLightBuildingCount++;
        AddIndustrialBuildingCount();
    }

    public void RemoveIndustrialLightBuildingCount()
    {
        industrialLightBuildingCount--;
        RemoveIndustrialBuildingCount();
    }

    public void AddIndustrialLightZoningCount()
    {
        industrialLightZoningCount++;
        AddIndustrialZoneCount();
    }

    public void RemoveIndustrialLightZoningCount()
    {
        industrialLightZoningCount--;
        RemoveIndustrialZoneCount();
    }

    public void AddIndustrialMediumBuildingCount()
    {
        industrialMediumBuildingCount++;
        AddIndustrialBuildingCount();
    }

    public void RemoveIndustrialMediumBuildingCount()
    {
        industrialMediumBuildingCount--;
        RemoveIndustrialBuildingCount();
    }

    public void AddIndustrialMediumZoningCount()
    {
        industrialMediumZoningCount++;
        AddIndustrialZoneCount();
    }

    public void RemoveIndustrialMediumZoningCount()
    {
        industrialMediumZoningCount--;
        RemoveIndustrialZoneCount();
    }

    public void AddIndustrialHeavyBuildingCount()
    {
        industrialHeavyBuildingCount++;
        AddIndustrialBuildingCount();
    }

    public void RemoveIndustrialHeavyBuildingCount()
    {
        industrialHeavyBuildingCount--;
        RemoveIndustrialBuildingCount();
    }

    public void AddIndustrialHeavyZoningCount()
    {
        industrialHeavyZoningCount++;
        AddIndustrialZoneCount();
    }

    public void RemoveIndustrialHeavyZoningCount()
    {
        industrialHeavyZoningCount--;
        RemoveIndustrialZoneCount();
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

    public void RemoveZoneBuildingCount(Zone.ZoneType zoneType)
    {
        switch (zoneType)
        {
            case Zone.ZoneType.ResidentialLightBuilding:
                RemoveResidentialLightBuildingCount();
                break;
            case Zone.ZoneType.ResidentialLightZoning:
                RemoveResidentialLightZoningCount();
                break;
            case Zone.ZoneType.ResidentialMediumBuilding:
                RemoveResidentialMediumBuildingCount();
                break;
            case Zone.ZoneType.ResidentialMediumZoning:
                RemoveResidentialMediumZoningCount();
                break;
            case Zone.ZoneType.ResidentialHeavyBuilding:
                RemoveResidentialHeavyBuildingCount();
                break;
            case Zone.ZoneType.ResidentialHeavyZoning:
                RemoveResidentialHeavyZoningCount();
                break;
            case Zone.ZoneType.CommercialLightBuilding:
                RemoveCommercialLightBuildingCount();
                break;
            case Zone.ZoneType.CommercialLightZoning:
                RemoveCommercialLightZoningCount();
                break;
            case Zone.ZoneType.CommercialMediumBuilding:
                RemoveCommercialMediumBuildingCount();
                break;
            case Zone.ZoneType.CommercialMediumZoning:
                RemoveCommercialMediumZoningCount();
                break;
            case Zone.ZoneType.CommercialHeavyBuilding:
                RemoveCommercialHeavyBuildingCount();
                break;
            case Zone.ZoneType.CommercialHeavyZoning:
                RemoveCommercialHeavyZoningCount();
                break;
            case Zone.ZoneType.IndustrialLightBuilding:
                RemoveIndustrialLightBuildingCount();
                break;
            case Zone.ZoneType.IndustrialLightZoning:
                RemoveIndustrialLightZoningCount();
                break;
            case Zone.ZoneType.IndustrialMediumBuilding:
                RemoveIndustrialMediumBuildingCount();
                break;
            case Zone.ZoneType.IndustrialMediumZoning:
                RemoveIndustrialMediumZoningCount();
                break;
            case Zone.ZoneType.IndustrialHeavyBuilding:
                RemoveIndustrialHeavyBuildingCount();
                break;
            case Zone.ZoneType.IndustrialHeavyZoning:
                RemoveIndustrialHeavyZoningCount();
                break;
            case Zone.ZoneType.Road:
                roadCount--;
                break;
            case Zone.ZoneType.Grass:
                grassCount--;
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

    public void UnregisterPowerPlant(PowerPlant powerPlant)
    {
        powerPlants.Remove(powerPlant);

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
        return cityPowerOutput > cityPowerConsumption;
    }

    public void HandleZoneBuildingPlacement(Zone.ZoneType zoneType, ZoneAttributes zoneAttributes)
    {
        RemoveMoney(zoneAttributes.ConstructionCost);
        AddPopulation(zoneAttributes.Population);
        AddHappiness(zoneAttributes.Happiness);
        AddZoneBuildingCount(zoneType);
        AddPowerConsumption(zoneAttributes.PowerConsumption);
    }

    public void HandleBuildingDemolition(Zone.ZoneType zoneType, ZoneAttributes zoneAttributes)
    {
        AddMoney(zoneAttributes.ConstructionCost / 5);
        AddPopulation(-zoneAttributes.Population);
        AddHappiness(-zoneAttributes.Happiness);
        RemoveZoneBuildingCount(zoneType);
        RemovePowerConsumption(zoneAttributes.PowerConsumption);
    }

    public CityStatsData GetCityStatsData()
    {
        CityStatsData cityStatsData = new CityStatsData
        {
            currentDate = currentDate,
            population = population,
            money = money,
            happiness = happiness,
            residentialZoneCount = residentialZoneCount,
            residentialBuildingCount = residentialBuildingCount,
            commercialZoneCount = commercialZoneCount,
            commercialBuildingCount = commercialBuildingCount,
            industrialZoneCount = industrialZoneCount,
            industrialBuildingCount = industrialBuildingCount,
            residentialLightBuildingCount = residentialLightBuildingCount,
            residentialLightZoningCount = residentialLightZoningCount,
            residentialMediumBuildingCount = residentialMediumBuildingCount,
            residentialMediumZoningCount = residentialMediumZoningCount,
            residentialHeavyBuildingCount = residentialHeavyBuildingCount,
            residentialHeavyZoningCount = residentialHeavyZoningCount,
            commercialLightBuildingCount = commercialLightBuildingCount,
            commercialLightZoningCount = commercialLightZoningCount,
            commercialMediumBuildingCount = commercialMediumBuildingCount,
            commercialMediumZoningCount = commercialMediumZoningCount,
            commercialHeavyBuildingCount = commercialHeavyBuildingCount,
            commercialHeavyZoningCount = commercialHeavyZoningCount,
            industrialLightBuildingCount = industrialLightBuildingCount,
            industrialLightZoningCount = industrialLightZoningCount,
            industrialMediumBuildingCount = industrialMediumBuildingCount,
            industrialMediumZoningCount = industrialMediumZoningCount,
            industrialHeavyBuildingCount = industrialHeavyBuildingCount,
            industrialHeavyZoningCount = industrialHeavyZoningCount,
            roadCount = roadCount,
            grassCount = grassCount,
            cityPowerConsumption = cityPowerConsumption,
            cityPowerOutput = cityPowerOutput,
            cityName = cityName
        };

        return cityStatsData;
    }
}

[System.Serializable]
public struct CityStatsData
{
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
    public string cityName;
}
