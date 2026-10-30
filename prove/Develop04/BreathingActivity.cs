
using System;

public class BreathingActivity : Activity
{
    public BreathingActivity() : base(
        "Breathing Activity",
        "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    public void Run()
    {
        DisplayStartingMessage();

        int duration = GetDuration();
        int elapsed = 0;

        while (elapsed < duration)
        {
            int breatheIn = Math.Min(4, duration - elapsed);

            Console.Write("Breathe in... ");
            ShowCountdown(breatheIn);
            elapsed += breatheIn;

            if (elapsed < duration)
            {
                int breatheOut = Math.Min(4, duration - elapsed);

                Console.Write("Breathe out... ");
                ShowCountdown(breatheOut);
                elapsed += breatheOut;
            }
        }

        DisplayEndingMessage();
    }
}
