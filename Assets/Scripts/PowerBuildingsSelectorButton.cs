using System.Collections.Generic;
using UnityEngine;

public class PowerBuildingsSelectButton : MonoBehaviour
{
    public BuildingSelectorMenuController popupController;
    public List<BuildingSelectorMenuManager.ItemType> powerBuildingItems;
    public UIManager uiManager;
    public void OnPowerBuildingsButtonClick()
    {
        popupController.ShowPopup(powerBuildingItems, OnPowerBuildingSelected);
    }

    private void OnPowerBuildingSelected(BuildingSelectorMenuManager.ItemType selectedItem)
    {
        switch (selectedItem.name)
        {
            case "Nuclear plant":
                uiManager.OnNuclearPowerPlantButtonClicked();
                break;
            default:
                break;
        }
    }
}
