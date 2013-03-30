using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik
{
    public class Middle: Piece
    {
        public Middle(int x, int y, int z, PositionValue a, PositionValue b, PositionValue c)
            : base(x, y, z, a, b, c)
        {
        }

        public override void Rotate(Axis axis, bool counterclockwise)
        {
            //I don't care if you rotate me
        }
    }
}
