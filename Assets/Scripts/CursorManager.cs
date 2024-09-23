using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Texture2D bulldozerTexture;
    public Texture2D detailsTexture;
    public Vector2 hotSpot;
    private GameObject previewInstance;
    public GridManager gridManager;

    void Start()
    {
        hotSpot = Vector2.zero;
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }

    public void SetBullDozerCursor()
    {
        hotSpot = new Vector2(0, bulldozerTexture.height);
        Cursor.SetCursor(bulldozerTexture, hotSpot, CursorMode.Auto);
    }

    public void SetDefaultCursor()
    {
        hotSpot = Vector2.zero;
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }

    public void SetDetailsCursor()
    {
        hotSpot = Vector2.zero;
        Cursor.SetCursor(detailsTexture, hotSpot, CursorMode.Auto);
    }

    public void ShowBuildingPreview(GameObject buildingPrefab)
    {
        if (previewInstance != null)
        {
            Destroy(previewInstance); // Remove any existing preview instance
        }

        // Instantiate a preview of the buildingPrefab
        previewInstance = Instantiate(buildingPrefab);
        previewInstance.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f); // Set transparency

        // Optionally disable colliders or other components
        Collider2D[] colliders = previewInstance.GetComponentsInChildren<Collider2D>();
        foreach (var col in colliders)
        {
            col.enabled = false; // Disable collision for the preview
        }
    }

    void Update()
    {
        if (previewInstance != null)
        {
            // Follow the mouse position with the preview instance
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Adjust z-axis if necessary

            Vector2 gridPosition = gridManager.GetGridPosition(mousePosition);

            Vector2 worldPosition = gridManager.GetWorldPosition((int)gridPosition.x, (int)gridPosition.y);

            previewInstance.transform.position = worldPosition;
        }
    }

    public void RemovePreview()
    {
        if (previewInstance != null)
        {
            Destroy(previewInstance);
        }
    }
}