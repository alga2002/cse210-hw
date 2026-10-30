
using System;
using System.Collections.Generic;

public class ReflectingActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> _questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different?",
        "What is your favorite thing about this experience?",
        "What did you learn about yourself?",
        "How can you use this experience in the future?"
    };

    private Random _random = new Random();

    public ReflectingActivity() : base(
        "Reflecting Activity",
        "This activity will help you reflect on times in your life when you have shown strength and resilience.")
    {
    }

    public void Run()
    {
        DisplayStartingMessage();

        int promptIndex = _random.Next(_prompts.Count);

        Console.WriteLine("Consider the following prompt:");
        Console.WriteLine();
        Console.WriteLine(_prompts[promptIndex]);
        Console.WriteLine();

        Console.WriteLine("When you have something in mind, press Enter to continue.");
        Console.ReadLine();

        Console.WriteLine("Now reflect on the following questions:");
        Console.WriteLine();
        Console.Write("You may begin in: ");
        ShowCountdown(3);

        DateTime endTime = DateTime.Now.AddSeconds(GetDuration());

        while (DateTime.Now < endTime)
        {
            int questionIndex = _random.Next(_questions.Count);

            Console.WriteLine();
            Console.WriteLine($"> {_questions[questionIndex]}");

            int remaining = (int)Math.Ceiling(
                (endTime - DateTime.Now).TotalSeconds);

            ShowSpinner(Math.Min(5, remaining));
        }

        DisplayEndingMessage();
    }
}
