using UnityEngine;

public class City : MonoBehaviour {
    public CityStats cityStats;

    void Start() {
        cityStats = GetComponent<CityStats>();
    }

    void Update() {
        // Update city stats UI
    }

    private void OnTileClicked(Zone zone) {
        AddZone(zone.zoneType);
    }

    public void AddZone(Zone.ZoneType zoneType) {
        switch (zoneType) {
            case Zone.ZoneType.Residential:
                cityStats.AddResidentialZoneCount();
                break;
            case Zone.ZoneType.Commercial:
                cityStats.AddCommercialZoneCount();
                break;
            case Zone.ZoneType.Industrial:
                cityStats.AddIndustrialZoneCount();
                break;
            case Zone.ZoneType.Road:
                cityStats.AddRoadCount();
                break;
            case Zone.ZoneType.Grass:
                cityStats.AddGrassCount();
                break;
        }
    }
}




