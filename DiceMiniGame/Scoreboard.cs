using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DiceMiniGame
{
    class Scoreboard
    {
        protected Dictionary<string, string> dictScores = new Dictionary<string, string>();

        public Scoreboard()
        {
            this.UpdateScoreboard();
        }

        public void UpdateScoreboard()
        {
            dictScores.Clear();
            try
            {
                using (StreamReader srScores = new StreamReader("HighScores.txt"))
                {
                    string file = srScores.ReadToEnd();
                    string[] players = file.Split('\n');
                    foreach (string player in players)
                    {
                        string[] splitLine = player.Split('\t');
                        this.dictScores.Add(splitLine[0], splitLine[1].Trim());
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("This file could not be read");
                Console.WriteLine(e.Message);
            }

        }

        public void DisplayScoreboard()
        {
            // Defining basic charateristics
            char iconChar = '@';
            string iconString = iconChar.ToString();
            string border = "".PadRight(35, iconChar);
            string buffer = iconString + "".PadRight(33) + iconString;

            // Writing the header
            Console.WriteLine();
            Console.WriteLine("HIGH SCORES".PadLeft(23));
            Console.WriteLine(border);
            Console.WriteLine(buffer);

            foreach(string name in dictScores.Keys)
            {
                Console.WriteLine(iconString + name.PadLeft(20) + " - " + dictScores[name].PadRight(10) + iconString);
            }

            Console.WriteLine(buffer);
            Console.WriteLine(border);
        }

        public bool CheckIfNewHighScore(Player iWinner)
        {
            bool newHighScore = false;
            foreach(string score in dictScores.Values)
            {
                if(iWinner.ReturnScore() > Convert.ToInt32(score))
                {
                    newHighScore = true;
                    break;
                }
            }
            return newHighScore;
        }

        public void AddNewHighScore(Player iWinner)
        {
            int newScore = iWinner.ReturnScore();
            List<string> newScoreboard = new List<string>();

            Console.WriteLine("\nCongratulations! your got a new high score!!!");
            string iName = string.Empty;

            while (true)
            {
                Console.Write("Please enter name: ");
                iName = Console.ReadLine();

                if (dictScores.Keys.Contains(iName))
                {
                    Console.WriteLine("\n" + iName + " already exists, please enter another name");
                }
                else
                {
                    break;
                }
            }

            bool scoreInserted = false;
            foreach (string name in dictScores.Keys)
            {
                if (scoreInserted == false && newScore > Convert.ToInt32(dictScores[name]))
                {
                    newScoreboard.Add(iName + "\t" + newScore.ToString());
                    newScoreboard.Add(name + "\t" + dictScores[name]);
                    scoreInserted = true;
                }
                else
                {
                    newScoreboard.Add(name + "\t" + dictScores[name]);
                }
            }

            try
            {
                using (StreamWriter swScores = new StreamWriter("HighScores.txt"))
                {
                    for(int i =0; i < 4; i++)
                    {
                        swScores.WriteLine(newScoreboard[i]);
                    }
                    swScores.Write(newScoreboard[4]);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("This find could not be read");
                Console.WriteLine(e.Message);
            }
        }

    }
}
