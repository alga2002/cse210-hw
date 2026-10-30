public class Investor
{
    private string _name;
    private Portfolio _portfolio;

    public Investor(string name, Portfolio portfolio)
    {
        _name = name;
        _portfolio = portfolio;
    }

    public string GetName() { return _name; }
    public Portfolio GetPortfolio() { return _portfolio; }
}
