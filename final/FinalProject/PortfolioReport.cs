using System;
using System.Collections.Generic;

public class PortfolioReport
{
    public void Display(Investor investor)
    {
        Portfolio portfolio = investor.GetPortfolio();
        decimal totalValue = portfolio.GetTotalValue();
        decimal totalCost = portfolio.GetTotalCost();

        Console.WriteLine($"\nPortfolio for {investor.GetName()}");
        Console.WriteLine("Type  Symbol    Qty       Cost       Value     Gain/Loss  Allocation  Annual Income");
        foreach (Investment investment in portfolio.GetInvestments())
        {
            decimal allocation = totalValue == 0 ? 0 : investment.GetCurrentValue() / totalValue * 100;
            Console.WriteLine($"{investment.GetInvestmentType(),-5} {investment.GetSymbol(),-8} {investment.GetQuantity(),3} {investment.GetCost(),11:C} {investment.GetCurrentValue(),11:C} {investment.GetGainLoss(),11:C} {allocation,9:F1}% {investment.GetAnnualIncome(),13:C}");
        }

        decimal gain = portfolio.GetTotalGainLoss();
        decimal returnPercent = totalCost == 0 ? 0 : gain / totalCost * 100;
        Console.WriteLine($"\nTotal invested: {totalCost:C}");
        Console.WriteLine($"Current value: {totalValue:C}");
        Console.WriteLine($"Gain/loss: {gain:C} ({returnPercent:F2}%)");
        Console.WriteLine($"Estimated annual income: {portfolio.GetTotalAnnualIncome():C}");
        Console.WriteLine("Note: Return excludes distributions received and fees.");
    }

    public void DisplayPurchaseHistory(Portfolio portfolio)
    {
        Console.WriteLine("\nPurchase History");
        List<Transaction> transactions = portfolio.GetTransactions();
        if (transactions.Count == 0) Console.WriteLine("No purchases recorded.");
        foreach (Transaction transaction in transactions) Console.WriteLine(transaction.GetSummary());
    }

    public void DisplayDiversification(Portfolio portfolio)
    {
        Console.WriteLine("\nDiversification by Investment Type");
        decimal total = portfolio.GetTotalValue();
        string[] types = { "Stock", "ETF", "Bond" };
        foreach (string type in types)
        {
            decimal value = 0;
            foreach (Investment investment in portfolio.GetInvestments())
            {
                if (investment.GetInvestmentType() == type) value += investment.GetCurrentValue();
            }
            decimal percent = total == 0 ? 0 : value / total * 100;
            Console.WriteLine($"{type,-6} {value,12:C}  {percent,6:F1}%");
        }
        Console.WriteLine("Allocation uses CURRENT market values.");
    }

    public void DisplayRiskSummary(Portfolio portfolio)
    {
        Console.WriteLine("\nIllustrative Risk Summary");
        Console.WriteLine("Symbol    Type    General risk note");
        foreach (Investment investment in portfolio.GetInvestments())
        {
            Console.WriteLine($"{investment.GetSymbol(),-9} {investment.GetInvestmentType(),-7} {investment.GetRiskLevel()}");
        }
        Console.WriteLine("These are broad asset-type labels, NOT a personalized risk score.");
        Console.WriteLine("Stock risk varies by company; ETF/bond risk varies by holdings and terms.");
    }
}
