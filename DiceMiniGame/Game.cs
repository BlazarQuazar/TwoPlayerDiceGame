using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMiniGame
{
    class Game
    {
        private int totalRounds = 5;
        private int currentRound = 0;
        Player player1, player2;
        List<Die> dice = new List<Die>();
        AuthorisedPlayers authPlayers = new AuthorisedPlayers();
        private bool player1sTurn = true;
        private int thisTurnsScore = 0;
        private bool firstThrow = true;

        private string[] AskForPlayerDetails()
        {
            string[] playerDetails = new string[2];

            Console.Write("Enter Username: ");
            playerDetails[0] = Console.ReadLine();
            Console.Write("Enter password: ");

            // Input mask for password
            string password = string.Empty;
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password.Substring(0, (password.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);

            playerDetails[1] = password;
            Console.WriteLine();
            return playerDetails;
        }

        public void TryToAddPlayer1()
        {
            string[] temp = AskForPlayerDetails();
            this.player1 = authPlayers.TryAddPlayerToGame(temp);
            if (this.player1 == null)
            {
                Console.WriteLine("Please re-enter details\n");
                TryToAddPlayer1();
            }
            else
            {
                Console.WriteLine("\nWelcome {0}\n", player1.ReturnUsername());
            }
        }

        public void TryToAddPlayer2()
        {
            string[] temp = AskForPlayerDetails();

            // if p2 trys to log in as p1
            if (temp[0] == player1.ReturnUsername())
            {
                Console.WriteLine("\nPlayer already logged in, try again");
                this.TryToAddPlayer2();
            }
            else
            {
                this.player2 = authPlayers.TryAddPlayerToGame(temp);
                if (this.player2 == null)
                {
                    Console.WriteLine("Please re-enter details\n");
                    TryToAddPlayer2();
                }
                else
                {
                    Console.WriteLine("\nWelcome {0}\n", player2.ReturnUsername());
                }
            }

            
        }

        public void UpdateHeader()
        {
            string outputString = string.Empty;
            outputString += "Round: " + (currentRound + 1) + "\tScores (";
            outputString += player1.ReturnUsername() + ": " + player1.ReturnScore();
            outputString += "\t" + player2.ReturnUsername() + ": " + player2.ReturnScore();
            outputString += ")\n";
            Console.WriteLine(outputString);


            if (firstThrow != true)
            {
                if (player1sTurn)
                {
                    Console.WriteLine(player1.ReturnUsername() + "'s throw:\n");
                }
                else
                {
                    Console.WriteLine(player2.ReturnUsername() + "'s throw:\n");
                }
            }
            firstThrow = false;
        }

        public void UpdateFooter()
        {
            string outputString = string.Empty;
            if (this.player1sTurn == true)
            {
                outputString += "\n" + player1.ReturnUsername() + "'s turn next.\npress enter to take your turn.";
            }
            else
            {
                outputString += "\n" + player2.ReturnUsername() + "'s turn next.\npress enter to take your turn.";
            }
            Console.WriteLine(outputString);
        }

        public void AddDie(int sides)
        {
            dice.Add(new Die(sides));
        }

        public List<int> TakeTurn()
        {
            List<int> results = new List<int>();

            foreach (Die die in dice)
            {
                results.Add(die.throwMyself());
            }
            return results;
        }

        private void DrawDice(List<int> iResults)
        {
            List<string[,]> diceArrays = new List<string[,]>();
            foreach (int roll in iResults)
            {
                diceArrays.Add(ConstructDiceArray(roll));
            }

            // have the arracy of dice in the dice arrays field
            // create array of 12 strings, add to each string from the 2D array
            string[] outputDicePicture = new string[12];
            string line = string.Empty;
            bool first = true;

            // Complie both dices, 
            foreach (string[,] die in diceArrays)
            {
                // add spacer if not the first dice
                if (first != true)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        outputDicePicture[i] += "      ";
                    }
                }

                for (int i = 0; i < 12; i++)
                {
                    for (int j = 0; j < 24; j++)
                    {
                        outputDicePicture[i] += die[i, j];
                    }
                }
                first = false;
            }

            foreach (string lineODP in outputDicePicture)
            {
                Console.WriteLine(lineODP);
            }
        }

        private string[,] ConstructDiceArray(int roll)
        {
            char icon = '@';
            string iconString = icon.ToString();
            // icons are twice as high as wide, so x axis = 2* y axis
            string[,] dieArray = new string[12, 24];
            // fill each with " "
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 24; j++)
                {
                    dieArray[i, j] = " ";
                }
            }
            //set top & bottom borders
            for (int i = 0; i < 12; i += 11)
            {
                for (int j = 0; j < 24; j++)
                {
                    dieArray[i, j] = iconString;
                }
            }
            //set left & right borders
            for (int i = 1; i < 11; i++)
            {
                dieArray[i, 0] = iconString;
                dieArray[i, 23] = iconString;
            }
            // add dots to dice
            switch (roll)
            {
                case 1:
                    // centre dot
                    for (int i = 5; i < 7; i++)
                    {
                        for (int j = 10; j < 14; j++)
                        {
                            dieArray[i, j] = iconString;
                        }
                    }
                    break;

                case 2:
                    // top left dot
                    for (int i = 2; i < 4; i++)
                    {
                        for (int j = 4; j < 8; j++)
                        {
                            dieArray[i, j] = iconString;
                        }
                    }
                    // bottom right dot
                    for (int i = 8; i < 10; i++)
                    {
                        for (int j = 16; j < 20; j++)
                        {
                            dieArray[i, j] = iconString;
                        }
                    }
                    break;

                case 3:
                    // top right
                    for (int i = 2; i < 4; i++)
                    {
                        for (int j = 16; j < 20; j++)
                        {
                            dieArray[i, j] = iconString;
                        }
                    }
                    // centre
                    for (int i = 5; i < 7; i++)
                    {
                        for (int j = 10; j < 14; j++)
                        {
                            dieArray[i, j] = iconString;
                        }
                    }
                    // bottom left
                    for (int i = 8; i < 10; i++)
                    {
                        for (int j = 4; j < 8; j++)
                        {
                            dieArray[i, j] = iconString;
                        }
                    }
                    break;

                case 4:
                    // top left
                    for (int i = 2; i < 4; i++)
                    {
                        for (int j = 4; j < 8; j++)
                        {
                            dieArray[i, j] = iconString;
                        }
                    }
                    // top right
                    for (int i = 2; i < 4; i++)
                    {
                        for (int j = 16; j < 20; j++)
                        {
                            dieArray[i, j] = iconString;
                        }
                    }
                    // bottom left
                    for (int i = 8; i < 10; i++)
                    {
                        for (int j = 4; j < 8; j++)
                        {
                            dieArray[i, j] = iconString;
                        }
                    }
                    // bottom right
                    for (int i = 8; i < 10; i++)
                    {
                        for (int j = 16; j < 20; j++)
                        {
                            dieArray[i, j] = iconString;
                        }
                    }
                    break;

                case 5:
                    // top left
                    for (int i = 2; i < 4; i++)
                    {
                        for (int j = 4; j < 8; j++)
                        {
                            dieArray[i, j] = iconString;
                        }
                    }
                    // top right
                    for (int i = 2; i < 4; i++)
                    {
                        for (int j = 16; j < 20; j++)
                        {
                            dieArray[i, j] = iconString;
                        }
                    }
                    // bottom left
                    for (int i = 8; i < 10; i++)
                    {
                        for (int j = 4; j < 8; j++)
                        {
                            dieArray[i, j] = iconString;
                        }
                    }
                    // bottom right
                    for (int i = 8; i < 10; i++)
                    {
                        for (int j = 16; j < 20; j++)
                        {
                            dieArray[i, j] = iconString;
                        }
                    }
                    // centre
                    for (int i = 5; i < 7; i++)
                    {
                        for (int j = 10; j < 14; j++)
                        {
                            dieArray[i, j] = iconString;
                        }
                    }
                    break;

                case 6:
                    // top left
                    for (int i = 2; i < 4; i++)
                    {
                        for (int j = 4; j < 8; j++)
                        {
                            dieArray[i, j] = iconString;
                        }
                    }
                    // top right
                    for (int i = 2; i < 4; i++)
                    {
                        for (int j = 16; j < 20; j++)
                        {
                            dieArray[i, j] = iconString;
                        }
                    }
                    // bottom left
                    for (int i = 8; i < 10; i++)
                    {
                        for (int j = 4; j < 8; j++)
                        {
                            dieArray[i, j] = iconString;
                        }
                    }
                    // bottom right
                    for (int i = 8; i < 10; i++)
                    {
                        for (int j = 16; j < 20; j++)
                        {
                            dieArray[i, j] = iconString;
                        }
                    }
                    // center left
                    for (int i = 5; i < 7; i++)
                    {
                        for (int j = 4; j < 8; j++)
                        {
                            dieArray[i, j] = iconString;
                        }
                    }
                    // center right
                    for (int i = 5; i < 7; i++)
                    {
                        for (int j = 16; j < 20; j++)
                        {
                            dieArray[i, j] = iconString;
                        }
                    }
                    break;
            }

            return dieArray;
        }

        public void PlayRounds(int iRounds)
        {
            int totalRounds = currentRound + iRounds;
            for (; currentRound < totalRounds; currentRound++)
            {
                for (int j = 0; j < 2; j++)
                {
                    List<int> results = this.TakeTurn();
                    Console.ReadLine();

                    Console.Clear();
                    thisTurnsScore = results.Sum();
                    this.UpdateHeader();

                    this.DrawDice(results);
                    Console.WriteLine("\nTHROW SCORE: " + thisTurnsScore);

                    if (thisTurnsScore % 2 == 0)
                    {
                        thisTurnsScore += 10;
                        Console.WriteLine("EVEN! +10 POINTS! : " + thisTurnsScore);
                    }
                    else
                    {
                        thisTurnsScore -= 5;
                        Console.WriteLine("ODD! -5 POINTS! : " + thisTurnsScore);
                    }

                    // if get a double
                    if (results[0] == results[1])
                    {
                        results.Add(dice[0].throwMyself());
                        Console.WriteLine("DOUBLE! EXTRA DICE THROWN! : " + results[2]);
                        thisTurnsScore += results[2];

                        Console.SetCursorPosition(0, 4);
                        this.DrawDice(results);
                        Console.SetCursorPosition(0, 20);
                        
                    }

                    Console.WriteLine("TOTAL THIS ROUND: " + thisTurnsScore);

                    if (player1sTurn)
                    {
                        player1.AddScore(thisTurnsScore);
                        player1sTurn = false;
                    }
                    else
                    {
                        player2.AddScore(thisTurnsScore);
                        player1sTurn = true;
                    }


                    this.UpdateFooter();
                }
            }
        }

        public Player DisplayWinner()
        {
            Player winner = player1;
            if(player2.ReturnScore() > player1.ReturnScore())
            {
                winner = player2;
            }

            // Defining borders
            char iconChar = '@';
            string iconString = iconChar.ToString();
            string border = "".PadRight(35, iconChar);
            string buffer = iconString + "".PadRight(33) + iconString;

            Console.Clear();
            this.UpdateHeader();

            Console.WriteLine(border);
            Console.WriteLine(buffer);
            // enter right side border, return curser to beg of line, print results
            Console.Write("\r".PadRight(35) + iconString + "\r");
            Console.WriteLine(iconString.PadRight(6) + "The winner is:   " + winner.ReturnUsername());

            Console.Write("\r".PadRight(35) + iconString + "\r");
            Console.WriteLine(iconString.PadRight(6) + "With a score of: " + winner.ReturnScore());
            Console.WriteLine(buffer);
            Console.WriteLine(border);

            return winner;
        }

        public Player PlayGame()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();

            this.UpdateHeader();
            this.UpdateFooter();
            this.PlayRounds(totalRounds);

            // play extra round if score are equal
            while(player1.ReturnScore() == player2.ReturnScore())
            {
                this.PlayRounds(currentRound + 1);
            }

            // display results waiting text
            Console.SetCursorPosition(0, Console.CursorTop - 2);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.Write("\rPress enter for results.....");
            Console.Write(new string(' ', Console.WindowWidth) + "\r");
            Console.ReadLine();

            return this.DisplayWinner();          

        }

        public void SayGoodbye()
        {
            Console.WriteLine("\nThank you for playing!\nI hope you enjoyed.\n\n");

            Console.Write("press enter to quit");
            Console.Read();
        }
    }
}
