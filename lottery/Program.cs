using System;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("Welcome to the Lottery Game!");

        // Generate random winning numbers
        int[] winningNumbers = GenerateLotteryNumbers();

        // Get user's lottery numbers
        int[] userNumbers = GetUserNumbers();

        // Display the results
        Console.WriteLine("\nWinning Numbers: " + string.Join(", ", winningNumbers));
        Console.WriteLine("Your Numbers   : " + string.Join(", ", userNumbers));

        // Check how many numbers match
        int matchingNumbers = CountMatchingNumbers(winningNumbers, userNumbers);

        // Display the result
        Console.WriteLine("\nMatching Numbers: " + matchingNumbers);

        // Determine the result (1st, 2nd, 3rd, or no win)
        DetermineWinningRank(matchingNumbers);

        Console.ReadLine(); // Keep the console window open
    }

    static int[] GenerateLotteryNumbers()
    {
        Random random = new Random();
        int[] numbers = new int[6]; // You can adjust the number of lottery numbers as needed

        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = random.Next(1, 50); // You can adjust the range of lottery numbers as needed
        }

        return numbers;
    }

    static int[] GetUserNumbers()
    {
        int[] numbers = new int[6];

        Console.WriteLine("\nEnter your lottery numbers (1-49):");

        for (int i = 0; i < numbers.Length; i++)
        {
            Console.Write($"Enter number #{i + 1}: ");
            while (!int.TryParse(Console.ReadLine(), out numbers[i]) || numbers[i] < 1 || numbers[i] > 49)
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 49.");
                Console.Write($"Enter number #{i + 1}: ");
            }
        }

        return numbers;
    }

    static int CountMatchingNumbers(int[] winningNumbers, int[] userNumbers)
    {
        return userNumbers.Count(winningNumbers.Contains);
    }

    static void DetermineWinningRank(int matchingNumbers)
    {
        if (matchingNumbers == 5)
        {
            Console.WriteLine("Congratulations! You've won the lottery! (1st Place)");
        }
        else if (matchingNumbers >= 4)
        {
            Console.WriteLine("Congratulations! You've won a prize! (2nd Place)");
        }
        else if (matchingNumbers >= 2)
        {
            Console.WriteLine("Congratulations! You've won a prize! (3rd Place)");
        }
        else
        {
            Console.WriteLine("Better luck next time!");
        }
    }
}
