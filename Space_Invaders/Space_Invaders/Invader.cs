using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders
{
    //class InvaderSpecial
    //{
    //    Random r = new Random();
    //    public Direction direction;
    //    public Coordinates coordinates1;
    //    public Coordinates coordinates2;
    //    public Coordinates coordinates3;
    //    public int Points{ get; private set; }
    //    //private bool isalive = false;
    //    //public bool IsAlive { get { return isalive; } set { isalive = value; } }
    //    public InvaderSpecial()
    //    {
    //        if (r.Next(2) == 0)
    //        {
    //            coordinates1 = new Coordinates(1, 2);
    //            coordinates2 = new Coordinates(2, 2);
    //            coordinates3 = new Coordinates(3, 2);
    //            direction = Direction.Right;
    //        }
    //        else
    //        {
    //            coordinates1 = new Coordinates(51, 2);
    //            coordinates2 = new Coordinates(50, 2);
    //            coordinates3 = new Coordinates(49, 2);
    //            direction = Direction.Left;
    //        }
    //        Points = 300;
    //        //IsAlive = true;
    //    }
    //    public void Move()
    //    {
    //        if (direction == Direction.Left)
    //        {
    //            coordinates1.X = coordinates1.X - 1;
    //            coordinates2.X = coordinates2.X - 1;
    //            coordinates3.X = coordinates3.X - 1;
    //        }
    //        if (direction == Direction.Right)
    //        {
    //            coordinates1.X = coordinates1.X + 1;
    //            coordinates2.X = coordinates2.X + 1;
    //            coordinates3.X = coordinates3.X + 1;
    //        }
    //    }

    //    public void Write()
    //    {
    //        BattleFront.Battlefield[coordinates1.Y, coordinates1.X] = '?';
    //        BattleFront.Battlefield[coordinates2.Y, coordinates2.X] = '?';
    //        BattleFront.Battlefield[coordinates3.Y, coordinates3.X] = '?';
    //    }
    //}

    abstract class Invader
    {
        public Coordinates coordinates;
        public Direction direction = Direction.Right;
        public int Points { get; protected set; }
        public void Move()
        {
            if (direction == Direction.Left) { coordinates.X = coordinates.X - 1; }
            if (direction == Direction.Right) { coordinates.X = coordinates.X + 1; }
            if (direction == Direction.Up) { coordinates.Y = coordinates.Y - 1; }
            if (direction == Direction.Down) { coordinates.Y = coordinates.Y + 1; }
        }

        public void Shoot()
        {
            Array.Resize(ref BattleFront.invaderbullets, BattleFront.invaderbullets.GetLength(0) + 1);
            BattleFront.invaderbullets[BattleFront.invaderbullets.GetLength(0) - 1] = new Bullet(coordinates.X, coordinates.Y + 1, Direction.Down);
            BattleFront.Battlefield[coordinates.Y+1, coordinates.X] = '|';
        }
        public abstract void Write();

        public static Invader[] Destroy(Invader[] invaders, int i)
        {
            BattleFront.Battlefield[invaders[i].coordinates.Y, invaders[i].coordinates.X] = ' ';
            for (int j = i; j < invaders.GetLength(0) - 1; j++)
            {
                invaders[j] = invaders[j + 1];
            }
            Array.Resize(ref invaders, invaders.GetLength(0) - 1);
            return invaders;
        }

        public Invader(int x, int y)
        {
            coordinates = new Coordinates(x, y);
        }
    }

    class Invader1 : Invader
    { 
        public override void Write()
        {
            BattleFront.Battlefield[coordinates.Y, coordinates.X] = 'ф';
        }

        public Invader1(int x, int y)
            : base(x, y) { Points = 10; }
    }
    class Invader2 : Invader
    {
        public override void Write()
        {
            BattleFront.Battlefield[coordinates.Y, coordinates.X] = 'T';
        }

        public Invader2(int x, int y)
            : base(x, y) { Points = 20; }
    }
    class Invader3 : Invader
    {
        public override void Write()
        {
            BattleFront.Battlefield[coordinates.Y, coordinates.X] = 'Y';
        }

        public Invader3(int x, int y)
            : base(x, y) { Points = 30; }
    }
}
