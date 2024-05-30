using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Initialize the state of the forest
        string forestState = "Normal";

        // Define the dance moves for Lox and Drako
        string[] loxMoves = { "Twirl", "Leap", "Spin", "Twirl", "Leap" };
        string[] drakoMoves = { "Spin", "Twirl", "Leap", "Leap", "Spin" };

        // Loop through each sequence
        for (int i = 0; i < 5; i++)
        {
            // Determine the effect of the combined dance moves
            string effect = GetEffect(loxMoves[i], drakoMoves[i]);

            // Update the state of the forest
            forestState = UpdateForestState(forestState, effect);

            // Display the state of the forest after each sequence
            Console.WriteLine($"After sequence {i + 1}, the forest is: {forestState}");
        }

        // Display the final state of the forest
        Console.WriteLine($"After the dance, the final state of the forest is: {forestState}");
    }

    static string GetEffect(string loxMove, string drakoMove)
    {
        if (loxMove == "Twirl" && drakoMove == "Twirl")
        {
            return "Fireflies";
        }
        else if ((loxMove == "Leap" && drakoMove == "Spin") || (loxMove == "Spin" && drakoMove == "Leap"))
        {
            return "Rain";
        }
        else
        {
            return "Rainbow";
        }
    }

    static string UpdateForestState(string currentForestState, string effect)
    {
        // Here, we simply append the effect to the current state.
        // In a more complex application, you might want to use a more sophisticated method to update the state.
        return currentForestState + " + " + effect;
    }
}