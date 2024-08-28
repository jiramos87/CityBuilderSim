using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public CityStats cityStats;
    public TimeManager timeManager;

    public int residentialIncomeTax = 10;
    public int commercialIncomeTax = 10;
    public int industrialIncomeTax = 10;
    public void ProcessDailyEconomy()
    {
        // Add daily calculations if needed
        if (timeManager.GetCurrentDate().Day == 1)
        {
            ProcessMonthlyEconomy();
        }
    }

    private void ProcessMonthlyEconomy()
    {
        int residentialIncome = cityStats.residentialZoneCount * residentialIncomeTax;
        int commercialIncome = cityStats.commercialZoneCount * commercialIncomeTax;
        int industrialIncome = cityStats.industrialZoneCount * industrialIncomeTax;

        cityStats.AddMoney(residentialIncome + commercialIncome + industrialIncome);
    }

    public void RaiseResidentialTax()
    {
        residentialIncomeTax += 1;
    }

    public void LowerResidentialTax()
    {
        residentialIncomeTax -= 1;
    }

    public void RaiseCommercialTax()
    {
        commercialIncomeTax += 1;
    }

    public void LowerCommercialTax()
    {
        commercialIncomeTax -= 1;
    }

    public void RaiseIndustrialTax()
    {
        industrialIncomeTax += 1;
    }

    public void LowerIndustrialTax()
    {
        industrialIncomeTax -= 1;
    }

    public int GetResidentialTax()
    {
        return residentialIncomeTax;
    }

    public int GetCommercialTax()
    {
        return commercialIncomeTax;
    }

    public int GetIndustrialTax()
    {
        return industrialIncomeTax;
    }
}