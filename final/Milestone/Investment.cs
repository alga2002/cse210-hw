public abstract class Investment
{
    private string _symbol;
    private int _quantity;
    private decimal _purchasePrice;
    private decimal _currentPrice;

    public Investment(string symbol, int quantity, decimal purchasePrice, decimal currentPrice)
    {
        _symbol = symbol;
        _quantity = quantity;
        _purchasePrice = purchasePrice;
        _currentPrice = currentPrice;
    }

    public string GetSymbol() { return _symbol; }
    public int GetQuantity() { return _quantity; }
    public decimal GetCost() { return _quantity * _purchasePrice; }
    public decimal GetCurrentValue() { return _quantity * _currentPrice; }
    public decimal GetGainLoss() { return GetCurrentValue() - GetCost(); }
    public abstract decimal GetAnnualIncome();
    public abstract string GetInvestmentType();
}
