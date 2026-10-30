public class Bond : Investment
{
    private decimal _annualCouponPerBond;

    public Bond(string symbol, int quantity, decimal purchasePrice, decimal currentPrice, decimal annualCouponPerBond)
        : base(symbol, quantity, purchasePrice, currentPrice)
    {
        _annualCouponPerBond = annualCouponPerBond;
    }

    public override decimal GetAnnualIncome()
    {
        return GetQuantity() * _annualCouponPerBond;
    }

    public override string GetInvestmentType() { return "Bond"; }
}
