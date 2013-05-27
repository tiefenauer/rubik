using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik
{
    /// <summary>
    /// Class that saves a value and a Position of a face of a piece.
    /// </summary>
    public class PositionValue
    {
        private string val;

        public string Val
        {
            get { return val; }
            set { val = value; }
        }

        private int key;

        public int Key
        {
            get { return key; }
            set { key = value; }
        }
        public PositionValue(int key, string val)
        {
            this.Key = key;
            this.Val = val;
        }

        public PositionValue()
        {
        }
    }
}
