using UnityEngine;

public class Cell : MonoBehaviour
{
    public enum TileType
    {
        Grass,
        Residential,
        Commercial,
        Industrial,
        Road
    }

    public TileType tileType;
    public bool hasRoadAtLeft;
    public bool hasRoadAtTop;
    public bool hasRoadAtRight;
    public bool hasRoadAtBottom;
    public int population;

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