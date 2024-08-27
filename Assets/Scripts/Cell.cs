using UnityEngine;

public class Cell : MonoBehaviour
{
    public enum TileType
    {
        Grass,
        Residential,
        Commercial,
        Industrial,
        Road,
        Water,
        Power,
        Fire,
        Police,
        Hospital,
        School,
        Park
    }

    public TileType tileType;
    public bool hasRoadAtLeft;
    public bool hasRoadAtTop;
    public bool hasRoadAtRight;
    public bool hasRoadAtBottom;
    public int population;

    public GameObject occupiedBuilding { get; set; }

    void Start()
    {
        // Initialize default values
        tileType = TileType.Grass;
        hasRoadAtLeft = false;
        hasRoadAtTop = false;
        hasRoadAtRight = false;
        hasRoadAtBottom = false;
        population = 0;
    }
}