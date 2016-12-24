using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Space_Invaders
{
    class Player
    {
        public Direction direction;
        public Coordinates coordinates;
        private bool isalive;
        public bool Isalive
        {
            get { return isalive; } set { isalive = value; } 
        }
        public int Lives { get; private set; }
        public void Death()
        {
            Lives--;
        }
        public void Shoot()
        {
            Array.Resize(ref BattleFront.playerbullets, BattleFront.playerbullets.GetLength(0) + 1);
            BattleFront.playerbullets[BattleFront.playerbullets.GetLength(0)-1] = new Bullet(coordinates.X, coordinates.Y, Direction.Up);
        }
        public void Move()
        {
            if ((direction == Direction.Left)&& (coordinates.X - 1 != 0))
            {
                coordinates.X = coordinates.X - 1;
            }
            if (direction == Direction.Right && (coordinates.X + 1 != BattleFront.Battlefield.GetLength(1)-1))
            {
                coordinates.X = coordinates.X + 1;
            }
        }
        public void Write()
        { 
            BattleFront.Battlefield[coordinates.Y, coordinates.X] = 'Ж';
        }

    public Player(int x, int y)
        {
            isalive = true;
            Lives = 3;
            coordinates = new Coordinates(x,y);
        }
    }
}
