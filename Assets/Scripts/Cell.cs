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
    public string prefabName;
    public bool isPivot;
    public PowerPlant powerPlant { get; set; }

    public Zone.ZoneType zoneType;

    public GameObject occupiedBuilding { get; set; }
    public GameObject prefab { get; set; }

    private string occupiedBuildingName;
    public int sortingOrder;
    public int height;

    void Start()
    {
        ZoneManager zoneManager = FindObjectOfType<ZoneManager>();
        GameObject grassPrefab = zoneManager.GetGrassPrefab();
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
        buildingType = "Grass";
        buildingSize = 1;
        powerPlant = null;
        prefab = grassPrefab;
        prefabName = grassPrefab.name;
        occupiedBuildingName = "";
        isPivot = false;
        height = 1;
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

    public GameObject GetCellPrefab()
    {
        return prefab;
    }

    public CellData GetCellData()
    {
        CellData cellData = new CellData
        {
            hasRoadAtLeft = hasRoadAtLeft,
            hasRoadAtTop = hasRoadAtTop,
            hasRoadAtRight = hasRoadAtRight,
            hasRoadAtBottom = hasRoadAtBottom,
            population = population,
            powerOutput = powerOutput,
            powerConsumption = powerConsumption,
            buildingType = buildingType,
            buildingSize = buildingSize,
            x = x,
            y = y,
            happiness = happiness,
            prefabName = prefabName,
            zoneType = zoneType.ToString(),
            occupiedBuildingName = occupiedBuilding != null ? occupiedBuilding.name : "",
            isPivot = isPivot,
            sortingOrder = sortingOrder,
            height = height
        };

        return cellData;
    }

    public void SetCellData(CellData cellData)
    {
        hasRoadAtLeft = cellData.hasRoadAtLeft;
        hasRoadAtTop = cellData.hasRoadAtTop;
        hasRoadAtRight = cellData.hasRoadAtRight;
        hasRoadAtBottom = cellData.hasRoadAtBottom;
        population = cellData.population;
        powerOutput = cellData.powerOutput;
        powerConsumption = cellData.powerConsumption;
        buildingType = cellData.buildingType;
        buildingSize = cellData.buildingSize;
        x = cellData.x;
        y = cellData.y;
        happiness = cellData.happiness;
        prefabName = cellData.prefabName;
        zoneType = (Zone.ZoneType)System.Enum.Parse(typeof(Zone.ZoneType), cellData.zoneType);
        occupiedBuildingName = cellData.occupiedBuildingName;
        isPivot = cellData.isPivot;
        height = cellData.height;
    }

    public int GetSortingOrder()
    {
        return sortingOrder;
    }
}