using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders
{
    class Defenses
    {
        private int hp;
        public bool Isalive
        {
            get; private set;
        }
        public int Hp { get { return hp; } set { hp = value; } }
        public void HpDown()
        {
            Hp -= 1;
        }
        public Coordinates coordinates;
        public void Write()
        {
            if (Hp != 0)
            {
                Console.SetCursorPosition(coordinates.X, coordinates.Y);
                Console.Write('▀');
            }
        }
        public static Defenses[] Destroy(Defenses[] defenses, int i)
        {
            for (int j = i; j < defenses.GetLength(0) - 1; j++)
            {
                defenses[j] = defenses[j + 1];
            }
            Array.Resize(ref defenses, defenses.GetLength(0) - 1);
            return defenses;
        }

        public Defenses(int x, int y)
        {
            Hp = 10;
            coordinates = new Coordinates(x,y);
        }
    }
}
