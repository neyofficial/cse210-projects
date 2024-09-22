using System;

class Program
{
    static void Main(string[] args)
    {
        string playAgain = "yes"; // Variable to store if the user wants to play again

        while (playAgain.ToLower() == "yes")
        {
            // Generate a random number between 1 and 100
            Random random = new Random();
            int magicNumber = random.Next(1, 101);

            int guess = -1; // Initialize guess to an impossible value
            int guessCount = 0; // Initialize the guess count

            // Keep looping until the guess matches the magic number
            while (guess != magicNumber)
            {
                // Ask for the user's guess
                Console.Write("What is your guess? ");
                guess = int.Parse(Console.ReadLine());
                guessCount++; // Increment the guess count

                // Compare the guess with the magic number
                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine($"You guessed it in {guessCount} attempts!");
                }
            }

            // Ask the user if they want to play again
            Console.Write("Do you want to play again? (yes/no): ");
            playAgain = Console.ReadLine();
        }

        // End message
        Console.WriteLine("Thanks for playing!");
    }
}
