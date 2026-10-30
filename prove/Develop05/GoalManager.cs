
using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;

    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
    }

    public void DisplayScore()
    {
        Console.WriteLine($"You have {_score} points.");
        Console.WriteLine($"Current level: {GetLevel()}");
    }

    public int GetLevel()
    {
        return (_score / 500) + 1;
    }

    public void DisplayGoals()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals have been created yet.");
            return;
        }

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
    }

    public void RecordEvent(int goalNumber)
    {
        if (goalNumber < 1 || goalNumber > _goals.Count)
        {
            Console.WriteLine("Invalid goal number.");
            return;
        }

        Goal goal = _goals[goalNumber - 1];

        if (goal.IsComplete())
        {
            Console.WriteLine("This goal is already complete.");
            return;
        }

        int oldLevel = GetLevel();
        int earnedPoints = goal.RecordEvent();

        _score += earnedPoints;

        Console.WriteLine($"Congratulations! You earned {earnedPoints} points!");
        Console.WriteLine($"Your total score is now {_score}.");

        if (GetLevel() > oldLevel)
        {
            Console.WriteLine($"LEVEL UP! You reached level {GetLevel()}!");
        }
    }

    public void SaveGoals(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            outputFile.WriteLine(_score);

            foreach (Goal goal in _goals)
            {
                outputFile.WriteLine(goal.GetStringRepresentation());
            }
        }

        Console.WriteLine("Goals saved successfully!");
    }

    public void LoadGoals(string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        string[] lines = File.ReadAllLines(filename);

        if (lines.Length == 0 || !int.TryParse(lines[0], out int loadedScore))
        {
            Console.WriteLine("Invalid save file.");
            return;
        }

        List<Goal> loadedGoals = new List<Goal>();

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split('|');

            if (parts.Length < 4)
            {
                Console.WriteLine("Invalid save file.");
                return;
            }

            string type = parts[0];
            string name = parts[1];
            string description = parts[2];

            if (!int.TryParse(parts[3], out int points))
            {
                Console.WriteLine("Invalid save file.");
                return;
            }

            if (type == "Simple" && parts.Length == 5)
            {
                if (!bool.TryParse(parts[4], out bool completed))
                {
                    Console.WriteLine("Invalid save file.");
                    return;
                }

                loadedGoals.Add(new SimpleGoal(
                    name, description, points, completed));
            }
            else if (type == "Eternal" && parts.Length == 4)
            {
                loadedGoals.Add(new EternalGoal(
                    name, description, points));
            }
            else if (type == "Checklist" && parts.Length == 7)
            {
                if (!int.TryParse(parts[4], out int target) ||
                    !int.TryParse(parts[5], out int bonus) ||
                    !int.TryParse(parts[6], out int amountDone) ||
                    target <= 0 || amountDone < 0 || amountDone > target)
                {
                    Console.WriteLine("Invalid save file.");
                    return;
                }

                loadedGoals.Add(new ChecklistGoal(
                    name, description, points, target, bonus, amountDone));
            }
            else
            {
                Console.WriteLine("Invalid save file.");
                return;
            }
        }

        _score = loadedScore;
        _goals = loadedGoals;

        Console.WriteLine("Goals loaded successfully!");
    }
}
