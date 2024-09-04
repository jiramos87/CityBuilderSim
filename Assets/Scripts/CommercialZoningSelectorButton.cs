using System.Collections.Generic;
using UnityEngine;

public class CommercialZoningSelectorButton : MonoBehaviour
{
    public BuildingSelectorMenuController popupController; // Reference to the PopupController
    public List<BuildingSelectorMenuManager.ItemType> commercialZoningItems; // List of commercial zoning items
    public UIManager uiManager; // Reference to the UIManager

    public void OnCommercialZoningButtonClick()
    {
        Debug.Log("Commercial zoning button clicked");
        popupController.ShowPopup(commercialZoningItems, OnCommercialZoningTypeSelected);
    }

    private void OnCommercialZoningTypeSelected(BuildingSelectorMenuManager.ItemType selectedItem)
    {
        Debug.Log("Selected commercial zoning type: " + selectedItem.name);
        switch (selectedItem.name)
        {
            case "Light":
                Debug.Log("Low density commercial zoning selected");
                uiManager.OnLightCommercialButtonClicked();
                break;
            case "Medium":
                Debug.Log("High density commercial zoning selected");
                uiManager.OnMediumCommercialButtonClicked();
                break;
            case "Heavy":
                Debug.Log("Heavy commercial zoning selected");
                uiManager.OnHeavyCommercialButtonClicked();
                break;
            default:
                Debug.Log("Unknown commercial zoning type selected");
                break;
        }
    }
}