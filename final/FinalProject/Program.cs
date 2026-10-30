using System;
using System.Globalization;
using System.IO;

// CSE 210 Final Project - Open-ended Investment Portfolio Tracker

class Program
{
    static void Main(string[] args)
    {
        Portfolio portfolio = CreateSamplePortfolio();
        Investor investor = new Investor("Sample Investor", portfolio);
        PortfolioReport report = new PortfolioReport();
        string choice = "";

        while (choice != "9")
        {
            Console.WriteLine("\nINVESTMENT PORTFOLIO TRACKER");
            Console.WriteLine("1. View portfolio report");
            Console.WriteLine("2. Add investment");
            Console.WriteLine("3. Update investment price");
            Console.WriteLine("4. View purchase history");
            Console.WriteLine("5. View diversification analysis");
            Console.WriteLine("6. View risk summary");
            Console.WriteLine("7. Save portfolio");
            Console.WriteLine("8. Load portfolio");
            Console.WriteLine("9. Quit");
            Console.Write("Choose an option: ");
            choice = Console.ReadLine() ?? "";

            if (choice == "1") report.Display(investor);
            else if (choice == "2") AddInvestment(portfolio);
            else if (choice == "3") UpdatePrice(portfolio);
            else if (choice == "4") report.DisplayPurchaseHistory(portfolio);
            else if (choice == "5") report.DisplayDiversification(portfolio);
            else if (choice == "6") report.DisplayRiskSummary(portfolio);
            else if (choice == "7") SavePortfolio(portfolio, investor.GetName());
            else if (choice == "8") LoadPortfolio(ref portfolio, ref investor);
            else if (choice == "9") Console.WriteLine("Goodbye!");
            else Console.WriteLine("Please enter a number from 1 to 9.");
        }
    }

    static Portfolio CreateSamplePortfolio()
    {
        Portfolio portfolio = new Portfolio();
        portfolio.AddInvestment(new Stock("ACME", 10, 100m, 115m, 2m));
        portfolio.AddInvestment(new ETF("INDEX", 5, 200m, 215m, 4m));
        portfolio.AddInvestment(new Bond("BOND", 2, 1000m, 980m, 45m));
        portfolio.AddTransaction(new Transaction("ACME", new DateTime(2026, 10, 1), 10, 100m));
        portfolio.AddTransaction(new Transaction("INDEX", new DateTime(2026, 10, 2), 5, 200m));
        portfolio.AddTransaction(new Transaction("BOND", new DateTime(2026, 10, 3), 2, 1000m));
        return portfolio;
    }

    static void AddInvestment(Portfolio portfolio)
    {
        Console.WriteLine("\n1. Stock  2. ETF  3. Bond");
        Console.Write("Choose investment type: ");
        string type = Console.ReadLine() ?? "";
        if (type != "1" && type != "2" && type != "3")
        {
            Console.WriteLine("Invalid type.");
            return;
        }

        Console.Write("Enter ticker/symbol: ");
        string symbol = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();
        if (symbol.Length == 0 || symbol.Contains('|'))
        {
            Console.WriteLine("Symbol cannot be empty or contain |.");
            return;
        }

        int quantity = ReadPositiveInt("Quantity purchased: ");
        decimal purchasePrice = ReadNonNegativeDecimal("Purchase price per unit: ");
        decimal currentPrice = ReadNonNegativeDecimal("Current price per unit: ");
        decimal annualIncome = ReadNonNegativeDecimal("Estimated ANNUAL income per unit: ");

        Investment investment;
        if (type == "1") investment = new Stock(symbol, quantity, purchasePrice, currentPrice, annualIncome);
        else if (type == "2") investment = new ETF(symbol, quantity, purchasePrice, currentPrice, annualIncome);
        else investment = new Bond(symbol, quantity, purchasePrice, currentPrice, annualIncome);

        portfolio.AddInvestment(investment);
        portfolio.AddTransaction(new Transaction(symbol, DateTime.Today, quantity, purchasePrice));
        Console.WriteLine($"Added {quantity} {symbol}. Purchase recorded in history.");
        Console.WriteLine("Note: Repeat purchases are shown as separate lots in the report.");
    }

    static void UpdatePrice(Portfolio portfolio)
    {
        Console.Write("Symbol to update: ");
        string symbol = (Console.ReadLine() ?? "").Trim();
        decimal newPrice = ReadNonNegativeDecimal("New current price per unit: ");
        int updated = portfolio.UpdateInvestmentPrice(symbol, newPrice);
        if (updated == 0) Console.WriteLine("No holdings found with that symbol.");
        else Console.WriteLine($"Updated {updated} holding(s) for {symbol.ToUpperInvariant()}.");
    }

    static void SavePortfolio(Portfolio portfolio, string investorName)
    {
        Console.Write("Save filename (example: portfolio.txt): ");
        string filename = Console.ReadLine() ?? "";
        if (string.IsNullOrWhiteSpace(filename))
        {
            Console.WriteLine("Filename cannot be empty.");
            return;
        }
        try
        {
            portfolio.SaveToFile(filename, investorName);
            Console.WriteLine("Portfolio saved successfully.");
        }
        catch (Exception error) when (error is IOException || error is UnauthorizedAccessException || error is ArgumentException)
        {
            Console.WriteLine($"Save failed: {error.Message}");
        }
    }

    static void LoadPortfolio(ref Portfolio portfolio, ref Investor investor)
    {
        Console.Write("Load filename: ");
        string filename = Console.ReadLine() ?? "";
        try
        {
            string name;
            Portfolio loaded = Portfolio.LoadFromFile(filename, out name);
            portfolio = loaded;
            investor = new Investor(name, loaded);
            Console.WriteLine("Portfolio loaded successfully. Existing holdings were replaced.");
        }
        catch (Exception error) when (error is IOException || error is UnauthorizedAccessException || error is ArgumentException || error is FormatException || error is OverflowException)
        {
            Console.WriteLine($"Load failed: {error.Message}");
        }
    }

    static int ReadPositiveInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int number) && number > 0) return number;
            Console.WriteLine("Enter a whole number greater than zero.");
        }
    }

    static decimal ReadNonNegativeDecimal(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine() ?? "";
            if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.CurrentCulture, out decimal number) && number >= 0)
                return number;
            Console.WriteLine("Enter a valid amount of zero or greater (example: 125.50).");
        }
    }
}
