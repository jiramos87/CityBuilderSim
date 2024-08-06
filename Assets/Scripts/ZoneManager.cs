using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{
    public List<GameObject> residentialPrefabs;
    public List<GameObject> commercialPrefabs;
    public List<GameObject> industrialPrefabs;
    public List<GameObject> roadPrefabs;
    public List<GameObject> grassPrefabs;

    private Dictionary<Zone.ZoneType, List<GameObject>> zonePrefabs;

    void Start()
    {
        zonePrefabs = new Dictionary<Zone.ZoneType, List<GameObject>>
        {
            { Zone.ZoneType.Residential, residentialPrefabs },
            { Zone.ZoneType.Commercial, commercialPrefabs },
            { Zone.ZoneType.Industrial, industrialPrefabs },
            { Zone.ZoneType.Road, roadPrefabs },
            { Zone.ZoneType.Grass, grassPrefabs }
        };
    }

    public GameObject GetRandomZonePrefab(Zone.ZoneType zoneType)
    {
        List<GameObject> prefabs = zonePrefabs[zoneType];
        return prefabs[Random.Range(0, prefabs.Count)];
    }
}