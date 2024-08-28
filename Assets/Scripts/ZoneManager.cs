using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{
    public List<GameObject> lightResidentialBuildingPrefabs;
    public List<GameObject> mediumResidentialBuildingPrefabs;
    public List<GameObject> heavyResidentialBuildingPrefabs;

    public List<GameObject> lightCommercialBuildingPrefabs;
    public List<GameObject> mediumCommercialBuildingPrefabs;
    public List<GameObject> heavyCommercialBuildingPrefabs;

    public List<GameObject> lightIndustrialBuildingPrefabs;
    public List<GameObject> mediumIndustrialBuildingPrefabs;
    public List<GameObject> heavyIndustrialBuildingPrefabs;

    public List<GameObject> residentialLightZoningPrefabs;
    public List<GameObject> residentialMediumZoningPrefabs;
    public List<GameObject> residentialHeavyZoningPrefabs;

    public List<GameObject> commercialLightZoningPrefabs;
    public List<GameObject> commercialMediumZoningPrefabs;
    public List<GameObject> commercialHeavyZoningPrefabs;

    public List<GameObject> industrialLightZoningPrefabs;
    public List<GameObject> industrialMediumZoningPrefabs;
    public List<GameObject> industrialHeavyZoningPrefabs;

    public List<GameObject> roadPrefabs;
    public List<GameObject> grassPrefabs;

    private Dictionary<Zone.ZoneType, List<GameObject>> zonePrefabs;

    void Start()
    {
        zonePrefabs = new Dictionary<Zone.ZoneType, List<GameObject>>
        {
            { Zone.ZoneType.ResidentialLightBuilding, lightResidentialBuildingPrefabs },
            { Zone.ZoneType.ResidentialMediumBuilding, mediumResidentialBuildingPrefabs },
            { Zone.ZoneType.ResidentialHeavyBuilding, heavyResidentialBuildingPrefabs },
            { Zone.ZoneType.CommercialLightBuilding, lightCommercialBuildingPrefabs },
            { Zone.ZoneType.CommercialMediumBuilding, mediumCommercialBuildingPrefabs },
            { Zone.ZoneType.CommercialHeavyBuilding, heavyCommercialBuildingPrefabs },
            { Zone.ZoneType.IndustrialLightBuilding, lightIndustrialBuildingPrefabs },
            { Zone.ZoneType.IndustrialMediumBuilding, mediumIndustrialBuildingPrefabs },
            { Zone.ZoneType.IndustrialHeavyBuilding, heavyIndustrialBuildingPrefabs },
            { Zone.ZoneType.ResidentialLightZoning, residentialLightZoningPrefabs },
            { Zone.ZoneType.ResidentialMediumZoning, residentialMediumZoningPrefabs },
            { Zone.ZoneType.ResidentialHeavyZoning, residentialHeavyZoningPrefabs },
            { Zone.ZoneType.CommercialLightZoning, commercialLightZoningPrefabs },
            { Zone.ZoneType.CommercialMediumZoning, commercialMediumZoningPrefabs },
            { Zone.ZoneType.CommercialHeavyZoning, commercialHeavyZoningPrefabs },
            { Zone.ZoneType.IndustrialLightZoning, industrialLightZoningPrefabs },
            { Zone.ZoneType.IndustrialMediumZoning, industrialMediumZoningPrefabs },
            { Zone.ZoneType.IndustrialHeavyZoning, industrialHeavyZoningPrefabs },
            { Zone.ZoneType.Road, roadPrefabs },
            { Zone.ZoneType.Grass, grassPrefabs }
        };
    }

    public GameObject GetRandomZonePrefab(Zone.ZoneType zoneType)
    {
        List<GameObject> prefabs = zonePrefabs[zoneType];

        if (prefabs.Count == 0)
        {
            return null;
        }

        GameObject prefab = prefabs[Random.Range(0, prefabs.Count)];

        return prefab;
    }
}