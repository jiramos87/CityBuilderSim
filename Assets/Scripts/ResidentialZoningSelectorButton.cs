using System.Collections.Generic;
using UnityEngine;

public class ResidentialZoningSelectorButton : MonoBehaviour
{
    public BuildingSelectorMenuController popupController; // Reference to the PopupController
    public List<BuildingSelectorMenuManager.ItemType> residentialZoningItems; // List of residential zoning items
    public UIManager uiManager; // Reference to the UIManager

    public void OnResidentialZoningButtonClick()
    {
        Debug.Log("Residential zoning button clicked");
        popupController.ShowPopup(residentialZoningItems, OnResidentialZoningTypeSelected);
    }

    private void OnResidentialZoningTypeSelected(BuildingSelectorMenuManager.ItemType selectedItem)
    {
        Debug.Log("Selected residential zoning type: " + selectedItem.name);
        switch (selectedItem.name)
        {
            case "Light":
                Debug.Log("Low density residential zoning selected");
                uiManager.OnLightResidentialButtonClicked();
                break;
            case "Medium":
                Debug.Log("High density residential zoning selected");
                uiManager.OnMediumResidentialButtonClicked();
                break;
            case "Heavy":
                Debug.Log("Heavy residential zoning selected");
                uiManager.OnHeavyResidentialButtonClicked();
                break;
            default:
                Debug.Log("Unknown residential zoning selected");
                break;
        }
    }
}