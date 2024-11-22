

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Higher or Lower Card Game!");
        Console.WriteLine("Guess if the next card will be higher or lower than the current card.");
        Console.WriteLine("You have 10 seconds to make your guess for each round.");

        var game = new Game();

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Round: {game.CurrentRound + 1}");
            Console.WriteLine($"Score: {game.Score}\n");

            Console.WriteLine($"Current card: {game.CurrentCard}");
            Console.WriteLine($"Next card: ???");

            DateTime roundStartTime = DateTime.Now;
            string playerGuess = GetPlayerGuess(roundStartTime);

            bool isCorrectGuess = game.CheckGuess(playerGuess == "H");

            if (isCorrectGuess)
            {
                Console.WriteLine("Correct! Well done.");
                game.AdvanceToNextRound();
            }
            else
            {
                Console.WriteLine("Incorrect! Better luck next time.");
                game.AdvanceToNextRound();
                Console.WriteLine($"The next card was: {game.NextCard}");
            }

            if (game.NextCard == null)
            {
                Console.WriteLine("No more cards left. Game over!");
                break;
            }

            Console.WriteLine($"The next card was: {game.NextCard}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        Console.WriteLine($"Game Over! Final Score: {game.Score}");
    }

    static string GetPlayerGuess(DateTime startTime)
    {
        bool hasGuessed = false;
        string guess = string.Empty;

        Console.WriteLine("Will the next card be higher or lower?");
        Console.Write("Enter 'H' for Higher or 'L' for Lower: ");

        while ((DateTime.Now - startTime).TotalSeconds < 10 && !hasGuessed)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(intercept: true).KeyChar.ToString().ToUpper();
                if (key == "H" || key == "L")
                {
                    guess = key;
                    hasGuessed = true;
                }
            }
        }

        if (!hasGuessed)
        {
            Console.WriteLine("\nTime's up! You didn't make a choice.");
            guess = "L"; 
        }

        return guess;
    }
}