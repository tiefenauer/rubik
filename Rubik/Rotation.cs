﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik
{
    public class Rotation
    {
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
