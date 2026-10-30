
using System;

class Program
{
    static void Main(string[] args)
    {
        // EXCEEDING REQUIREMENTS:
        // I added a leveling system that gives the user a new
        // level for every 500 points earned. The program also
        // displays a special message when the user levels up.

        GoalManager manager = new GoalManager();
        string choice = "";

        while (choice != "7")
        {
            Console.WriteLine();
            manager.DisplayScore();

            Console.WriteLine();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Create New Goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Save Goals");
            Console.WriteLine("  4. Load Goals");
            Console.WriteLine("  5. Record Event");
            Console.WriteLine("  6. Display Score");
            Console.WriteLine("  7. Quit");
            Console.Write("Select a choice from the menu: ");

            choice = Console.ReadLine();

            if (choice == "1")
            {
                CreateGoal(manager);
            }
            else if (choice == "2")
            {
                Console.WriteLine();
                Console.WriteLine("Your Goals:");
                manager.DisplayGoals();
            }
            else if (choice == "3")
            {
                Console.Write("What is the filename? ");
                string filename = Console.ReadLine();
                manager.SaveGoals(filename);
            }
            else if (choice == "4")
            {
                Console.Write("What is the filename? ");
                string filename = Console.ReadLine();
                manager.LoadGoals(filename);
            }
            else if (choice == "5")
            {
                Console.WriteLine("Your Goals:");
                manager.DisplayGoals();

                Console.Write("Which goal did you accomplish? ");

                if (int.TryParse(Console.ReadLine(), out int goalNumber))
                {
                    manager.RecordEvent(goalNumber);
                }
                else
                {
                    Console.WriteLine("Invalid goal number.");
                }
            }
            else if (choice == "6")
            {
                manager.DisplayScore();
            }
            else if (choice == "7")
            {
                Console.WriteLine("Goodbye!");
            }
            else
            {
                Console.WriteLine("Invalid option. Please try again.");
            }
        }
    }

    static void CreateGoal(GoalManager manager)
    {
        Console.WriteLine();
        Console.WriteLine("The types of Goals are:");
        Console.WriteLine("  1. Simple Goal");
        Console.WriteLine("  2. Eternal Goal");
        Console.WriteLine("  3. Checklist Goal");
        Console.Write("Which type of goal would you like to create? ");

        string type = Console.ReadLine();

        if (type != "1" && type != "2" && type != "3")
        {
            Console.WriteLine("Invalid goal type.");
            return;
        }

        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine();

        Console.Write("What is a short description of it? ");
        string description = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name) ||
            string.IsNullOrWhiteSpace(description) ||
            name.Contains("|") || description.Contains("|"))
        {
            Console.WriteLine("Name and description must not be empty or contain |.");
            return;
        }

        int points = GetPositiveNumber(
            "How many points is this goal worth? ");

        if (type == "1")
        {
            manager.AddGoal(new SimpleGoal(name, description, points));
        }
        else if (type == "2")
        {
            manager.AddGoal(new EternalGoal(name, description, points));
        }
        else if (type == "3")
        {
            int target = GetPositiveNumber(
                "How many times is this goal needed? ");

            int bonus = GetPositiveNumber(
                "What is the bonus for completing the goal? ");

            manager.AddGoal(new ChecklistGoal(
                name, description, points, target, bonus));
        }

        Console.WriteLine("Goal created successfully!");
    }

    static int GetPositiveNumber(string message)
    {
        while (true)
        {
            Console.Write(message);

            if (int.TryParse(Console.ReadLine(), out int number) &&
                number > 0)
            {
                return number;
            }

            Console.WriteLine("Please enter a positive whole number.");
        }
    }
}
