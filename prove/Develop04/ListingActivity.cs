
using System;
using System.Collections.Generic;
using System.Threading;

public class ListingActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are some of your personal strengths?",
        "Who are people that you have helped this week?",
        "What are things that make you happy?",
        "Who are some of your personal heroes?"
    };

    private Random _random = new Random();

    public ListingActivity() : base(
        "Listing Activity",
        "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    public void Run()
    {
        DisplayStartingMessage();

        int index = _random.Next(_prompts.Count);

        Console.WriteLine("List as many responses as you can to the following prompt:");
        Console.WriteLine();
        Console.WriteLine(_prompts[index]);
        Console.WriteLine();

        Console.Write("You may begin in: ");
        ShowCountdown(5);

        DateTime endTime = DateTime.Now.AddSeconds(GetDuration());
        int count = 0;
        string response = "";

        Console.Write("> ");

        while (DateTime.Now < endTime)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();

                    if (!string.IsNullOrWhiteSpace(response))
                    {
                        count++;
                    }

                    response = "";
                    Console.Write("> ");
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (response.Length > 0)
                    {
                        response = response.Substring(0, response.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    response += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
            }
            else
            {
                Thread.Sleep(50);
            }
        }

        Console.WriteLine();
        Console.WriteLine($"You listed {count} items!");

        DisplayEndingMessage();
    }
}
