using System.Collections.Generic;
using UnityEngine;

public class ShowStatsButton : MonoBehaviour
{
  public DataPopupController popupController; // Reference to the PopupController
  public UIManager uiManager; // Reference to the UIManager

  public void OnShowStatsButtonClick()
  {
    Debug.Log("Show stats button clicked");
    uiManager.UpdateUI();
    popupController.ToggleStats();  
  }
}