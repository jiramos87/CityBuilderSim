using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

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
    public GameObject roadTilePrefabCrossing;
    public GameObject roadTilePrefabTIntersectionUp;
    public GameObject roadTilePrefabTIntersectionDown;
    public GameObject roadTilePrefabTIntersectionLeft;
    public GameObject roadTilePrefabTIntersectionRight;
    public GameObject roadTilePrefabElbowUpLeft;
    public GameObject roadTilePrefabElbowUpRight;
    public GameObject roadTilePrefabElbowDownLeft;
    public GameObject roadTilePrefabElbowDownRight;

    private Vector3 startPosition;
    private bool isDrawingRoad = false;

    private List<GameObject> previewRoadTiles = new List<GameObject>();
    private List<GameObject> adjacentRoadTiles = new List<GameObject>();

    public Vector3 mouseGridPosition;

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

                Cell cellComponent = gridCell.AddComponent<Cell>();
                gridArray[x, y] = gridCell;

                // Instantiate a random zone tile
                GameObject zoneTile = Instantiate(
                    zoneManager.GetRandomZonePrefab(Zone.ZoneType.Grass),
                    gridCell.transform.position,
                    Quaternion.identity
                );
                zoneTile.transform.SetParent(gridCell.transform);

                // Set a low sorting order for the grass tile
                SpriteRenderer sr = zoneTile.GetComponent<SpriteRenderer>();
                sr.sortingOrder = -1000; // Use a very low value to ensure it's always in the background


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

    void Update()
    {
        try
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseGridPosition = GetGridPosition(worldPoint);

            if (IsInRoadDrawingMode())
            {
                HandleRaycast(worldPoint);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Update error: " + ex);
        }
    }

    bool IsInRoadDrawingMode()
    {
        return uiManager.GetSelectedZoneType() == Zone.ZoneType.Road ||
          uiManager.GetSelectedZoneType() == Zone.ZoneType.Residential ||
          uiManager.GetSelectedZoneType() == Zone.ZoneType.Commercial ||
          uiManager.GetSelectedZoneType() == Zone.ZoneType.Industrial ||
          uiManager.GetSelectedZoneType() == Zone.ZoneType.Grass;
    }

    void HandleRaycast(Vector2 worldPoint)
    {
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        if (hit.collider != null)
        {
            Zone zone = hit.collider.GetComponent<Zone>();

            if (zone != null)
            {
                Zone.ZoneType selectedZoneType = uiManager.GetSelectedZoneType();

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
                        foreach (Transform child in cell.transform)
                        {
                            Destroy(child.gameObject);
                        }
                    }

                    // Instantiate the new zone tile
                    GameObject zoneTile = Instantiate(
                      zoneManager.GetRandomZonePrefab(selectedZoneType),
                      position,
                      Quaternion.identity
                    );
                    zoneTile.transform.SetParent(cell.transform);

                    // Update the Cell component
                    Cell cellComponent = cell.GetComponent<Cell>();
                    cellComponent.tileType = (Cell.TileType)selectedZoneType;
                    // Update other properties if necessary

                    // Set the sorting order for the isometric grid
                    SetTileSortingOrder(zoneTile);

                    // Update CityStats
                    uiManager.OnTileClicked(selectedZoneType, zoneAttributes);
                    break;
                }
            }
        }
    }

    void SetTileSortingOrder(GameObject tile)
    {
        Vector3 gridPos = GetGridPosition(tile.transform.position);
        int x = (int)gridPos.x;
        int y = (int)gridPos.y;
        SpriteRenderer sr = tile.GetComponent<SpriteRenderer>();
        sr.sortingOrder = -(y * 10 + x) - 100;
    }

    void HandleRoadDrawing(Vector2 worldPoint)
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDrawingRoad = true;
            startPosition = GetGridPosition(worldPoint);
        }
        else if (isDrawingRoad && Input.GetMouseButton(0))
        {
            Vector3 currentMousePosition = GetGridPosition(worldPoint);
            DrawPreviewLine(startPosition, currentMousePosition);
        }
        
        if (Input.GetMouseButtonUp(0) && isDrawingRoad)
        {
            isDrawingRoad = false;
            // Vector3 endPosition = GetGridPosition(worldPoint);
            DrawRoadLine(true);
            ClearPreview(true);
        }

        if (Input.GetMouseButtonDown(1))
        {
            isDrawingRoad = false;
            ClearPreview();
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
                DrawPreviewRoadTile(gridPos, worldPos, path, i, true);
            }
        }
    }

    Vector2[] GetRoadColliderPoints()
    {
        Vector2[] points = new Vector2[4];
        points[0] = new Vector2(-0.5f, 0f);
        points[1] = new Vector2(0f, 0.25f);
        points[2] = new Vector2(0.5f, 0f);
        points[3] = new Vector2(0f, -0.25f);

        return points;
    }

    void SetPreviewTileCollider(GameObject previewTile)
    {
        PolygonCollider2D collider = previewTile.AddComponent<PolygonCollider2D>();
        collider.points = GetRoadColliderPoints();
        collider.isTrigger = true;
    }

    void SetSpriteRendererSortingOrder(GameObject tile, int order)
    {
        SpriteRenderer sr = tile.GetComponent<SpriteRenderer>();
        sr.sortingOrder = order;
    }

    void SetRoadTileZoneDetails(GameObject roadTile)
    {
        Zone zone = roadTile.AddComponent<Zone>();
        zone.zoneType = Zone.ZoneType.Road;
    }

    void SetPreviewRoadTileDetails(GameObject previewTile)
    {
      SetPreviewTileCollider(previewTile);
      SetSpriteRendererSortingOrder(previewTile, -999);
      SetRoadTileZoneDetails(previewTile);
    }

    void DrawPreviewRoadTile(Vector3 gridPos, Vector3 worldPos, List<Vector3> path, int i, bool isCenterRoadTile = true)
    {
      Vector3 prevGridPos = i == 0 ? (path.Count > 1 ? path[1] : gridPos) : path[i - 1];

      bool isPreview = true;

      GameObject roadPrefab = GetCorrectRoadPrefab(prevGridPos, gridPos, isCenterRoadTile, isPreview);

      GameObject previewTile = Instantiate(
          roadPrefab,
          worldPos,
          Quaternion.identity
      );

      SetPreviewRoadTileDetails(previewTile);
      previewTile.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);

      previewRoadTiles.Add(previewTile);

      GameObject cell = gridArray[(int)gridPos.x, (int)gridPos.y];

      previewTile.transform.SetParent(cell.transform);
    }

    bool isAdjacentRoadInPreview(Vector3 gridPos)
    {
        foreach (GameObject previewRoadTile in previewRoadTiles)
        {
            Vector3 previewGridPos = GetGridPosition(previewRoadTile.transform.position);

            if (previewGridPos == gridPos)
            {
                return true;
            }
        }
        return false;
    }

    void UpdateAdjacentRoadPrefabs(Vector3 gridPos, int i)
    {
        Debug.Log("UpdateAdjacentRoadPrefabs at " + gridPos + " adjRoadTiles: " + adjacentRoadTiles.Count);
        foreach (GameObject adjacentRoadTile in adjacentRoadTiles)
        {
            Debug.Log("adjRoadTile: " + adjacentRoadTile);
            Vector3 adjacentGridPos = GetGridPosition(adjacentRoadTile.transform.position);
            Vector3 worldPos = GetWorldPosition((int)adjacentGridPos.x, (int)adjacentGridPos.y);

            bool isAdjacent = true;
            if (IsWithinGrid(worldPos) && !isAdjacentRoadInPreview(adjacentGridPos))
            {
                Debug.Log("Placing adjacent road tile at: " + adjacentGridPos);
                PlaceRoadTile(adjacentGridPos, i, isAdjacent);
            }
        }
    }

    void ClearPreview(bool isEnd = false)
    {
        foreach (GameObject previewTile in previewRoadTiles)
        {
            Destroy(previewTile);
        }
        previewRoadTiles.Clear();
    }

    void DestroyPreviousZoning(GameObject cell)
    {
        if (cell.transform.childCount > 0)
        {
            foreach (Transform child in cell.transform)
            {
                DestroyImmediate(child.gameObject);
            }
        }
    }

    void DestroyPreviousRoadTile(GameObject cell)
    {
        if (cell.transform.childCount > 0)
        {
            foreach (Transform child in cell.transform)
            {
                Zone zone = child.GetComponent<Zone>();
                if (zone != null)
                {
                    DestroyImmediate(child.gameObject);
                    continue;
                }
            }
        }
    }

    void PlaceRoadTile(Vector3 gridPos, int i = 0, bool isAdjacent = false)
    {
        GameObject cell = gridArray[(int)gridPos.x, (int)gridPos.y];
        
        bool isCenterRoadTile = !isAdjacent;
        bool isPreview = false;

        Vector3 prevGridPos = isAdjacent
            ? (i == 0 ? gridPos : GetGridPosition(previewRoadTiles[i - 1].transform.position))
            : new Vector3(0, 0, 0);

        GameObject correctRoadPrefab = GetCorrectRoadPrefab(
            prevGridPos,
            gridPos,
            isCenterRoadTile,
            isPreview
        );
        if (isAdjacent)
        {
            Debug.Log("Placing adjacent road tile at: " + gridPos + " with prefab: " + correctRoadPrefab);
        }
        DestroyPreviousRoadTile(cell);

        GameObject roadTile = Instantiate(
            correctRoadPrefab,
            GetWorldPosition((int)gridPos.x, (int)gridPos.y),
            Quaternion.identity
        );

        roadTile.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

        Zone zone = roadTile.AddComponent<Zone>();
        zone.zoneType = Zone.ZoneType.Road;

        SetSpriteRendererSortingOrder(roadTile, -1000);

        roadTile.transform.SetParent(cell.transform);
    }

    void DrawRoadLine(bool calculateCost = true)
    {
        for (int i = 0; i < previewRoadTiles.Count; i++)
        {
            Vector3 gridPos = GetGridPosition(previewRoadTiles[i].transform.position);

            PlaceRoadTile(gridPos, i, false);

            UpdateAdjacentRoadPrefabs(gridPos, i);
        }

        if (calculateCost)
        {
            int totalCost = CalculateTotalCost(previewRoadTiles.Count);

            cityStats.RemoveMoney(totalCost);
        }
    }

    GameObject GetCorrectRoadPrefab(Vector3 prevGridPos, Vector3 currGridPos, bool isCenterRoadTile = true, bool isPreview = false)
    {
        Vector3 direction = currGridPos - prevGridPos;
        if (isPreview)
        {
          
          if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
          {
              return roadTilePrefab2;
          }
          else
          {
              return roadTilePrefab1;
          }
        }

        bool hasLeft = IsRoadAt(currGridPos + new Vector3(-1, 0, 0));
        bool hasRight = IsRoadAt(currGridPos + new Vector3(1, 0, 0));
        bool hasUp = IsRoadAt(currGridPos + new Vector3(0, 1, 0));
        bool hasDown = IsRoadAt(currGridPos + new Vector3(0, -1, 0));
  
        if (isCenterRoadTile) {
          UpdateAdjacentRoadTilesArray(currGridPos, hasLeft, hasRight, hasUp, hasDown, isPreview);
        }

        if (hasLeft && hasRight && hasUp && hasDown)
        {
            return roadTilePrefabCrossing;
        }
        else if (hasLeft && hasRight && hasUp && !hasDown)
        {
            return roadTilePrefabTIntersectionDown;
        }
        else if (hasLeft && hasRight && hasDown && !hasUp)
        {
            return roadTilePrefabTIntersectionUp;
        }
        else if (hasUp && hasDown && hasLeft && !hasRight)
        {
            return roadTilePrefabTIntersectionRight;
        }
        else if (hasUp && hasDown && hasRight && !hasLeft)
        {
            return roadTilePrefabTIntersectionLeft;
        }
        else if (hasLeft && hasUp && !hasRight && !hasDown)
        {
            return roadTilePrefabElbowDownRight;
        }
        else if (hasRight && hasUp && !hasLeft && !hasDown)
        {
            return roadTilePrefabElbowDownLeft;
        }
        else if (hasLeft && hasDown && !hasRight && !hasUp)
        {
            return roadTilePrefabElbowUpRight;
        }
        else if (hasRight && hasDown && !hasLeft && !hasUp)
        {
            return roadTilePrefabElbowUpLeft;
        }
        else if (hasLeft || hasRight)
        {
          return roadTilePrefab2;
        }

        else if (hasUp || hasDown)
        {
          return roadTilePrefab1;
        }

        // If no intersection or elbow, fall back to horizontal/vertical

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            return roadTilePrefab2;
        }
        else
        {
            return roadTilePrefab1;
        }
    }

    void UpdateAdjacentRoadTilesArray(Vector3 currGridPos, bool hasLeft, bool hasRight, bool hasUp, bool hasDown, bool isPreview)
    {
        adjacentRoadTiles.Clear();

        if (hasLeft)
        {
            adjacentRoadTiles.Add(gridArray[(int)currGridPos.x - 1, (int)currGridPos.y]);
        }
        if (hasRight)
        {
            adjacentRoadTiles.Add(gridArray[(int)currGridPos.x + 1, (int)currGridPos.y]);
        }
        if (hasUp)
        {
            adjacentRoadTiles.Add(gridArray[(int)currGridPos.x, (int)currGridPos.y + 1]);
        }
        if (hasDown)
        {
            adjacentRoadTiles.Add(gridArray[(int)currGridPos.x, (int)currGridPos.y - 1]);
        }
    }

    bool IsAnyChildRoad(int gridX, int gridY)
    {
        GameObject cell = gridArray[gridX, gridY];
        bool isRoad = false;

        if (cell != null && cell.transform.childCount > 0)
        {
            for (int i = 0; i < cell.transform.childCount; i++)
            {
                Transform child = cell.transform.GetChild(i);
                Zone zone = child.GetComponent<Zone>();

                if (zone != null && zone.zoneType == Zone.ZoneType.Road)
                {
                    isRoad = true;
                    break;
                }
            }
        }

        return isRoad;
    }

    bool IsRoadAt(Vector3 gridPos)
    {
        bool isRoad = false;
        int gridX = Mathf.RoundToInt(gridPos.x);
        int gridY = Mathf.RoundToInt(gridPos.y);

        

        if (gridX >= 0 && gridX < width && gridY >= 0 && gridY < height)
        {
            isRoad = IsAnyChildRoad(gridX, gridY);

            return isRoad;
        }

        return false;
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

    int CalculateTotalCost(int tilesCount)
    {
        return tilesCount * 50;
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
