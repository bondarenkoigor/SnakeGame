using System;
using System.Threading;

namespace SnakeGame
{
    internal class Program
    {
        static Game Menu()
        { 
            Console.WriteLine("Введите размер карты(x и y):");
            int x = int.Parse(Console.ReadLine()), y = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите скорость:\n1-медленная\n2-средняя\n3-быстрая");
            int speed = int.Parse(Console.ReadLine());
            if (speed == 1) speed = 500;
            else if (speed == 3) speed = 100;
            else  speed = 300;
            return new Game(x, y, speed);

        }
        static void Main(string[] args)
        {
            Game game = Menu();
            game.Start();
            while (game.GameCycle()) { }
            Console.Clear();
            Console.WriteLine("Game over");
        }
    }
}
