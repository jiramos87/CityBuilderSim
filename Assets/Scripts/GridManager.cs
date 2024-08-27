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

    private Vector2 startPosition;
    private bool isDrawingRoad = false;

    private bool isPlacingBuilding = false;

    private List<GameObject> previewRoadTiles = new List<GameObject>();

    private List<Vector2> previewRoadGridPositions = new List<Vector2>();
    private List<Vector2> adjacentRoadTiles = new List<Vector2>();

    public Vector2 mouseGridPosition;

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
                sr.sortingOrder = -1001; // Use a very low value to ensure it's always in the background


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
                return ZoneAttributes.Residential;
            case Zone.ZoneType.Commercial:
                return ZoneAttributes.Commercial;
            case Zone.ZoneType.Industrial:
                return ZoneAttributes.Industrial;
            case Zone.ZoneType.Road:
                return ZoneAttributes.Road;
            case Zone.ZoneType.Grass:
                return ZoneAttributes.Grass;
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

            if (mouseGridPosition.x < 0 || mouseGridPosition.x >= width || mouseGridPosition.y < 0 || mouseGridPosition.y >= height)
            {
                return;
            }

            if (uiManager.isBulldozeMode())
            {
                HandleBulldozerMode(mouseGridPosition);
            }

            if (IsInRoadDrawingMode())
            {
                HandleRaycast(mouseGridPosition);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Update error: " + ex);
        }
    }

    void HandleBulldozerMode(Vector2 gridPosition)
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleBulldozerClick(gridPosition);
        }
    }

    void HandleBulldozerClick(Vector2 gridPosition)
    {
        GameObject cell = gridArray[(int)gridPosition.x, (int)gridPosition.y];

        Zone zone = cell.transform.GetChild(0).GetComponent<Zone>();

        IBuilding building = cell.GetComponent<Cell>().occupiedBuilding?.GetComponent<IBuilding>();

        HandleBulldozeTile(zone, cell);
    }

    void HandleBulldozeTile(Zone zone, GameObject cell)
    {
        if (cell.transform.childCount > 0)
        {
            // Destroy(cell.transform.GetChild(0).gameObject);
            foreach (Transform child in cell.transform)
            {
                Destroy(child.gameObject);
            }
        }

        Cell cellComponent = cell.GetComponent<Cell>();
        cellComponent.tileType = Cell.TileType.Grass;

        GameObject zoneTile = Instantiate(
            zoneManager.GetRandomZonePrefab(Zone.ZoneType.Grass),
            cell.transform.position,
            Quaternion.identity
        );

        zoneTile.transform.SetParent(cell.transform);

        SpriteRenderer sr = zoneTile.GetComponent<SpriteRenderer>();

        sr.sortingOrder = -1001;

        cityStats.AddMoney(25);

        ZoneAttributes zoneAttributes = GetZoneAttributes(zone.zoneType);
        cityStats.RemovePowerConsumption(zoneAttributes.PowerConsumption);
    }

    bool IsInRoadDrawingMode()
    {
        return uiManager.GetSelectedZoneType() == Zone.ZoneType.Road ||
          uiManager.GetSelectedZoneType() == Zone.ZoneType.Residential ||
          uiManager.GetSelectedZoneType() == Zone.ZoneType.Commercial ||
          uiManager.GetSelectedZoneType() == Zone.ZoneType.Industrial ||
          uiManager.GetSelectedZoneType() == Zone.ZoneType.Grass ||
          uiManager.GetSelectedBuilding() != null;
    }

    void HandleRaycast(Vector2 gridPosition)
    {
          GameObject cell = gridArray[(int)gridPosition.x, (int)gridPosition.y];

          Zone.ZoneType selectedZoneType = uiManager.GetSelectedZoneType();
          IBuilding selectedBuilding = uiManager.GetSelectedBuilding();

          if (selectedZoneType == Zone.ZoneType.Road)
          {
              HandleRoadDrawing(gridPosition);
          }
          else if (selectedBuilding != null)
          {
              HandleBuildingPlacement(gridPosition, selectedBuilding);
          }
          else
          {
              HandleTilePlacement(gridPosition);
          }

    }

    void HandleBuildingPlacement(Vector3 gridPosition, IBuilding selectedBuilding)
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaceBuilding(gridPosition, selectedBuilding);
        }
    }
        

    void HandleTilePlacement(Vector3 gridPosition)
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaceZone(gridPosition);
        }
    }

    bool canPlaceZone(ZoneAttributes zoneAttributes, Vector3 gridPosition)
    {
        return zoneAttributes != null && cityStats.CanAfford(zoneAttributes.ConstructionCost) && canPlaceBuilding(gridPosition, 1);
    }

    void destroyCellChildren(GameObject cell)
    {
        if (cell.transform.childCount > 0)
        {
            foreach (Transform child in cell.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    void PlaceZone(Vector3 gridPosition)
    {
        Vector2 worldPosition = GetWorldPosition((int)gridPosition.x, (int)gridPosition.y);
        Zone.ZoneType selectedZoneType = uiManager.GetSelectedZoneType();
        var zoneAttributes = GetZoneAttributes(selectedZoneType);

        if (canPlaceZone(zoneAttributes, gridPosition))
        {
            GameObject cell = gridArray[(int)gridPosition.x, (int)gridPosition.y];

            destroyCellChildren(cell);

            // Instantiate the new zone tile
            GameObject zoneTile = Instantiate(
              zoneManager.GetRandomZonePrefab(selectedZoneType),
              worldPosition,
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

            cityStats.AddPowerConsumption(zoneAttributes.PowerConsumption);
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

    void HandleRoadDrawing(Vector2 gridPosition)
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDrawingRoad = true;
            startPosition = gridPosition;
        }
        else if (isDrawingRoad && Input.GetMouseButton(0))
        {
            Vector3 currentMousePosition = gridPosition;
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

    Vector2 GetGridPosition(Vector2 worldPoint)
    {
        // Convert world point to grid coordinates
        float posX = worldPoint.x / (tileWidth / 2);
        float posY = worldPoint.y / (tileHeight / 2);

        int gridX = Mathf.RoundToInt((posY + posX) / 2);
        int gridY = Mathf.RoundToInt((posY - posX) / 2);

        return new Vector2(gridX, gridY);
    }

    Vector3 GetWorldPosition(int gridX, int gridY)
    {
        // Convert grid coordinates back to world position
        float posX = (gridX - gridY) * (tileWidth / 2);
        float posY = (gridX + gridY) * (tileHeight / 2);

        return new Vector2(posX, posY);
    }

    void DrawPreviewLine(Vector2 start, Vector2 end)
    {
        ClearPreview();
        List<Vector2> path = GetLine(start, end);

        for (int i = 0; i < path.Count; i++)
        {
            Vector2 gridPos = path[i];

            DrawPreviewRoadTile(gridPos, path, i, true);
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
      previewTile.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
    }

    void DrawPreviewRoadTile(Vector2 gridPos, List<Vector2> path, int i, bool isCenterRoadTile = true)
    {
      Vector2 prevGridPos = i == 0 ? (path.Count > 1 ? path[1] : gridPos) : path[i - 1];

      bool isPreview = true;

      GameObject roadPrefab = GetCorrectRoadPrefab(prevGridPos, gridPos, isCenterRoadTile, isPreview);

      Vector2 worldPos = GetWorldPosition((int)gridPos.x, (int)gridPos.y);

      GameObject previewTile = Instantiate(
          roadPrefab,
          worldPos,
          Quaternion.identity
      );

      SetPreviewRoadTileDetails(previewTile);
      
      previewRoadTiles.Add(previewTile);

      previewRoadGridPositions.Add(new Vector2(gridPos.x, gridPos.y));

      GameObject cell = gridArray[(int)gridPos.x, (int)gridPos.y];

      previewTile.transform.SetParent(cell.transform);
    }

    bool isAdjacentRoadInPreview(Vector2 gridPos)
    {
        foreach (Vector2 previewGridPos in previewRoadGridPositions)
        {
            if (gridPos == previewGridPos)
            {
                return true;
            }
        }
        return false;
    }

    void UpdateAdjacentRoadPrefabs(Vector2 gridPos, int i)
    {
        foreach (Vector2 adjacentRoadTile in adjacentRoadTiles)
        {
            bool isAdjacent = true;
            if (!isAdjacentRoadInPreview(adjacentRoadTile))
            {
                PlaceRoadTile(adjacentRoadTile, i, isAdjacent);
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
        previewRoadGridPositions.Clear();
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

    void PlaceRoadTile(Vector2 gridPos, int i = 0, bool isAdjacent = false)
    {
        GameObject cell = gridArray[(int)gridPos.x, (int)gridPos.y];
        
        bool isCenterRoadTile = !isAdjacent;
        bool isPreview = false;

        Vector2 prevGridPos = isAdjacent
            ? (i == 0 ? gridPos : previewRoadGridPositions[i - 1])
            : new Vector2(0, 0);

        GameObject correctRoadPrefab = GetCorrectRoadPrefab(
            prevGridPos,
            gridPos,
            isCenterRoadTile,
            isPreview
        );

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
        for (int i = 0; i < previewRoadGridPositions.Count; i++)
        {
            Vector2 gridPos = previewRoadGridPositions[i];

            PlaceRoadTile(gridPos, i, false);

            UpdateAdjacentRoadPrefabs(gridPos, i);
        }

        if (calculateCost)
        {
            int totalCost = CalculateTotalCost(previewRoadGridPositions.Count);

            cityStats.RemoveMoney(totalCost);

            int roadPowerConsumption = previewRoadGridPositions.Count * ZoneAttributes.Road.PowerConsumption;

            cityStats.AddPowerConsumption(roadPowerConsumption);
        }
    }

    GameObject GetCorrectRoadPrefab(Vector2 prevGridPos, Vector2 currGridPos, bool isCenterRoadTile = true, bool isPreview = false)
    {
        Vector2 direction = currGridPos - prevGridPos;
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

        bool hasLeft = IsRoadAt(currGridPos + new Vector2(-1, 0));
        bool hasRight = IsRoadAt(currGridPos + new Vector2(1, 0));
        bool hasUp = IsRoadAt(currGridPos + new Vector2(0, 1));
        bool hasDown = IsRoadAt(currGridPos + new Vector2(0, -1));
  
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

    void UpdateAdjacentRoadTilesArray(Vector2 currGridPos, bool hasLeft, bool hasRight, bool hasUp, bool hasDown, bool isPreview)
    {
        adjacentRoadTiles.Clear();

        if (hasLeft)
        {
            adjacentRoadTiles.Add(new Vector2(currGridPos.x - 1, currGridPos.y));
        }
        if (hasRight)
        {
            adjacentRoadTiles.Add(new Vector2(currGridPos.x + 1, currGridPos.y));
        }
        if (hasUp)
        {
            adjacentRoadTiles.Add(new Vector2(currGridPos.x, currGridPos.y + 1));
        }
        if (hasDown)
        {
            adjacentRoadTiles.Add(new Vector2(currGridPos.x, currGridPos.y - 1));
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

    bool IsRoadAt(Vector2 gridPos)
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

    bool IsWithinGrid(Vector2 position)
    {
        foreach (GameObject cell in gridArray)
        {
            Vector3 position3D = new Vector3(position.x, position.y, 0);
            if (cell.transform.position == position3D)
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

    List<Vector2> GetLine(Vector2 start, Vector2 end)
    {
        List<Vector2> line = new List<Vector2>();

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
            line.Add(new Vector2(x0, y0));

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

    bool canPlaceBuilding (Vector2 gridPosition, int buildingSize) {
        for (int x = 0; x < buildingSize; x++)
        {
            for (int y = 0; y < buildingSize; y++)
            {
                int gridX = (int)gridPosition.x + x - buildingSize / 2;
                int gridY = (int)gridPosition.y + y - buildingSize / 2;
                if (gridArray[gridX, gridY].transform.childCount > 0 && gridArray[gridX, gridY].transform.GetChild(0).GetComponent<Zone>().zoneType != Zone.ZoneType.Grass)
                {
                    return false;
                }
            }
        }
        return true;
    }

    void PlaceBuilding(Vector2 gridPos, IBuilding iBuilding)
    {
        int buildingSize = iBuilding.BuildingSize;
        GameObject buildingPrefab = iBuilding.Prefab;
        Vector2 position = GetWorldPosition((int)gridPos.x, (int)gridPos.y);

        if (canPlaceBuilding(gridPos, buildingSize))
        {
            GameObject building = Instantiate(buildingPrefab, position, Quaternion.identity);
            building.transform.SetParent(gridArray[(int)gridPos.x, (int)gridPos.y].transform);

            PowerPlant powerPlant = iBuilding.GameObjectReference.GetComponent<PowerPlant>();

            if (powerPlant != null)
            {
                cityStats.RegisterPowerPlant(powerPlant);
            }
            SetTileSortingOrder(building);

            for (int x = 0; x < buildingSize; x++)
            {
                for (int y = 0; y < buildingSize; y++)
                {
                    int gridX = (int)gridPos.x + x - buildingSize / 2;
                    int gridY = (int)gridPos.y + y - buildingSize / 2;

                    gridArray[gridX, gridY].GetComponent<Cell>().occupiedBuilding = building;
                }
            }
        }
        else
        {
            Debug.Log("Cannot place building here, area is not available.");
        }
    }
}
