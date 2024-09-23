using System.Collections.Generic;
using UnityEngine;

public class DetailsPopupController : MonoBehaviour
{
  public GameObject detailsPanel;

  public void ShowDetails()
  {
    detailsPanel.SetActive(true);
  }
}