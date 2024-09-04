using System.Collections.Generic;
using UnityEngine;

public class RoadsSelectorButton : MonoBehaviour
{
    public BuildingSelectorMenuController popupController; // Reference to the PopupController
    public List<BuildingSelectorMenuManager.ItemType> roadItems; // List of road items
    public UIManager uiManager; // Reference to the UIManager

    public void OnRoadsButtonClick()
    {
        Debug.Log("Roads button clicked");
        popupController.ShowPopup(roadItems, OnRoadTypeSelected);
    }

    private void OnRoadTypeSelected(BuildingSelectorMenuManager.ItemType selectedItem)
    {
        Debug.Log("Selected road type: " + selectedItem.name);
        switch (selectedItem.name)
        {
            case "Two-way road":
                Debug.Log("Two-way road selected");
                uiManager.OnTwoWayRoadButtonClicked();
                break;
            default:
                Debug.Log("Unknown power building selected");
                break;
        }
    }
}
