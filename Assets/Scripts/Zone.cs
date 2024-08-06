using UnityEngine;

public class Zone : MonoBehaviour
{
    public enum ZoneType { Residential, Commercial, Industrial, Road, Grass }
    public ZoneType zoneType;

    void Start()
    {
        // Initialize zone-specific properties
    }
}