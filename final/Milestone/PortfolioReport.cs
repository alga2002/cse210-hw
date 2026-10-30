public class PortfolioReport
{
    public void Display(Investor investor)
    {
        Portfolio portfolio = investor.GetPortfolio();
        decimal totalValue = portfolio.GetTotalValue();
        decimal totalCost = portfolio.GetTotalCost();

        Console.WriteLine($"\nPortfolio for {investor.GetName()}");
        Console.WriteLine("Type  Symbol  Cost       Value      Gain/Loss  Allocation  Annual Income");

        foreach (Investment investment in portfolio.GetInvestments())
        {
            decimal allocation = totalValue == 0 ? 0 : investment.GetCurrentValue() / totalValue * 100;
            Console.WriteLine($"{investment.GetInvestmentType(),-5} {investment.GetSymbol(),-7} {investment.GetCost(),9:C} {investment.GetCurrentValue(),10:C} {investment.GetGainLoss(),10:C} {allocation,9:F1}% {investment.GetAnnualIncome(),13:C}");
        }

        decimal gain = totalValue - totalCost;
        decimal returnPercent = totalCost == 0 ? 0 : gain / totalCost * 100;
        Console.WriteLine($"\nTotal invested: {totalCost:C}");
        Console.WriteLine($"Current value: {totalValue:C}");
        Console.WriteLine($"Gain/loss: {gain:C} ({returnPercent:F2}%)");
        Console.WriteLine($"Estimated annual income: {portfolio.GetTotalAnnualIncome():C}");
    }
}
