using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingSelectorMenuManager : MonoBehaviour
{
    [Serializable]
    public class ItemType
    {
        public string name;
        public Sprite icon;
        public int price;
    }

    public GameObject itemButtonPrefab; // Reference to the item button prefab
    public Transform content; // Reference to the Content object of ScrollView

    // Removed items list from here to avoid confusion on the data source

    // Call this method to populate the UI with items
    public void PopulateItems(List<ItemType> itemList, Action<ItemType> onItemSelected)
    {
        Debug.Log("Populating items: " + itemList.Count + " items with action: " + onItemSelected.Method.Name);
        // Clear existing buttons in the content
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);  // Clear previous UI elements
        }

        // Create a button for each item type
        foreach (var item in itemList)
        {
            CreateNewItemButton(item, onItemSelected);
        }
    }

    // Method to create a new item button
    private void CreateNewItemButton(ItemType item, Action<ItemType> onItemSelected)
    {
        Debug.Log("Creating new item button: " + item.name + " with price: " + item.price + " and icon: " + item.icon.name + " with action: " + onItemSelected.Method.Name);
        // Instantiate a new button for each item type
        GameObject newButton = Instantiate(itemButtonPrefab, content);

        // Set the icon and text of the button
        foreach (Transform child in newButton.transform)
        {
            Debug.Log("Child name: " + child.name);
        }

        // Set the icon of the button
        newButton.transform.Find("Image").GetComponent<Image>().sprite = item.icon;
        newButton.transform.Find("PriceText").GetComponent<TextMeshProUGUI>().text = item.price.ToString();
        newButton.transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = item.name;

        // Add a click listener to the button
        newButton.GetComponent<Button>().onClick.AddListener(() => onItemSelected(item));
    }
}