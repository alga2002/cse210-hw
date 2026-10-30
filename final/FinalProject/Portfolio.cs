using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

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

    public List<Investment> GetInvestments() { return new List<Investment>(_investments); }
    public List<Transaction> GetTransactions() { return new List<Transaction>(_transactions); }

    public int UpdateInvestmentPrice(string symbol, decimal price)
    {
        int updated = 0;
        foreach (Investment investment in _investments)
        {
            if (investment.GetSymbol().Equals(symbol, StringComparison.OrdinalIgnoreCase))
            {
                investment.UpdatePrice(price);
                updated++;
            }
        }
        return updated;
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

    public decimal GetTotalGainLoss() { return GetTotalValue() - GetTotalCost(); }

    public decimal GetTotalAnnualIncome()
    {
        decimal total = 0;
        foreach (Investment investment in _investments) total += investment.GetAnnualIncome();
        return total;
    }

    // Simple text-file persistence; the program disallows '|' in symbols and investor names.
    public void SaveToFile(string filename, string investorName)
    {
        using (StreamWriter file = new StreamWriter(filename))
        {
            file.WriteLine("PortfolioTracker|1");
            file.WriteLine("Investor|" + investorName);
            foreach (Investment investment in _investments)
            {
                file.WriteLine(string.Join("|", "Asset", investment.GetInvestmentType(),
                    investment.GetSymbol(), investment.GetQuantity(),
                    investment.GetPurchasePrice().ToString(CultureInfo.InvariantCulture),
                    investment.GetCurrentPrice().ToString(CultureInfo.InvariantCulture),
                    investment.GetIncomePerUnit().ToString(CultureInfo.InvariantCulture)));
            }
            foreach (Transaction transaction in _transactions)
            {
                file.WriteLine(string.Join("|", "Purchase", transaction.GetSymbol(),
                    transaction.GetDate().ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    transaction.GetQuantity(),
                    transaction.GetPricePerUnit().ToString(CultureInfo.InvariantCulture)));
            }
        }
    }

    public static Portfolio LoadFromFile(string filename, out string investorName)
    {
        string[] lines = File.ReadAllLines(filename);
        if (lines.Length < 2 || lines[0] != "PortfolioTracker|1")
            throw new FormatException("This is not a valid Portfolio Tracker save file.");

        string[] owner = lines[1].Split('|');
        if (owner.Length != 2 || owner[0] != "Investor" || string.IsNullOrWhiteSpace(owner[1]))
            throw new FormatException("Invalid investor information.");

        Portfolio loaded = new Portfolio();
        for (int i = 2; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split('|');
            if (parts[0] == "Asset" && parts.Length == 7)
            {
                string type = parts[1];
                string symbol = parts[2];
                int quantity = int.Parse(parts[3], CultureInfo.InvariantCulture);
                decimal cost = decimal.Parse(parts[4], CultureInfo.InvariantCulture);
                decimal price = decimal.Parse(parts[5], CultureInfo.InvariantCulture);
                decimal income = decimal.Parse(parts[6], CultureInfo.InvariantCulture);
                if (string.IsNullOrWhiteSpace(symbol) || quantity <= 0 || cost < 0 || price < 0 || income < 0)
                    throw new FormatException("Invalid investment values.");

                Investment investment;
                if (type == "Stock") investment = new Stock(symbol, quantity, cost, price, income);
                else if (type == "ETF") investment = new ETF(symbol, quantity, cost, price, income);
                else if (type == "Bond") investment = new Bond(symbol, quantity, cost, price, income);
                else throw new FormatException("Unknown investment type.");
                loaded.AddInvestment(investment);
            }
            else if (parts[0] == "Purchase" && parts.Length == 5)
            {
                string symbol = parts[1];
                DateTime date = DateTime.ParseExact(parts[2], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                int quantity = int.Parse(parts[3], CultureInfo.InvariantCulture);
                decimal price = decimal.Parse(parts[4], CultureInfo.InvariantCulture);
                if (string.IsNullOrWhiteSpace(symbol) || quantity <= 0 || price < 0)
                    throw new FormatException("Invalid purchase values.");
                loaded.AddTransaction(new Transaction(symbol, date, quantity, price));
            }
            else
            {
                throw new FormatException("Invalid line in the save file.");
            }
        }
        investorName = owner[1];
        return loaded;
    }
}
