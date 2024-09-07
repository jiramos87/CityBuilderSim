using System.Collections.Generic;
using UnityEngine;

public class ShowTaxesButton : MonoBehaviour
{
    public DataPopupController popupController; // Reference to the PopupController
    public UIManager uiManager; // Reference to the UIManager

    public void OnShowTaxesButtonClick()
    {
        Debug.Log("Show taxes button clicked");
        uiManager.UpdateUI();
        popupController.ToggleTaxes();
    }
}
