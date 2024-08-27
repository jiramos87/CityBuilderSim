using UnityEngine;
using System.Collections.Generic;

public class CityStats : MonoBehaviour
{
    public int population;
    public int money;

    public int happiness;
    public int residentialZoneCount;
    public int commercialZoneCount;
    public int industrialZoneCount;
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

    public void AddCommercialZoneCount()
    {
        commercialZoneCount++;
    }

    public void AddIndustrialZoneCount()
    {
        industrialZoneCount++;
    }


    public void AddRoadCount()
    {
        roadCount++;
    }

    public void AddGrassCount()
    {
        grassCount++;
    }


    public void AddZoneCount(Zone.ZoneType zoneType)
    {
        switch (zoneType)
        {
            case Zone.ZoneType.Residential:
                AddResidentialZoneCount();
                break;
            case Zone.ZoneType.Commercial:
                AddCommercialZoneCount();
                break;
            case Zone.ZoneType.Industrial:
                AddIndustrialZoneCount();
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
}