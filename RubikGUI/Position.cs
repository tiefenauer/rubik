using RubikModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubikGUI
{
    /// <summary>
    /// Helper Class for position
    /// </summary>
    public class Position
    {
        public Position(int x, int y, int z, Axis axis)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.Axis = axis;
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

        private Axis axis;

        public Axis Axis
        {
            get { return axis; }
            set { axis = value; }
        }
    }
}
