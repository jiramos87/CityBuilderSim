using System.Collections.Generic;
using UnityEngine;

public class IndustrialZoningSelectorButton : MonoBehaviour
{
    public BuildingSelectorMenuController popupController; // Reference to the PopupController
    public List<BuildingSelectorMenuManager.ItemType> industrialZoningItems; // List of industrial zoning items
    public UIManager uiManager; // Reference to the UIManager

    public void OnIndustrialZoningButtonClick()
    {
        Debug.Log("Industrial zoning button clicked");
        popupController.ShowPopup(industrialZoningItems, OnIndustrialZoningSelected);
    }

    private void OnIndustrialZoningSelected(BuildingSelectorMenuManager.ItemType selectedItem)
    {
        Debug.Log("Selected industrial zoning: " + selectedItem.name);
        switch (selectedItem.name)
        {
            case "Light":
                Debug.Log("Low density industrial zoning selected");
                uiManager.OnLightIndustrialButtonClicked();
                break;
            case "Medium":
                Debug.Log("High density industrial zoning selected");
                uiManager.OnMediumIndustrialButtonClicked();
                break;
            case "Heavy":
                Debug.Log("Heavy industrial zoning selected");
                uiManager.OnHeavyIndustrialButtonClicked();
                break;
            default:
                Debug.Log("Unknown industrial zoning selected");
                break;
        }
    }
}