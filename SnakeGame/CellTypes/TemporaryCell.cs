using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.CellTypes
{
    internal abstract class TemporaryCell : Cell
    {
        public int LifeTime { get; set; }   
        public TemporaryCell(int lifetime)
        {
            LifeTime = lifetime;
        }
    }
}
