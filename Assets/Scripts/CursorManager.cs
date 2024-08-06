using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D cursorTexture;  // Assign the cursor texture in the Inspector
    public Vector2 hotSpot = Vector2.zero;

    void Start()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }

    void Update()
    {
        // Handle cursor interactions here
        HandleCursorInteractions();
    }

    void HandleCursorInteractions()
    {
        if (Input.GetMouseButtonDown(0))  // Left mouse button clicked
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                // Handle tile interaction
                Zone zone = hit.collider.GetComponent<Zone>();
                if (zone != null)
                {
                    OnTileClicked(zone);
                }

                // Handle UI interaction (if any)
            }
        }
    }

    void OnTileClicked(Zone zone)
    {
        // Example interaction: Log the clicked zone type
        Debug.Log("Clicked on a " + zone.zoneType + " tile");
    }
}