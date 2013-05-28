using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubikModel
{
    [Serializable]
    public class Middle: Piece
    {
        public Middle(int x, int y, int z, PositionValue a, PositionValue b, PositionValue c)
            : base(x, y, z, a, b, c)
        {
        }

        /// <summary>
        /// Empty Constructor for Serialization
        /// </summary>
        public Middle()
        {
        }

        /// <summary>
        /// Rotation method. Does nothing. Middle layers won't be moved.
        /// </summary>
        /// <param name="axis">the axis that we rotate around</param>
        /// <param name="counterclockwise">true if rotation is counterclock wise</param>
        public override void Rotate(Axis axis, bool counterclockwise)
        {
            //I don't care if you rotate me
        }
    }
}
