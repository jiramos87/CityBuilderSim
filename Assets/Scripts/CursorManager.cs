using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Texture2D bulldozerTexture;
    public Vector2 hotSpot;

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
}