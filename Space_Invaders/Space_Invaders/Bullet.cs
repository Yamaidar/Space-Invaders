using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders
{
    class Bullet
    {
        public Coordinates coordinates;
        public Direction direction;
        public void Move()
        {
            if (direction == Direction.Up)
            {
                if (coordinates.Y - 1 != 0)
                {
                    coordinates.Y = coordinates.Y - 1;
                }
            }
            if ((direction == Direction.Down)&&(coordinates.Y+1!=BattleFront.Battlefield.GetLength(0))) { coordinates.Y = coordinates.Y + 1; }
        }
        public static Bullet[] Destroy(Bullet[] bullets, int i)
        {
            for (int j = i; j < bullets.GetLength(0)-1; j++)
            {
                bullets[j] = bullets[j + 1];
            }
            Array.Resize(ref bullets, bullets.GetLength(0) - 1);
            return bullets;
        }

        public void Write()
        {
            BattleFront.Battlefield[coordinates.Y, coordinates.X] = '|';
        }

        public Bullet(int x, int y, Direction direction)
        {
            coordinates = new Coordinates(x,y);
            this.direction = direction;
        }
    }
}
