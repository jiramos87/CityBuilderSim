using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public CityStats cityStats;
    public CursorManager cursorManager;
    public GridManager gridManager;

    public TimeManager timeManager;

    public EconomyManager economyManager;

    public Text populationText;
    public Text moneyText;
    public Text happinessText;
    public Text gridCoordinatesText;
    public Text cityPowerOutputText;
    public Text cityPowerConsumptionText;

    public Text dateText;

    public Text residentialTaxText;
    public Text commercialTaxText;
    public Text industrialTaxText;

    public Text buttonMoneyText;
    public Text detailsNameText;
    public Text detailsOccupancyText;
    public Text detailsHappinessText;
    public Text detailsPowerOutputText;
    public Text detailsPowerConsumptionText;
    public Text detailsDateBuiltText;
    public Text detailsBuildingTypeText;

    public Image detailsImage;

    private Zone.ZoneType selectedZoneType;

    private IBuilding selectedBuilding;

    public GameObject powerPlantAPrefab;
    public DetailsPopupController detailsPopupController;

    public bool bulldozeMode;
    public bool detailsMode;

    public GameManager gameManager;

    public string saveName;

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

    public void UpdateUI()
    {
        populationText.text = "Population: " + cityStats.population;
        moneyText.text = "Money: $" + cityStats.money;
        buttonMoneyText.text = "Money: $" + cityStats.money;
        happinessText.text = "Happiness: " + cityStats.happiness;
        cityPowerOutputText.text = "City Power Output: " + cityStats.cityPowerOutput + " MW";
        cityPowerConsumptionText.text = "City Power Consumption: " + cityStats.cityPowerConsumption + " MW";
        dateText.text = "Date: " + timeManager.GetCurrentDate();
        residentialTaxText.text = "Residential Tax: " + economyManager.GetResidentialTax() + "%";
        commercialTaxText.text = "Commercial Tax: " + economyManager.GetCommercialTax() + "%";
        industrialTaxText.text = "Industrial Tax: " + economyManager.GetIndustrialTax() + "%";
        gridCoordinatesText.text = "Grid Coordinates: " + "x: " + gridManager.mouseGridPosition.x + ", y: " + gridManager.mouseGridPosition.y;
    }

    public void OnLightResidentialButtonClicked()
    {
        selectedZoneType = Zone.ZoneType.ResidentialLightZoning;
        cursorManager.SetDefaultCursor();
        bulldozeMode = false;
        ClearSelectedBuilding();
    }

    public void OnMediumResidentialButtonClicked()
    {
        selectedZoneType = Zone.ZoneType.ResidentialMediumZoning;
        cursorManager.SetDefaultCursor();
        bulldozeMode = false;
        ClearSelectedBuilding();
    }

    public void OnHeavyResidentialButtonClicked()
    {
        selectedZoneType = Zone.ZoneType.ResidentialHeavyZoning;
        cursorManager.SetDefaultCursor();
        bulldozeMode = false;
        ClearSelectedBuilding();
    }

    public void OnLightCommercialButtonClicked()
    {
        selectedZoneType = Zone.ZoneType.CommercialLightZoning;
        cursorManager.SetDefaultCursor();
        bulldozeMode = false;
        ClearSelectedBuilding();
    }

    public void OnMediumCommercialButtonClicked()
    {
        selectedZoneType = Zone.ZoneType.CommercialMediumZoning;
        cursorManager.SetDefaultCursor();
        bulldozeMode = false;
        ClearSelectedBuilding();
    }

    public void OnHeavyCommercialButtonClicked()
    {
        selectedZoneType = Zone.ZoneType.CommercialHeavyZoning;
        cursorManager.SetDefaultCursor();
        bulldozeMode = false;
        ClearSelectedBuilding();
    }

    public void OnLightIndustrialButtonClicked()
    {
        selectedZoneType = Zone.ZoneType.IndustrialLightZoning;
        cursorManager.SetDefaultCursor();
        bulldozeMode = false;
        ClearSelectedBuilding();
    }

    public void OnMediumIndustrialButtonClicked()
    {
        selectedZoneType = Zone.ZoneType.IndustrialMediumZoning;
        cursorManager.SetDefaultCursor();
        bulldozeMode = false;
        ClearSelectedBuilding();
    }

    public void OnHeavyIndustrialButtonClicked()
    {
        selectedZoneType = Zone.ZoneType.IndustrialHeavyZoning;
        cursorManager.SetDefaultCursor();
        bulldozeMode = false;
        ClearSelectedBuilding();
    }

    public void OnTwoWayRoadButtonClicked()
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

    public void OnNuclearPowerPlantButtonClicked()
    {
        ClearSelectedZoneType();
        
        GameObject powerPlantObject = Instantiate(powerPlantAPrefab);

        PowerPlant powerPlant = powerPlantObject.AddComponent<PowerPlant>();

        powerPlant.Initialize("Power Plant A", 10000, 100, 50, 25, 3, 10000, powerPlantAPrefab);

        selectedBuilding = powerPlant;

        cursorManager.SetDefaultCursor();

        cursorManager.ShowBuildingPreview(powerPlantAPrefab);

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
    }

    public bool isBulldozeMode()
    {
      return bulldozeMode;
    }

    public void OnDetailsButtonClicked()
    {
        cursorManager.SetDetailsCursor();
        detailsMode = !detailsMode;
    }

    public void OnRaiseResidentialTaxButtonClicked()
    {
        economyManager.RaiseResidentialTax();
    }

    public void OnLowerResidentialTaxButtonClicked()
    {
        economyManager.LowerResidentialTax();
    }

    public void OnRaiseCommercialTaxButtonClicked()
    {
        economyManager.RaiseCommercialTax();
    }

    public void OnLowerCommercialTaxButtonClicked()
    {
        economyManager.LowerCommercialTax();
    }

    public void OnRaiseIndustrialTaxButtonClicked()
    {
        economyManager.RaiseIndustrialTax();
    }

    public void OnLowerIndustrialTaxButtonClicked()
    {
        economyManager.LowerIndustrialTax();
    }

    public void ShowTileDetails(Cell cell)
    {
        detailsPopupController.ShowDetails();
        detailsNameText.text = cell.GetBuildingName();
        detailsOccupancyText.text = "Occupancy: " + cell.GetPopulation();
        detailsHappinessText.text = "Happiness: " + cell.GetHappiness();
        detailsPowerOutputText.text = "Power Output: " + cell.GetPowerOutput() + " MW";
        detailsPowerConsumptionText.text = "Power Consumption: " + cell.GetPowerConsumption() + " MW";
        // detailsDateBuiltText.text = "Date Built: " + timeManager.GetCurrentDate();
        detailsBuildingTypeText.text = "Building Type: " + cell.GetBuildingType();
        detailsImage.sprite = cell.GetCellPrefab().GetComponent<SpriteRenderer>().sprite;
    }

    public bool IsDetailsMode()
    {
        return detailsMode;
    }

    public void OnSaveGameButtonClicked()
    {
        gameManager.SaveGame(saveName);
    }

    public void OnLoadGameButtonClicked(string saveFilePath)
    {
        gameManager.LoadGame(saveFilePath);
    }
}