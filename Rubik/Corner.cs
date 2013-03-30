using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik
{
    public class Corner: Piece
    {
        public Corner(int x, int y, int z, PositionValue a, PositionValue b, PositionValue c)
            : base(x, y, z, a, b, c)
        {
        }
    }
}
