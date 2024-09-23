using System.Collections.Generic;
using UnityEngine;

public class CommercialZoningSelectorButton : MonoBehaviour
{
    public BuildingSelectorMenuController popupController; // Reference to the PopupController
    public List<BuildingSelectorMenuManager.ItemType> commercialZoningItems; // List of commercial zoning items
    public UIManager uiManager; // Reference to the UIManager

    public void OnCommercialZoningButtonClick()
    {
        popupController.ShowPopup(commercialZoningItems, OnCommercialZoningTypeSelected);
    }

    private void OnCommercialZoningTypeSelected(BuildingSelectorMenuManager.ItemType selectedItem)
    {
        switch (selectedItem.name)
        {
            case "Light":
                uiManager.OnLightCommercialButtonClicked();
                break;
            case "Medium":
                uiManager.OnMediumCommercialButtonClicked();
                break;
            case "Heavy":
                uiManager.OnHeavyCommercialButtonClicked();
                break;
            default:
                break;
        }
    }
}