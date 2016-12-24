using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders
{
    class BattleFront
    {
        static Random r = new Random();
        private static string[] battlefront = System.IO.File.ReadAllLines(@"Battlefield.txt");

        private static char[,] battlefield = new char[battlefront.GetLength(0), battlefront[0].Length];

        public static char[,] Battlefield
        {
            get { return battlefield; } set { battlefield = value; }
        }

        private static int score = 0;
        public static int Score
        { get {return score; } set { score = value; } }

        public static Player player = new Player(25, battlefield.GetLength(0)-2);

        public static Defenses[] defenses = new Defenses[15];

        //public static InvaderSpecial[] invaderspecial = new InvaderSpecial[0];

        public static Bullet[] playerbullets = new Bullet[0];
        public static int playerbulletsamount = 3;

        public static Bullet[] invaderbullets = new Bullet[0];
        public static int invaderbulletsamount;

        public static Invader[] invaders = new Invader[60];

        public static void Create()
        {
            int c1 = 0;
            int l = 0;
            for (int i = 0; i < battlefront.GetLength(0); i++)
            {
                for (int j = 0; j < battlefront[0].Length; j++)
                {
                    battlefield[i, j] = battlefront[i][j];
                    if (battlefield[i, j] == 'Ж') player = new Player(j, i);
                    if (battlefield[i, j] == '▀') { defenses[l]= new Defenses(j, i); l++; }
                    if (battlefield[i, j] == 'Y') { invaders[c1]= new Invader3(j, i); c1++; }
                    if (battlefield[i, j] == 'Т') { invaders[c1]= new Invader2(j, i); c1++; }
                    if (battlefield[i, j] == 'ф') { invaders[c1]= new Invader1(j, i); c1++; }
                }
            }
        }

        //public static void SpawnSpecial()
        //{
        //    Array.Resize(ref invaderspecial, 1);
        //    invaderspecial[0] = new InvaderSpecial();
        //    invaderspecial[0].Write();
        //}

        public static void Shoot()
        {
            for (int i = 0; i < invaders.GetLength(0); i++)
            {
                if (invaderbullets.GetLength(0) < invaderbulletsamount + 1)
                {

                    if (r.Next(Time.curspeed + Time.modifier) == 0)
                    {
                        invaders[i].Shoot();
                    }
                }
            }
        }

        public static void InCheckForCollisions()
        {
            for (int i=0; i<invaderbullets.GetLength(0); i++)
            {
                for (int j=0; j<defenses.GetLength(0); j++)
                {
                    if (invaderbullets[i].coordinates.X == defenses[j].coordinates.X && invaderbullets[i].coordinates.Y == defenses[j].coordinates.Y)
                    {
                        defenses[j].Hp -= 1;
                        if (defenses[j].Hp == 0) defenses = Defenses.Destroy(defenses, j);
                        invaderbullets = Bullet.Destroy(invaderbullets, i);
                    }
                }
                if (invaderbullets[i].coordinates.Y + 1 == battlefield.GetLength(0))
                {
                    invaderbullets = Bullet.Destroy(invaderbullets, i);
                }
                if (invaderbullets[i].coordinates.X == player.coordinates.X && invaderbullets[i].coordinates.Y == player.coordinates.Y)
                {
                    player.Death();
                    invaderbullets = Bullet.Destroy(invaderbullets, i);
                    Console.SetCursorPosition(58, 1);
                    Console.Write(player.Lives);
                    if (player.Lives == 0) player.Isalive = false;
                }
            }
        }

        public static void PlCheckForCollisions()
        {
            for (int i = 0; i < playerbullets.GetLength(0); i++)
            {
                for (int j = 0; j < defenses.GetLength(0); j++)
                {
                    if (playerbullets[i].coordinates.X == defenses[j].coordinates.X && playerbullets[i].coordinates.Y == defenses[j].coordinates.Y)
                    {
                        defenses[j].Hp -= 1;
                        if (defenses[j].Hp == 0) defenses = Defenses.Destroy(defenses, j);
                        playerbullets = Bullet.Destroy(playerbullets, i);
                    }
                }
                if (playerbullets[i].coordinates.Y-1 == 0)
                {
                    playerbullets = Bullet.Destroy(playerbullets, i);
                }
                for (int j=0; j<invaders.GetLength(0); j++)
                {
                    if ((invaders[j].coordinates.X == playerbullets[i].coordinates.X) && (invaders[j].coordinates.Y == playerbullets[i].coordinates.Y))
                    {
                        Score += invaders[j].Points;
                        Console.SetCursorPosition(59, 3);
                        Console.Write(Score);
                        invaders = Invader.Destroy(invaders, j);
                        Console.SetCursorPosition(65, 5);
                        Console.Write($"{invaders.GetLength(0)}" );
                        playerbullets = Bullet.Destroy(playerbullets, i);
                        
                    }
                }
            }
        }

        //public static void EraseSpecial()
        //{
        //    for (int i = 0; i < invaderspecial.GetLength(0); i++)
        //    {
        //        battlefield[invaderspecial[i].coordinates1.Y, invaderspecial[i].coordinates1.X] = ' ';
        //        battlefield[invaderspecial[i].coordinates2.Y, invaderspecial[i].coordinates2.X] = ' ';
        //        battlefield[invaderspecial[i].coordinates3.Y, invaderspecial[i].coordinates3.X] = ' ';
        //    }
        //}

        public static void EraseInvaders()
        {
            for (int i = 0; i < invaders.GetLength(0); i++)
            {
                battlefield[invaders[i].coordinates.Y, invaders[i].coordinates.X] = ' ';
            }
        }

        public static void EraseBullets()
        {
            for (int i = 0; i < invaderbullets.GetLength(0); i++)
            {
                battlefield[invaderbullets[i].coordinates.Y, invaderbullets[i].coordinates.X] = ' ';
            }
            for (int i = 0; i < playerbullets.GetLength(0); i++)
            {
                battlefield[playerbullets[i].coordinates.Y, playerbullets[i].coordinates.X] = ' ';
            }
            player.Write();
            foreach (Invader invader in invaders)
            {
                invader.Write();
            }
        }

        public static void MoveInvaders()
        {
            bool log = true;
            EraseInvaders();
            if (invaders[0].direction == Direction.Right)
            {
                foreach (Invader invader in invaders)
                    if (invader.coordinates.X + 1 == battlefield.GetLength(1) - 1) { log = false; break; }
                    else log = true;
                if (!log)
                {
                    for (int i = 0; i < invaders.GetLength(0); i++)
                    {
                        invaders[i].direction = Direction.Down;
                        invaders[i].Move();
                        invaders[i].direction = Direction.Left;
                    }
                }
                else
                {
                    for (int i = 0; i < invaders.GetLength(0); i++)
                    {
                        invaders[i].Move();
                    }
                }
            }
            else
            {
                foreach (Invader invader in invaders)
                    if (invader.coordinates.X - 1 == 0) { log = false; break; }
                    else log = true;
                if (!log)
                {
                    for (int i = 0; i < invaders.GetLength(0); i++)
                    {

                        invaders[i].direction = Direction.Down;
                        invaders[i].Move();
                        invaders[i].direction = Direction.Right;
                    }
                }
                else
                {
                    for (int i = 0; i < invaders.GetLength(0); i++)
                    {
                        invaders[i].Move();
                    }
                }
            }

            for (int i = 0; i < invaders.GetLength(0); i++)
            {
                invaders[i].Write();
            }
            
        }

        //public static void MoveSpecial()
        //{
        //    EraseSpecial();
        //    for (int i = 0; i < invaderspecial.GetLength(0); i++)
        //    {
        //        if (invaderspecial[i].direction == Direction.Right)
        //        {
        //            if (invaderspecial[i].coordinates3.X + 1 == battlefield.GetLength(1) - 1)
        //            {
        //                Array.Resize(ref invaderspecial, 0);
        //            }
        //            else
        //            {
        //                invaderspecial[i].Move();
        //            }
        //        }
        //        else
        //        {
        //            if (invaderspecial[i].coordinates3.X - 1 == 0)
        //            {
        //                Array.Resize(ref invaderspecial, 0);
        //            }
        //            else
        //            {
        //                invaderspecial[i].Move();
        //            }
        //        }
        //        invaderspecial[i].Write();
        //    }
        //}

        public static void MoveBullets()
        {
            EraseBullets();
            for (int i = 0; i < invaderbullets.GetLength(0); i++)
            {
                invaderbullets[i].Move();
                InCheckForCollisions();
                invaderbullets[i].Write();
            }
            for (int i = 0; i < playerbullets.GetLength(0); i++)
            {
                playerbullets[i].Move();
                PlCheckForCollisions();
                playerbullets[i].Write();
            }
        }

        public static void Write()
        {
            for (int i = 0; i < battlefield.GetLength(0); i++)
            {
                for (int j = 0; j < battlefield.GetLength(1); j++)
                {
                    Console.Write(battlefield[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
