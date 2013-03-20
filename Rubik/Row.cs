using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik
{
    public class Row
    {
        public Row(string name)
        {
            left.name = name;
            middle.name = name;
            right.name = name;
        }

        Square left;
        Square middle;
        Square right;

        public Square Right
        {
            get { return right; }
            set { right = value; }
        }

        public Square Middle
        {
            get { return middle; }
            set { middle = value; }
        }

        public Square Left
        {
            get { return left; }
            set { left = value; }
        }

    }
}
