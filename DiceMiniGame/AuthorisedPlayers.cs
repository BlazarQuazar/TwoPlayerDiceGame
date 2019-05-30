using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DiceMiniGame
{
    class AuthorisedPlayers
    {
        protected Dictionary<string, string> dictPlayers = new Dictionary<string, string>();

        public AuthorisedPlayers()
        {
            try
            {
                using (StreamReader srPlayers = new StreamReader("Players.txt"))
                {
                    string file = srPlayers.ReadToEnd();
                    string[] players = file.Split('\n');
                    foreach (string player in players)
                    {

                        string[] splitLine = player.Split('\t');
                        this.dictPlayers.Add(splitLine[0], splitLine[1].Trim());
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("This file could not be read");
                Console.WriteLine(e.Message);
            }
        }

        private bool ContainsUsername(string iUsername)
        {
            if (this.dictPlayers.Keys.Contains(iUsername))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool PasswordMatch(string iUsername, string iPassword)
        {
            if (this.dictPlayers[iUsername].Trim() == iPassword)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Player TryAddPlayerToGame(string[] playerDetails)
        {
            string iUsername = playerDetails[0];
            string iPassword = playerDetails[1];

            if (this.ContainsUsername(iUsername))
            {
                if (this.PasswordMatch(iUsername, iPassword))
                {
                    return new Player(iUsername);
                }
                else
                {
                    Console.WriteLine("\nIncorrect Password!");
                    return null;
                }
            }
            else
            {
                Console.WriteLine("\nUnrecognised player!");
                return null;
            }
        }
    }
}
