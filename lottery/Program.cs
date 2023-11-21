using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // Set up the lottery system
        LotterySystem lotterySystem = new LotterySystem();

        // Display welcome message
        Console.WriteLine("Welcome to the Lottery Game!");

        // Main menu loop
        while (true)
        {
            Console.WriteLine("\nMain Menu:");
            Console.WriteLine("1. Buy Lottery Ticket");
            Console.WriteLine("2. Draw Lottery Numbers");
            Console.WriteLine("3. Exit");

            Console.Write("Choose an option (1-3): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    lotterySystem.BuyLotteryTicket();
                    break;
                case "2":
                    lotterySystem.DrawLotteryNumbers();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please choose a valid option.");
                    break;
            }
        }
    }
}

class LotterySystem
{
    private List<int[]> tickets = new List<int[]>();
    private int[] winningNumbers;

    public void BuyLotteryTicket()
    {
        Console.WriteLine("\nBuy Lottery Ticket:");

        // Generate a random set of lottery numbers for the ticket
        int[] lotteryNumbers = GenerateLotteryNumbers();

        // Display the user's selected lottery numbers for the ticket
        Console.WriteLine("Your Numbers: " + string.Join(", ", lotteryNumbers));

        // Add the ticket to the list
        tickets.Add(lotteryNumbers);
    }

    public void DrawLotteryNumbers()
    {
        Console.WriteLine("\nDraw Lottery Numbers:");

        // Generate the winning numbers
        winningNumbers = GenerateLotteryNumbers();

        // Display the winning numbers
        Console.WriteLine("Winning Numbers: " + string.Join(", ", winningNumbers));

        // Check how many numbers match for each ticket
        foreach (var ticket in tickets)
        {
            int matchingNumbers = CountMatchingNumbers(ticket, winningNumbers);
            Console.WriteLine($"Ticket Numbers: {string.Join(", ", ticket)} | Matching Numbers: {matchingNumbers}");
        }

        // Clear the tickets for the next draw
        tickets.Clear();
    }

    private int[] GenerateLotteryNumbers()
    {
        Random random = new Random();
        int[] numbers = new int[6]; // You can adjust the number of lottery numbers as needed
        HashSet<int> uniqueNumbers = new HashSet<int>();

        while (uniqueNumbers.Count < numbers.Length)
        {
            int randomNumber = random.Next(1, 50); // You can adjust the range of lottery numbers as needed
            uniqueNumbers.Add(randomNumber);
        }

        uniqueNumbers.CopyTo(numbers);

        return numbers;
    }

    private int CountMatchingNumbers(int[] userNumbers, int[] winningNumbers)
    {
        return userNumbers.Count(winningNumbers.Contains);
    }
}
