using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool hasRoadAtLeft;
    public bool hasRoadAtTop;
    public bool hasRoadAtRight;
    public bool hasRoadAtBottom;
    public int population;

    public Zone.ZoneType zoneType;

    public GameObject occupiedBuilding { get; set; }

    void Start()
    {
        // Initialize default values
        zoneType = Zone.ZoneType.Grass;
        hasRoadAtLeft = false;
        hasRoadAtTop = false;
        hasRoadAtRight = false;
        hasRoadAtBottom = false;
        population = 0;
    }
}