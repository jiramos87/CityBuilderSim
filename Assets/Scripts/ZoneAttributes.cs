using UnityEngine;

public class ZoneAttributes
{
    public int ConstructionCost { get; set; }
    public int Population { get; set; }
    public int Happiness { get; set; }
    public int PowerConsumption { get; set; }

    void Start()
    {
        // Initialize zone-specific properties
    }

    public ZoneAttributes(int constructionCost, int population, int happiness, int powerConsumption)
    {
        ConstructionCost = constructionCost;
        Population = population;
        Happiness = happiness;
        PowerConsumption = powerConsumption;
    }

    public static readonly ZoneAttributes ResidentialLightZoning = new ZoneAttributes
    (
        constructionCost: 2,
        population: 0,
        happiness: 0,
        powerConsumption: 0
    );

    public static readonly ZoneAttributes CommercialLightZoning = new ZoneAttributes
    (
        constructionCost: 2,
        population: 0,
        happiness: 0,
        powerConsumption: 0
    );

    public static readonly ZoneAttributes IndustrialLightZoning = new ZoneAttributes
    (
        constructionCost: 2,
        population: 0,
        happiness: 0,
        powerConsumption: 0
    );

    public static readonly ZoneAttributes ResidentialMediumZoning = new ZoneAttributes
    (
        constructionCost: 4,
        population: 0,
        happiness: 0,
        powerConsumption: 0
    );

    public static readonly ZoneAttributes CommercialMediumZoning = new ZoneAttributes
    (
        constructionCost: 4,
        population: 0,
        happiness: 0,
        powerConsumption: 0
    );

    public static readonly ZoneAttributes IndustrialMediumZoning = new ZoneAttributes
    (
        constructionCost: 5,
        population: 0,
        happiness: 0,
        powerConsumption: 0
    );

    public static readonly ZoneAttributes ResidentialHeavyZoning = new ZoneAttributes
    (
        constructionCost: 6,
        population: 0,
        happiness: 0,
        powerConsumption: 0
    );

    public static readonly ZoneAttributes CommercialHeavyZoning = new ZoneAttributes
    (
        constructionCost: 7,
        population: 0,
        happiness: 0,
        powerConsumption: 0
    );

    public static readonly ZoneAttributes IndustrialHeavyZoning = new ZoneAttributes
    (
        constructionCost: 8,
        population: 0,
        happiness: 0,
        powerConsumption: 0
    );

    public static readonly ZoneAttributes ResidentialLightBuilding = new ZoneAttributes
    (
        constructionCost: 0,
        population: 5,
        happiness: 100,
        powerConsumption: 10
    );

    public static readonly ZoneAttributes CommercialLightBuilding = new ZoneAttributes
    (
        constructionCost: 0,
        population: 0,
        happiness: 100,
        powerConsumption: 40
    );

    public static readonly ZoneAttributes IndustrialLightBuilding = new ZoneAttributes
    (
        constructionCost: 0,
        population: 0,
        happiness: 5,
        powerConsumption: 60
    );

    public static readonly ZoneAttributes ResidentialMediumBuilding = new ZoneAttributes
    (
        constructionCost: 0,
        population: 20,
        happiness: 100,
        powerConsumption: 40
    );

    public static readonly ZoneAttributes CommercialMediumBuilding = new ZoneAttributes
    (
        constructionCost: 0,
        population: 0,
        happiness: 100,
        powerConsumption: 80
    );

    public static readonly ZoneAttributes IndustrialMediumBuilding = new ZoneAttributes
    (
        constructionCost: 0,
        population: 0,
        happiness: 100,
        powerConsumption: 120
    );

    public static readonly ZoneAttributes ResidentialHeavyBuilding = new ZoneAttributes
    (
        constructionCost: 0,
        population: 50,
        happiness: 100,
        powerConsumption: 80
    );

    public static readonly ZoneAttributes CommercialHeavyBuilding = new ZoneAttributes
    (
        constructionCost: 0,
        population: 0,
        happiness: 100,
        powerConsumption: 160
    );

    public static readonly ZoneAttributes IndustrialHeavyBuilding = new ZoneAttributes
    (
        constructionCost: 0,
        population: 0,
        happiness: 100,
        powerConsumption: 240
    );

    public static readonly ZoneAttributes Road = new ZoneAttributes
    (
        constructionCost: 50,
        population: 0,
        happiness: 0,
        powerConsumption: 100
    );

    public static readonly ZoneAttributes Grass = new ZoneAttributes
    (
        constructionCost: 0,
        population: 0,
        happiness: 0,
        powerConsumption: 0
    );
}