using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

namespace Space_Invaders
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(100, 31);
            BattleFront.Create();
            BattleFront.Write();
            Console.SetCursorPosition(52, 0);
            Console.WriteLine("##############################################");
            Console.SetCursorPosition(58, 1);
            Console.WriteLine("Welcome to the Space Invaders game!");
            Console.SetCursorPosition(52, 3);
            Console.WriteLine("The rules are simple:");
            Console.SetCursorPosition(52, 5);
            Console.WriteLine("'Ж' is your ship");
            Console.SetCursorPosition(52, 6);
            Console.WriteLine("'Y', 'T' and 'ф' are your enemies");
            Console.SetCursorPosition(52, 7);
            Console.WriteLine("'▀' icons are defenses, you can hide under them");
            Console.SetCursorPosition(52, 9);
            Console.WriteLine("You move with arrows, shoot with spacebar");
            Console.SetCursorPosition(52, 11);
            Console.WriteLine("Destroy all enemies and try not to get hit!");
            Console.SetCursorPosition(52, 13);
            Console.WriteLine("The game ends when there are no enemies left.");
            Console.SetCursorPosition(52, 18);
            Console.WriteLine("Enjoy!");
            Console.SetCursorPosition(52, 20);
            Console.WriteLine("(press Enter to continue)");
            Console.SetCursorPosition(0, 30);
            Console.ReadLine();
            Console.Clear();
            BattleFront.Write();
            Console.SetCursorPosition(52, 0);
            Console.WriteLine("##############################################");
            Console.SetCursorPosition(52, 1);
            Console.WriteLine("Choose difficulty: (Normal is default)");
            Console.SetCursorPosition(52, 3);
            Console.WriteLine("1) Easy");
            Console.SetCursorPosition(52, 4);
            Console.WriteLine("2) Normal");
            Console.SetCursorPosition(52, 5);
            Console.WriteLine("Hard");
            Console.SetCursorPosition(52, 7);
            Time.Speed = 8;
            Time.modifier = 3;
            BattleFront.invaderbulletsamount = 2;
            Console.WriteLine("(type e, n or h, then press Enter)");
            Console.SetCursorPosition(0, 30);
            string key = Console.ReadLine();
            if (key=="e")
            {
                Time.Speed = 10;
                Time.modifier = 4;
                BattleFront.invaderbulletsamount = 1;
            }
            if (key=="h")
            {
                Time.Speed = 6;
                Time.modifier = 2;
                BattleFront.invaderbulletsamount = 3;
            }
            Time.curspeed = Time.Speed;
            Console.Clear();
            BattleFront.Write();
            Console.SetCursorPosition(52, 0);
            Console.WriteLine("##############################################");
            Console.SetCursorPosition(52, 1);
            Console.WriteLine($"Lives:{BattleFront.player.Lives}");
            Console.SetCursorPosition(52, 3);
            Console.WriteLine($"Points:{BattleFront.Score}");
            Console.SetCursorPosition(52, 5);
            Console.WriteLine($"Enemies left:{BattleFront.invaders.GetLength(0)} ");
            Time timer = new Time();
            timer.Main(50);

        }
    }
}
