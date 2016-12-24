using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

namespace Space_Invaders
{
    class Time
    {
        Random r = new Random();
        public int tick = 0;
        private static int speed;
        public static int modifier;
        public static int curspeed;
        public static int Speed {get{ return speed; } set { speed = value; } }
        private Timer aTimer;
        public void Main(int i)
        {
            
            aTimer = new System.Timers.Timer();
            aTimer.Interval = i;
            aTimer.Elapsed += Go;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            do
            {
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey(true);
                    if (key.Key.Equals(ConsoleKey.LeftArrow))
                    {
                        BattleFront.Battlefield[BattleFront.player.coordinates.Y, BattleFront.player.coordinates.X] = ' ';
                        BattleFront.player.direction = Direction.Left;
                        BattleFront.player.Move();
                        BattleFront.player.Write();
                    }

                    if (key.Key.Equals(ConsoleKey.RightArrow))
                    {
                        BattleFront.Battlefield[BattleFront.player.coordinates.Y, BattleFront.player.coordinates.X] = ' ';
                        BattleFront.player.direction = Direction.Right;
                        BattleFront.player.Move();
                        BattleFront.player.Write();
                    }
                    if (key.Key.Equals(ConsoleKey.Spacebar))
                    {
                        if (BattleFront.playerbullets.GetLength(0)<BattleFront.playerbulletsamount)
                        BattleFront.player.Shoot();
                    }
                }
            }
            while (BattleFront.player.Isalive);

            Console.ReadLine();
        }

        private void Go(object source, ElapsedEventArgs e)
        {
            //if (BattleFront.Score>500)
            //{
            //    BattleFront.SpawnSpecial();
            //}
            //BattleFront.MoveSpecial();
            if (BattleFront.invaders.GetLength(0) < 36 && curspeed == speed) { curspeed = speed-modifier; BattleFront.invaderbulletsamount += (4-modifier); }
            if (BattleFront.invaders.GetLength(0) < 12 && curspeed == speed-modifier) { curspeed -= modifier; BattleFront.invaderbulletsamount +=(4-modifier); }
            if (r.Next(curspeed) == 0) BattleFront.Shoot();
            BattleFront.MoveBullets();
            tick++;
            if (tick > curspeed)
            {
                tick = 0;
                BattleFront.MoveInvaders();
            }
            Console.SetCursorPosition(0, 0);
            BattleFront.Write();
            
            if (BattleFront.invaders.GetLength(0) == 0)
            {
                aTimer.Enabled = false;
                Console.SetCursorPosition(60, 15);
                Console.Write("You win!");
                Console.SetCursorPosition(0, 30);
            }
            if (BattleFront.player.Isalive == false)
            {
                aTimer.Enabled = false;
                Console.SetCursorPosition(60, 15);
                Console.Write("You lose");
                Console.SetCursorPosition(0, 30);
            }
        }
    }
}

