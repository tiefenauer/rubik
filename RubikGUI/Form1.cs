using Rubik;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RubikGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Cubev2 cube = new Cubev2();
            cube.Rotate(Axis.yAxis, true, 1);
            textBox1.Text = cube.ToString();
        }
    }
}
