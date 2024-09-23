using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool hasRoadAtLeft;
    public bool hasRoadAtTop;
    public bool hasRoadAtRight;
    public bool hasRoadAtBottom;
    public int population;
    public int powerOutput;
    public int powerConsumption;
    public string buildingType;
    public int buildingSize;
    public int x;
    public int y;
    public int happiness;
    public PowerPlant powerPlant { get; set; }

    public Zone.ZoneType zoneType;

    public GameObject occupiedBuilding { get; set; }

    void Start()
    {
        // Initialize default values
        zoneType = Zone.ZoneType.Grass;
        hasRoadAtLeft = false;
        hasRoadAtTop = false;
        hasRoadAtRight = false;
        hasRoadAtBottom = false;
        population = 0;
        powerOutput = 0;
        powerConsumption = 0;
        happiness = 0;
        buildingType = "";
        buildingSize = 1;
        powerPlant = null;
    }

    public string GetBuildingType()
    {
        return buildingType;
    }

    public int GetBuildingSize()
    {
        return buildingSize;
    }

    public int GetPopulation()
    {
        return population;
    }

    public int GetPowerOutput()
    {
        return powerOutput;
    }

    public int GetPowerConsumption()
    {
        return powerConsumption;
    }

    public int GetHappiness()
    {
        return happiness;
    }

    public Zone.ZoneType GetZoneType()
    {
        return zoneType;
    }

    public string GetBuildingName()
    {
        if (occupiedBuilding != null)
        {
            return occupiedBuilding.name;
        }
        return "";
    }
}