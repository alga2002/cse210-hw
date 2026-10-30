
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // EXCEEDING REQUIREMENTS:
        // 1. The program randomly selects from three scriptures.
        // 2. The program only hides words that are still visible,
        //    so every time the user presses Enter, progress is made.

        List<Scripture> scriptures = new List<Scripture>();

        Reference reference1 = new Reference("John", 3, 16);
        Scripture scripture1 = new Scripture(reference1,
            "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life.");
        scriptures.Add(scripture1);

        Reference reference2 = new Reference("Proverbs", 3, 5, 6);
        Scripture scripture2 = new Scripture(reference2,
            "Trust in the LORD with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths.");
        scriptures.Add(scripture2);

        Reference reference3 = new Reference("Psalm", 23, 1);
        Scripture scripture3 = new Scripture(reference3,
            "The LORD is my shepherd; I shall not want.");
        scriptures.Add(scripture3);

        Random random = new Random();
        int index = random.Next(scriptures.Count);

        Scripture scripture = scriptures[index];

        string choice = "";

        while (choice.ToLower() != "quit" &&
               !scripture.IsCompletelyHidden())
        {
            Console.Clear();

            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();
            Console.WriteLine("Press Enter to hide words or type 'quit' to finish.");

            choice = Console.ReadLine();

            if (choice.ToLower() != "quit")
            {
                scripture.HideRandomWords(3);
            }
        }

        Console.Clear();
        Console.WriteLine(scripture.GetDisplayText());
        Console.WriteLine();
        Console.WriteLine("Program finished!");
    }
}
