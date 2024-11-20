using System;

namespace Guessing_Game
{
    public class GuessGame
    {
        private readonly Random random = new Random();

        public GuessGame()
        {
            SecretNumber = GenerateSecretNumber();
            Attempts = 0;
            Score = 50;
            MaxAttempts = 5;
            HasWon = false;
        }

        public int SecretNumber { get; private set; }
        public int PlayerGuess { get; private set; }
        public int Attempts { get; private set; }
        public int MaxAttempts { get; private set; }
        public int Score { get; private set; }
        public bool HasWon { get; private set; }

        private int GenerateSecretNumber()
        {
            return random.Next(1, 100); // Generates a number between 1 and 99
        }

        public void Play()
        {
            Console.WriteLine("Welcome to the Guessing Game!");
            Console.WriteLine("Try to guess the secret number (between 1 and 99).");

            while (Attempts < MaxAttempts && !HasWon)
            {
                Console.Write("Enter your guess: ");

                if (!int.TryParse(Console.ReadLine(), out int guess) || guess < 1 || guess > 99)
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 99.");
                    continue;
                }

                PlayerGuess = guess;
                Attempts++;

                if (PlayerGuess == SecretNumber)
                {
                    HasWon = true;
                    Console.WriteLine("Congratulations! You've guessed the correct number!");
                }
                else
                {
                    AdjustScore();
                    Console.WriteLine(PlayerGuess > SecretNumber
                        ? "Too high! Try a smaller number."
                        : "Too low! Try a larger number.");
                }
            }

            DisplayResult();
        }

        private void AdjustScore()
        {
            Score = Math.Max(0, Score - 10); // Ensure score doesn't go below 0
        }

        private void DisplayResult()
        {
            Console.WriteLine(HasWon
                ? $"You won! Score: {Score}, Attempts: {Attempts}"
                : $"You lost! The secret number was {SecretNumber}.");

            Console.WriteLine("\nDo you want to play again?");
            Console.WriteLine("1. Play again");
            Console.WriteLine("2. Exit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ResetGame();
                    break;
                case "2":
                    Console.WriteLine("Thanks for playing! Goodbye.");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Exiting game.");
                    break;
            }
        }

        private void ResetGame()
        {
            Console.Clear();
            Console.WriteLine("Starting a new game...\n");
            SecretNumber = GenerateSecretNumber();
            Attempts = 0;
            Score = 50;
            HasWon = false;
            Play();
        }
    }
}
