using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.CellTypes
{
    internal class SnakeCell : Cell
    {
        public int LifeTime { get; set; }
        public SnakeCell(int lifetime)
        {
            Symbol = '♦';
            LifeTime = lifetime;
        }
    }
}
