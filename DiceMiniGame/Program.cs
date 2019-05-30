using System;


namespace DiceMiniGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            Scoreboard scoreboard = new Scoreboard();

            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Clear();
            
            game.AddDie(6);
            game.AddDie(6);

            scoreboard.DisplayScoreboard();

            Console.WriteLine("\nPlayer 1:");
            game.TryToAddPlayer1();

            Console.WriteLine("Player 2:");
            game.TryToAddPlayer2();

            Console.WriteLine("Press enter to start game");
            Console.ReadLine();
            Console.Clear();
 
            Player winner = game.PlayGame();

            bool isNewHighScore = scoreboard.CheckIfNewHighScore(winner);            
            if (isNewHighScore)
            {
                scoreboard.AddNewHighScore(winner);
                Console.Clear();
                scoreboard.UpdateScoreboard();
                scoreboard.DisplayScoreboard();
            }
            game.SayGoodbye();
        }
    }
}
