using System;

namespace SnakeGame
{
    internal class Snake
    {
        public int CoordX { get; set; }
        public int CoordY { get; set; }
        public char Symbol { get; set; }
        public Snake(int X, int Y)
        {
            CoordX = X;
            CoordY = Y;
            Symbol = '+';
        }
    }
}
