using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public int width, height;
    public float tileWidth = 1f; // Full width of the tile
    public float tileHeight = 0.5f; // Effective height due to isometric perspective
    private GameObject[,] gridArray;

    public ZoneManager zoneManager;
    public UIManager uiManager;

    public CityStats cityStats;

    public GameObject roadTilePrefab1;
    public GameObject roadTilePrefab2;

    private Vector3 startPosition;
    private bool isDrawingRoad = false;

    private List<GameObject> previewRoadTiles = new List<GameObject>();

    void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        gridArray = new GameObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject gridCell = new GameObject($"Cell_{x}_{y}");
                    
                // Calculate the isometric position
                float posX = (x - y) * (tileWidth / 2);
                float posY = (x + y) * (tileHeight / 2);
                    
                gridCell.transform.position = new Vector3(posX, posY, 0);
                gridCell.transform.SetParent(transform);
                gridArray[x, y] = gridCell;

                // Instantiate a random zone tile
                GameObject zoneTile = Instantiate(
                    zoneManager.GetRandomZonePrefab(Zone.ZoneType.Grass),
                    gridCell.transform.position,
                    Quaternion.identity
                );
                zoneTile.transform.SetParent(gridCell.transform);

                // Ensure the zoneTile has a PolygonCollider2D
                PolygonCollider2D polygonCollider = zoneTile.GetComponent<PolygonCollider2D>();
                if (polygonCollider == null)
                {
                    polygonCollider = zoneTile.AddComponent<PolygonCollider2D>();
                }

                // Ensure the zoneTile has a Zone component
                Zone zoneComponent = zoneTile.GetComponent<Zone>();
                if (zoneComponent == null)
                {
                    zoneComponent = zoneTile.AddComponent<Zone>();
                    zoneComponent.zoneType = Zone.ZoneType.Grass; // Set the initial zone type
                }
            }
        }
    }

    ZoneAttributes GetZoneAttributes(Zone.ZoneType zoneType)
    {
        switch (zoneType)
        {
            case Zone.ZoneType.Residential:
                return new ZoneAttributes(-100, 10, 5, 100);
            case Zone.ZoneType.Commercial:
                return new ZoneAttributes(-200, 20, 10, 200);
            case Zone.ZoneType.Industrial:
                return new ZoneAttributes(-300, 30, 15, 300);
            case Zone.ZoneType.Road:
                return new ZoneAttributes(-50, 0, 0, 50);
            case Zone.ZoneType.Grass:
                return new ZoneAttributes(1, 0, 1, 0);
            default:
                return null;
        }
    }

    void PlaceZone(Vector3 position)
    {
        Zone.ZoneType selectedZoneType = uiManager.GetSelectedZoneType();
        var zoneAttributes = GetZoneAttributes(selectedZoneType);

        if (zoneAttributes != null && cityStats.CanAfford(zoneAttributes.Cost))
        {
            // Find the cell at the given position
            foreach (GameObject cell in gridArray)
            {
                if (cell.transform.position == position)
                {
                    // Destroy the current child (existing zone tile)
                    if (cell.transform.childCount > 0)
                    {
                        Destroy(cell.transform.GetChild(0).gameObject);
                    }

                    // Instantiate the new zone tile
                    GameObject zoneTile = Instantiate(
                      zoneManager.GetRandomZonePrefab(selectedZoneType),
                      position,
                      Quaternion.identity
                    );
                    zoneTile.transform.SetParent(cell.transform);

                    // Update CityStats
                    uiManager.OnTileClicked(selectedZoneType, zoneAttributes);
                    break;
                }
            }
        }
    }

    void Update()
    {
        Zone.ZoneType selectedZoneType = uiManager.GetSelectedZoneType();

        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        if (hit.collider != null)
        {
            Zone zone = hit.collider.GetComponent<Zone>();

            if (zone != null)
            {
                if (selectedZoneType == Zone.ZoneType.Road)
                {
                    HandleRoadDrawing(worldPoint);
                }
                else
                {
                    HandleTilePlacement(selectedZoneType, zone);
                }
            }
        }
    }

    void HandleTilePlacement(Zone.ZoneType selectedZoneType, Zone zone)
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaceZone(zone.transform.position);
        }
    }

    void HandleRoadDrawing(Vector2 worldPoint)
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDrawingRoad = true;
            startPosition = GetGridPosition(worldPoint);
            Debug.Log("Started road drawing at " + startPosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDrawingRoad = false;
            Vector3 endPosition = GetGridPosition(worldPoint);
            Debug.Log("Ended road drawing at " + endPosition);
            DrawRoadLine(startPosition, endPosition);
            ClearPreview();
        }

        if (isDrawingRoad)
        {
            Vector3 currentMousePosition = GetGridPosition(worldPoint);
            DrawPreviewLine(startPosition, currentMousePosition);
        }
    }

    Vector3 GetGridPosition(Vector2 worldPoint)
    {
        // Convert world point to grid coordinates
        float posX = worldPoint.x / (tileWidth / 2);
        float posY = worldPoint.y / (tileHeight / 2);

        int gridX = Mathf.RoundToInt((posY + posX) / 2);
        int gridY = Mathf.RoundToInt((posY - posX) / 2);

        return new Vector3(gridX, gridY, 0);
    }

    Vector3 GetWorldPosition(int gridX, int gridY)
    {
        // Convert grid coordinates back to world position
        float posX = (gridX - gridY) * (tileWidth / 2);
        float posY = (gridX + gridY) * (tileHeight / 2);

        return new Vector3(posX, posY, 0);
    }

    void DrawPreviewLine(Vector3 start, Vector3 end)
    {
        ClearPreview();

        List<Vector3> path = GetLine(start, end);

        for (int i = 0; i < path.Count; i++)
        {
            Vector3 gridPos = path[i];
            Vector3 worldPos = GetWorldPosition((int)gridPos.x, (int)gridPos.y);
            if (IsWithinGrid(worldPos))
            {
                // Determine the direction for the current segment
                Vector3 prevGridPos = i == 0 ? start : path[i - 1];
                GameObject previewTile = Instantiate(
                    GetCorrectRoadPrefab(prevGridPos, gridPos),
                    worldPos,
                    Quaternion.identity
                );
                previewTile.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f); // Make it semi-transparent
                previewTile.transform.SetParent(transform);
                previewRoadTiles.Add(previewTile);
            }
        }
    }

    void ClearPreview()
    {
        foreach (GameObject previewTile in previewRoadTiles)
        {
            Destroy(previewTile);
        }
        previewRoadTiles.Clear();
    }

    void DrawRoadLine(Vector3 start, Vector3 end)
    {
        List<Vector3> path = GetLine(start, end);

        for (int i = 0; i < path.Count; i++)
        {
            Vector3 gridPos = path[i];
            Vector3 worldPos = GetWorldPosition((int)gridPos.x, (int)gridPos.y);
            if (IsWithinGrid(worldPos))
            {
                // Determine the direction for the current segment
                Vector3 prevGridPos = i == 0 ? start : path[i - 1];
                PlaceRoadTile(worldPos, prevGridPos, gridPos);
            }
        }

        int totalCost = CalculateTotalCost(start, end);
        cityStats.RemoveMoney(totalCost);
    }

    void PlaceRoadTile(Vector3 position, Vector3 prevGridPos, Vector3 currGridPos)
    {
        // Find the cell at the given position
        foreach (GameObject cell in gridArray)
        {
            if (cell.transform.position == position)
            {
                // Destroy the current child (existing zone tile)
                if (cell.transform.childCount > 0)
                {
                    Destroy(cell.transform.GetChild(0).gameObject);
                }

                // Instantiate the road tile
                GameObject roadTile = Instantiate(
                    GetCorrectRoadPrefab(prevGridPos, currGridPos),
                    position,
                    Quaternion.identity
                );
                roadTile.transform.SetParent(cell.transform);
                break;
            }
        }
    }
    GameObject GetCorrectRoadPrefab(Vector3 prevGridPos, Vector3 currGridPos)
    {
        Vector3 direction = currGridPos - prevGridPos;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            return roadTilePrefab2; // Horizontal or diagonal
        }
        else
        {
            return roadTilePrefab1; // Vertical or diagonal
        }
    }

    bool IsWithinGrid(Vector3 position)
    {
        foreach (GameObject cell in gridArray)
        {
            if (cell.transform.position == position)
            {
                return true;
            }
        }
        return false;
    }

    int CalculateTotalCost(Vector3 start, Vector3 end)
    {
        float distance = Vector3.Distance(start, end);
        return Mathf.CeilToInt(distance) * 50; // Example: cost per tile is 50
    }

    List<Vector3> GetLine(Vector3 start, Vector3 end)
    {
        List<Vector3> line = new List<Vector3>();

        int x0 = (int)start.x;
        int y0 = (int)start.y;
        int x1 = (int)end.x;
        int y1 = (int)end.y;

        int dx = Mathf.Abs(x1 - x0);
        int dy = Mathf.Abs(y1 - y0);
        int sx = x0 < x1 ? 1 : -1;
        int sy = y0 < y1 ? 1 : -1;
        int err = dx - dy;

        while (true)
        {
            line.Add(new Vector3(x0, y0, 0));

            if (x0 == x1 && y0 == y1) break;

            int e2 = err * 2;
            if (e2 > -dy)
            {
                err -= dy;
                x0 += sx;
            }
            if (e2 < dx)
            {
                err += dx;
                y0 += sy;
            }
        }

        return line;
    }
}
