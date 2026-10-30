# Investment Portfolio Tracker — CSE 210 Final Project

A console application that tracks investments and reports their performance using sample or manually entered data. No market-data API or internet connection required.

## Requirements
- .NET 8 SDK
- Terminal (PowerShell, Command Prompt, or VS Code terminal)
- C# Dev Kit extension recommended.

## Run

### Option 1: Visual Studio Code (VS Code)

1. Open Visual Studio Code.
2. Select File → Open Folder.
3. Navigate to cse210-hw/final/FinalProject and open the folder.
4. Open the integrated terminal using Terminal → New Terminal.
5. Run the following commands:

dotnet build
dotnet run

The application will start in the terminal, where you can interact with the menu by typing a number and pressing Enter.

### Option 2: Visual Studio

1. Open Visual Studio.
2. Select Open a project or solution.
3. Navigate to cse210-hw/final/FinalProject.
4. Open the FinalProject.csproj file.
5. Wait for Visual Studio to load the project.
6. Press Ctrl + F5 or select Debug → Start Without Debugging.

The application will open in a console window where you can interact with the menu.

### Option 3: PowerShell or Command Prompt

Open a terminal inside the FinalProject folder containing FinalProject.csproj, then run:

dotnet build
dotnet run

Or from the repository root:

dotnet run --project final/FinalProject/FinalProject.csproj

## Using the application
At startup, three fictional investments (ACME stock, INDEX ETF, BOND bond) are preloaded. Users can explore the existing portfolio or add their own investments using manually entered financial information.

To navigate the application, enter the number corresponding to a menu option and press Enter.

1. **View portfolio report:** original cost, current value, gain/loss, percentage allocation, and projected annual income.
2. **Add investment:** choose Stock, ETF, or Bond; enter symbol, quantity, purchase price, current price, and estimated annual income per unit. This also adds a purchase to history.
3. **Update investment price:** enter a symbol and new current price. All purchase lots with the same symbol are updated.
4. **View purchase history:** displays saved purchases and their costs.
5. **View diversification analysis:** current values and allocation percentages by investment type.
6. **View risk summary:** general educational labels by investment type. Not personalized financial advice.
7. **Save portfolio:** write holdings, prices, income estimates, and purchase records to a text file, such as `portfolio.txt`.
8. **Load portfolio:** restore data from a previously saved file, replacing the current holdings and transactions.
9. **Quit:** exits the program.

## Saving and Loading Files

Files are saved relative to the application's working directory unless an absolute file path is provided.

For example, entering portfolio.txt will generally save the file in the directory where the program was launched.

1. Restart the application.
2. Select 8. Load Portfolio.
3. Enter the filename used when saving (for example, portfolio.txt).
4. The program will restore the saved investments and purchase history.

Note: The application uses manually entered market prices and does not retrieve live financial data. Estimated annual income is based on the values provided by the user.

## Financial calculations

The Investment Portfolio Tracker uses basic financial calculations to evaluate individual investments and overall portfolio performance.

- **Cost:** quantity × purchase price per unit.
- **Market value:** quantity × current price per unit.
- **Unrealized gain/loss:** market value − cost.
- **Price return (%):** total gain/loss ÷ total cost × 100 (when initial cost is nonzero).
- **Allocation (%):** holding/type market value ÷ total portfolio market value × 100.
- **Estimated annual income:** quantity × entered annual income per unit, calculated by each investment subclass.

### Additional Information

* Annual Income: Income is estimated using the values entered by the user. Each investment type calculates its projected annual income through its own implementation of the shared method.
* Investment Returns: The reported percentage return reflects changes in market prices only. It does not include dividends, distributions, or coupon payments.
* Excluded Costs: The program does not account for commissions, transaction fees, taxes, or other investment expenses.
* Market Information: Current prices must be entered manually. The application does not connect to financial markets or retrieve live prices.
* Risk Assessment: The risk classifications are general educational labels based on investment type, rather than statistically calculated risk measures.

## Object-Oriented Design

The Investment Portfolio Tracker is organized into eight classes, in addition to the main Program class. Each class has a specific responsibility, helping keep the application organized and easier to maintain.

- **Investment:** abstract base class with shared prices, quantities, and calculations.
- **Stock, ETF, Bond:** derived classes overriding income estimation and investment type/risk description.
- **Portfolio:** maintains investments and transactions, calculates totals, manages saving/loading.
- **Investor:** associates a name with a portfolio.
- **Transaction:** models a purchase record.
- **PortfolioReport:** formats investment, diversification, risk, and purchase reports.

### Object-Oriented Programming Principles

The program demonstrates the four main principles covered in CSE 210:

* Abstraction: Each class focuses on a specific responsibility, separating investment calculations, portfolio management, transactions, and reporting.
* Encapsulation: Internal attributes are kept private, while public methods allow other classes to access necessary information and functionality.
* Inheritance: The Stock, ETF, and Bond classes inherit common attributes and methods from the Investment base class, reducing repeated code.
* Polymorphism: Different investment classes override shared methods, such as GetAnnualIncome(), GetInvestmentType(), and GetRiskLevel(), allowing the program to handle different investment types through a common interface.

This structure makes it easier to expand the application by adding new investment types or financial analysis features without having to rewrite the entire program.

## Suggested tests

1. Select 1: starting invested cost **$4,000**, value **$4,185**, gain **$185**, projected annual income **$130**.
2. Select 2: add 2 shares of a Stock symbol `TEST` at $10 purchase/$12 current, annual dividend $1. New cost **$4,020**, market value **$4,209**, gain **$189**, annual income **$132**.
3. Select 3: update `TEST` to $15; expected portfolio market value **$4,215** and gain **$195**.
4. Select 4: verify the TEST purchase appears in history.
5. Select 5 and 6: verify allocation and risk outputs.
6. Select 7: save as `portfolio.txt`; quit, restart, select 8 and load; verify values and history are restored.
7. Select 9: verify the program exits cleanly.

## Notes

* Data Storage: The application uses a simple text-based file format for saving and loading portfolios. This approach was selected to keep the implementation straightforward and appropriate for CSE 210. Saved files should not be manually modified, as changes to their structure may cause loading errors.
* Investment Symbols: Investment symbols cannot contain the | character because it is used as a delimiter in the save file.
* Multiple Purchases: Multiple purchases of the same investment symbol are stored as separate holdings (purchase lots), allowing the program to maintain their individual purchase prices and quantities.
* Price Updates: Updating an investment's current price automatically updates all holdings with the same symbol.
* Market Data: The application does not connect to external financial services. Investment prices and annual income estimates must be entered manually.
* Educational Purpose: This project was developed to demonstrate object-oriented programming concepts and basic financial calculations. It is intended for educational use and does not provide financial recommendations.

These limitations reflect the project's intended scope as a console-based investment portfolio tracker rather than a professional trading or investment management platform.

## Future Improvements

While the current application focuses on fundamental portfolio tracking and object-oriented programming, several enhancements could be explored in future versions:

- **Live Market Data:** Integrate a financial API to retrieve current stock and ETF prices automatically.
- **Interactive Dashboard:** Develop a graphical interface to visualize portfolio performance and diversification.
- **Historical Performance:** Track investment values over time and display performance trends.
- **Advanced Risk Analysis:** Incorporate statistical measures such as volatility and portfolio concentration to provide more detailed risk insights.
