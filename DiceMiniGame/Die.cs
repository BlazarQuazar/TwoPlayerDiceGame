using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMiniGame
{
    class Die
    {
        private int sides = 6;
        Random random = new Random(Guid.NewGuid().GetHashCode());

        public Die(int iSides)
        {
            this.sides = iSides;
        }

        public int throwMyself()
        {
            return random.Next(1, this.sides + 1);
        }
    }
}
