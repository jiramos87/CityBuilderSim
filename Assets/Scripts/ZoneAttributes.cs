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

    public static readonly ZoneAttributes Residential = new ZoneAttributes
    (
        constructionCost: 100,
        population: 10,
        happiness: 5,
        powerConsumption: 100
    );

    public static readonly ZoneAttributes Commercial = new ZoneAttributes
    (
        constructionCost: 200,
        population: 0,
        happiness: 10,
        powerConsumption: 200
    );

    public static readonly ZoneAttributes Industrial = new ZoneAttributes
    (
        constructionCost: 300,
        population: 0,
        happiness: 0,
        powerConsumption: 300
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
        happiness: 10,
        powerConsumption: 0
    );
}