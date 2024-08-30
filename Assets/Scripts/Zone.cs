using UnityEngine;

public class Zone : MonoBehaviour
{
    public enum ZoneType
    {
      ResidentialLightBuilding,
      ResidentialMediumBuilding,
      ResidentialHeavyBuilding,
      CommercialLightBuilding,
      CommercialMediumBuilding,
      CommercialHeavyBuilding,
      IndustrialLightBuilding,
      IndustrialMediumBuilding,
      IndustrialHeavyBuilding,
      ResidentialLightZoning,
      ResidentialMediumZoning,
      ResidentialHeavyZoning,
      CommercialLightZoning,
      CommercialMediumZoning,
      CommercialHeavyZoning,
      IndustrialLightZoning,
      IndustrialMediumZoning,
      IndustrialHeavyZoning,
      Road,
      Grass,
      None
    }
    public ZoneType zoneType;

    void Start()
    {
        // Initialize zone-specific properties
    }
}