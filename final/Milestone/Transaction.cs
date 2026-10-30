public class Transaction
{
    private string _symbol;
    private DateTime _date;
    private int _quantity;
    private decimal _pricePerUnit;

    public Transaction(string symbol, DateTime date, int quantity, decimal pricePerUnit)
    {
        _symbol = symbol;
        _date = date;
        _quantity = quantity;
        _pricePerUnit = pricePerUnit;
    }

    public string GetSummary()
    {
        return $"{_date:d} - Bought {_quantity} {_symbol} at {_pricePerUnit:C} each";
    }
}
