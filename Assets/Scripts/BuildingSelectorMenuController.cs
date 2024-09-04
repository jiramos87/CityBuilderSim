using System.Collections.Generic;
using UnityEngine;

public class BuildingSelectorMenuController : MonoBehaviour
{
    public BuildingSelectorMenuManager menuManager; // Reference to the menu manager
    public GameObject popupPanel; // Reference to the popup panel

    public void ShowPopup(List<BuildingSelectorMenuManager.ItemType> items, System.Action<BuildingSelectorMenuManager.ItemType> onItemSelected)
    {
        Debug.Log("Showing popup + " + items.Count + " items with action: " + onItemSelected.Method.Name);
        TogglePopup(true);
        menuManager.PopulateItems(items, onItemSelected);
    }

    public void HidePopup()
    {
        TogglePopup(false);
    }

    private void TogglePopup(bool isVisible)
    {
        Debug.Log("Toggling popup visibility: " + isVisible);
        popupPanel.SetActive(isVisible); // Toggle the popup panel visibility
    }
}