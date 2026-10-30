public class Stock : Investment
{
    private decimal _annualDividendPerShare;

    public Stock(string symbol, int quantity, decimal purchasePrice, decimal currentPrice, decimal annualDividendPerShare)
        : base(symbol, quantity, purchasePrice, currentPrice)
    {
        _annualDividendPerShare = annualDividendPerShare;
    }

    public override decimal GetAnnualIncome()
    {
        return GetQuantity() * _annualDividendPerShare;
    }

    public override string GetInvestmentType() { return "Stock"; }
}
