using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubikModel
{
    /// <summary>
    /// Wrapper for a Rotation that contains an axis, a boolean and the value on which to rotate
    /// </summary>
    public class Rotation
    {
        public Rotation(Axis axis, bool counterclockwise, int val)
        {
            this.Axis = axis;
            this.Counterclockwise = counterclockwise;
            this.Value = val;
        }


        private int value;

        public int Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        private Axis axis;

        public Axis Axis
        {
            get { return axis; }
            set { axis = value; }
        }

        private bool counterclockwise;

        public bool Counterclockwise
        {
            get { return counterclockwise; }
            set { counterclockwise = value; }
        }
    }
}
