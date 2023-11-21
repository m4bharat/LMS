namespace LotteryAPI.LotteryBusiness
{
    public class LotterySystem
    {
        private List<int[]> tickets = new List<int[]>();
        private int[] winningNumbers;

        public int[] BuyLotteryTicket()
        {
            return GenerateLotteryNumbers();
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
}