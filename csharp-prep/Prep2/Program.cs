using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter your grade value: ");
        int grade_value = int.Parse(Console.ReadLine());

        string letter;
        string sign = "";

        if (grade_value >= 90)
        {
            letter = "A";
        }
        else if (grade_value >= 80)
        {
            letter = "B";
        }
        else if (grade_value >= 70)
        {
            letter = "C";
        }
        else if (grade_value >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        int last_digit = grade_value % 10;

        if (last_digit >= 7)
        {
            sign = "+";
        }
        else if (last_digit < 3)
        {
            sign = "-";
        }

        if (letter == "A" && sign == "+")
        {
            sign = "";
        }

        if (letter == "F")
        {
            sign = "";
        }

        Console.WriteLine($"Your grade is {letter}{sign}.");

        if (grade_value >= 70)
        {
            Console.WriteLine("Congratulations! You passed the course.");
        }
        else
        {
            Console.WriteLine("Keep working hard. You can do better next time!");
        }
    }
}