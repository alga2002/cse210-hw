public class Portfolio
{
    private List<Investment> _investments = new List<Investment>();
    private List<Transaction> _transactions = new List<Transaction>();

    public void AddInvestment(Investment investment)
    {
        _investments.Add(investment);
    }

    public void AddTransaction(Transaction transaction)
    {
        _transactions.Add(transaction);
    }

    public List<Investment> GetInvestments()
    {
        return new List<Investment>(_investments);
    }

    public List<Transaction> GetTransactions()
    {
        return new List<Transaction>(_transactions);
    }

    public decimal GetTotalCost()
    {
        decimal total = 0;
        foreach (Investment investment in _investments) total += investment.GetCost();
        return total;
    }

    public decimal GetTotalValue()
    {
        decimal total = 0;
        foreach (Investment investment in _investments) total += investment.GetCurrentValue();
        return total;
    }

    public decimal GetTotalAnnualIncome()
    {
        decimal total = 0;
        foreach (Investment investment in _investments) total += investment.GetAnnualIncome();
        return total;
    }
}
