public class ETF : Investment
{
    private decimal _annualDistributionPerShare;

    public ETF(string symbol, int quantity, decimal purchasePrice, decimal currentPrice, decimal annualDistributionPerShare)
        : base(symbol, quantity, purchasePrice, currentPrice)
    {
        _annualDistributionPerShare = annualDistributionPerShare;
    }

    public override decimal GetAnnualIncome() { return GetQuantity() * _annualDistributionPerShare; }
    public override decimal GetIncomePerUnit() { return _annualDistributionPerShare; }
    public override string GetInvestmentType() { return "ETF"; }
    public override string GetRiskLevel() { return "Varies"; }
}
