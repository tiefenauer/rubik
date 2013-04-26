using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik
{
    /// <summary>
    /// Resembles a piece of the cube.
    /// </summary>
    public abstract class Piece
    {
        public Piece()
        {
        }
        /// <summary>
        /// Constructor. Internal coordinates have the same heading as external.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="a">internal x</param>
        /// <param name="b">internal y</param>
        /// <param name="c">internal z</param>
        public Piece(int x, int y, int z, PositionValue a, PositionValue b, PositionValue c)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.A = a;
            this.B = b;
            this.C = c;            
        }

        private int x;

        public int X
        {
            get { return x; }
            set { x = value; }
        }
        private int y;

        public int Y
        {
            get { return y; }
            set { y = value; }
        }
        private int z;

        public int Z
        {
            get { return z; }
            set { z = value; }
        }

        private PositionValue a;

        public PositionValue A
        {
            get { return a; }
            set { a = value; }
        }

        private PositionValue b;

        public PositionValue B
        {
            get { return b; }
            set { b = value; }
        }
        private PositionValue c;

        public PositionValue C
        {
            get { return c; }
            set { c = value; }
        }

        public virtual void Rotate(Axis axis, bool counterclockwise)
        {
        }
    }
}
