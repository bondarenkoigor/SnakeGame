using System;
using System.Threading;
using SnakeGame.CellTypes;

namespace SnakeGame
{
    internal class Game
    {
        public int SizeX { get; private set; }
        public int SizeY { get; private set; }
        public Cell[,] Cells { get; private set; }
        public Snake SnakeHead { get; private set; }
        public string SnakeDirection { get; private set; }
        public int SnakeSize { get; private set; }
        public int Speed { get; private set; }


        public Game(int sizeX, int sizeY, int speed)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            Cells = new Cell[SizeX, SizeY];
            SnakeHead = new Snake(SizeX / 2, SizeY / 2);
            SnakeDirection = "right";
            SnakeSize = 1;
            Speed = speed;
        }

        public void Start()
        {
            GenerateMap();
            PrintMap();
            PlaceFood();
            StartThread();
        }

        public void GenerateMap()
        {
            for (int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY; j++)
                {
                    if (i == 0 || j == 0 || i == SizeX - 1 || j == SizeY - 1) Cells[i, j] = new Obstacle();
                    else Cells[i, j] = new Cell();
                }
            }
        }

        public void PrintMap()
        {
            for (int i = 0; i < SizeY; i++)
            {
                for (int j = 0; j < SizeX; j++)
                {
                    Console.Write(Cells[j, i].Symbol);
                }
                Console.WriteLine();
            }
            Console.SetCursorPosition(SnakeHead.CoordX, SnakeHead.CoordY);
            Console.Write(SnakeHead.Symbol);
            Console.SetCursorPosition(0, SizeY + 2);
            Console.Write($"Snake size: {SnakeSize}");
            Console.SetCursorPosition(0, 0);
        }
        public bool GameCycle()
        {
            if (Cells[0, 0] == null) return false;

            MoveSnake();
            PrintMap();

            if (Cells[SnakeHead.CoordX, SnakeHead.CoordY] is FoodCell)
            {
                EatFood((Cells[SnakeHead.CoordX, SnakeHead.CoordY] as FoodCell).FoodSize);
            }

            Thread.Sleep(Speed);
            if (Cells[SnakeHead.CoordX, SnakeHead.CoordY] is Obstacle || Cells[SnakeHead.CoordX, SnakeHead.CoordY] is SnakeCell) return false;
            return true;
        }

        public void StartThread()
        {
            Thread th = new Thread(KeyPressCheck);
            th.Start();
        }

        private void KeyPressCheck(object? obj)
        {
            var key = Console.ReadKey().Key;
            if (key == ConsoleKey.UpArrow && SnakeDirection != "down") SnakeDirection = "up";
            else if (key == ConsoleKey.DownArrow && SnakeDirection != "up") SnakeDirection = "down";
            else if (key == ConsoleKey.LeftArrow && SnakeDirection != "right") SnakeDirection = "left";
            else if (key == ConsoleKey.RightArrow && SnakeDirection != "left") SnakeDirection = "right";
            StartThread();
        }

        public void DeleteSnakeTail()
        {
            for (int i = 0; i < SizeY; i++)
            {
                for (int j = 0; j < SizeX; j++)
                {
                    if (Cells[j, i] is SnakeCell)
                    {
                        SnakeCell tmp = Cells[j, i] as SnakeCell;
                        tmp.LifeTime--;
                        if (tmp.LifeTime == 0) Cells[j, i] = new Cell();
                        else Cells[j, i] = tmp;
                    }
                }
            }
        }

        public void PlaceFood()
        {
            Random rand = new Random();
            int x, y;
            while (true)
            {
                x = rand.Next(0, SizeX);
                y = rand.Next(0, SizeY);
                if (Cells[x, y] is Obstacle || Cells[x, y] is SnakeCell || (x == SnakeHead.CoordX && y == SnakeHead.CoordY)) continue;
                else break;
            }
            bool IsBonus = (rand.Next(0, 10) == 9) ? true : false; //10% шанс бонуса
            Cells[x, y] = new FoodCell(IsBonus);
        }
        public void EatFood(int FoodSize)
        {
            Cells[SnakeHead.CoordX, SnakeHead.CoordY] = new Cell();
            SnakeSize += FoodSize;
            for (int i = 0; i < SizeY; i++)
            {
                for (int j = 0; j < SizeX; j++)
                {
                    if (Cells[j, i] is SnakeCell)
                    {
                        SnakeCell tmp = Cells[j, i] as SnakeCell;
                        tmp.LifeTime += FoodSize;
                        Cells[j, i] = tmp;
                    }
                }
            }
            PlaceFood();
        }

        public void MoveSnake()
        {
            Cells[SnakeHead.CoordX, SnakeHead.CoordY] = new SnakeCell(SnakeSize);
            DeleteSnakeTail();
            if (SnakeDirection == "up") SnakeHead.CoordY -= 1;
            if (SnakeDirection == "down") SnakeHead.CoordY += 1;
            if (SnakeDirection == "left") SnakeHead.CoordX -= 1;
            if (SnakeDirection == "right") SnakeHead.CoordX += 1;
        }
    }
}
