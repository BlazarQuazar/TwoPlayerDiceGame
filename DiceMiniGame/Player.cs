using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMiniGame
{
    class Player
    {
        private string username = string.Empty;
        private int score = 0;

        public Player(string iUsername)
        {
            this.username = iUsername;
        }

        public int ReturnScore()
        {
            return this.score;
        }

        public string ReturnUsername()
        {
            return this.username;
        }

        public void AddScore(int iScore)
        {
            this.score += iScore;

            if (this.score < 0) { this.score = 0; }
        }
    }
}
