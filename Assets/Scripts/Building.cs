using UnityEngine;

public class Building : MonoBehaviour
{
    public enum BuildingType { Power, Water, Fire, Police, Hospital, School, Park }
    public BuildingType buildingType;

    void Start()
    {
        // Initialize building-specific properties
    }
}