// CSE 210 Final Project - Open-ended milestone
class Program
{
    static void Main(string[] args)
    {
        Portfolio portfolio = new Portfolio();
        portfolio.AddInvestment(new Stock("ACME", 10, 100m, 115m, 2m));
        portfolio.AddInvestment(new ETF("INDEX", 5, 200m, 215m, 4m));
        portfolio.AddInvestment(new Bond("BOND", 2, 1000m, 980m, 45m));
        portfolio.AddTransaction(new Transaction("ACME", new DateTime(2026, 10, 1), 10, 100m));
        portfolio.AddTransaction(new Transaction("INDEX", new DateTime(2026, 10, 2), 5, 200m));
        portfolio.AddTransaction(new Transaction("BOND", new DateTime(2026, 10, 3), 2, 1000m));

        Investor investor = new Investor("Sample Investor", portfolio);
        PortfolioReport report = new PortfolioReport();
        string choice = "";

        while (choice != "3")
        {
            Console.WriteLine("\nInvestment Portfolio Tracker");
            Console.WriteLine("1. View portfolio report");
            Console.WriteLine("2. View purchase history");
            Console.WriteLine("3. Quit");
            Console.Write("Choose an option: ");
            choice = Console.ReadLine() ?? "";

            if (choice == "1")
            {
                report.Display(investor);
            }
            else if (choice == "2")
            {
                foreach (Transaction transaction in portfolio.GetTransactions())
                {
                    Console.WriteLine(transaction.GetSummary());
                }
            }
            else if (choice == "3")
            {
                Console.WriteLine("Goodbye!");
            }
            else
            {
                Console.WriteLine("Please choose 1, 2, or 3.");
            }
        }
    }
}
