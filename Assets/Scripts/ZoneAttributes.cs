using UnityEngine;

public class ZoneAttributes
{
    public int Money { get; set; }
    public int Population { get; set; }
    public int Happiness { get; set; }
    public int Cost { get; set; }

    void Start()
    {
        // Initialize zone-specific properties
    }

    public ZoneAttributes(int money, int population, int happiness, int cost)
    {
        Money = money;
        Population = population;
        Happiness = happiness;
        Cost = cost;
    }
}