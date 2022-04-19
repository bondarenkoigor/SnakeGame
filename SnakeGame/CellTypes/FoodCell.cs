using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.CellTypes
{
    internal class FoodCell : Cell
    {
        public int FoodSize { get; private set; }
        public FoodCell(bool IsBonus)
        {
            if (IsBonus)
            {
                Symbol = '☼';
                FoodSize = 3;
            }
            else
            {
                Symbol = '·';
                FoodSize = 1;
            }
        }

    }
}
