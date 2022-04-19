using System;
using System.Threading;

namespace SnakeGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(30, 15);
            game.Start();
            while(game.GameCycle())
            {
                Thread.Sleep(300);
            }
            Console.Clear();
            Console.WriteLine("Game over");
            Console.ReadLine();
        }
    }
}
