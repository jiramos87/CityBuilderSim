using System.Collections.Generic;
using UnityEngine;

public class PowerBuildingsSelectButton : MonoBehaviour
{
    public BuildingSelectorMenuController popupController; // Reference to the PopupController
    public List<BuildingSelectorMenuManager.ItemType> powerBuildingItems; // List of power building items
    public UIManager uiManager; // Reference to the UIManager
    public void OnPowerBuildingsButtonClick()
    {
        popupController.ShowPopup(powerBuildingItems, OnPowerBuildingSelected);
    }

    private void OnPowerBuildingSelected(BuildingSelectorMenuManager.ItemType selectedItem)
    {
        Debug.Log("Selected power building: " + selectedItem.name);
        switch (selectedItem.name)
        {
            case "Nuclear plant":
                Debug.Log("Nuclear plant selected");
                uiManager.OnNuclearPowerPlantButtonClicked();
                break;
            default:
                Debug.Log("Unknown power building selected");
                break;
        }
    }
}
