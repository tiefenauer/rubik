using RubikModel;
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
    public partial class CubeView : Form
    {
        private Cubev2 cube = new Cubev2();

        public CubeView()
        {
            InitializeComponent();            
            textBox1.Text = cube.ToString();
        }

        // Rotate right
        private void rotateRightButton_Click(object sender, EventArgs e)
        {
            cube.Rotate(Axis.xAxis, false, +1);
            textBox1.Text = cube.ToString();
        }
        private void rotateRightInvertedButton_Click(object sender, EventArgs e)
        {
            cube.Rotate(Axis.xAxis, true, +1);
            textBox1.Text = cube.ToString();
        }

        // Rotate left
        private void rotateLeftButton_Click(object sender, EventArgs e)
        {
            cube.Rotate(Axis.xAxis, false, -1);
            textBox1.Text = cube.ToString();
        }
        private void rotateLeftInvertedButton_Click(object sender, EventArgs e)
        {
            cube.Rotate(Axis.xAxis, true, -1);
            textBox1.Text = cube.ToString();
        }

        // Rotate up
        private void rotateUpButton_Click(object sender, EventArgs e)
        {
            cube.Rotate(Axis.zAxis, false, +1);
            textBox1.Text = cube.ToString();
        }
        private void rotateUpInvertedButton_Click(object sender, EventArgs e)
        {
            cube.Rotate(Axis.zAxis, true, +1);
            textBox1.Text = cube.ToString();
        }

        // Rotate down
        private void rotateDownButton_Click(object sender, EventArgs e)
        {
            cube.Rotate(Axis.zAxis, false, -1);
            textBox1.Text = cube.ToString();
        }
        private void rotateDownInvertedButton_Click(object sender, EventArgs e)
        {
            cube.Rotate(Axis.zAxis, true, -1);
            textBox1.Text = cube.ToString();
        }

        // Rotate front
        private void rotateFrontButton_Click(object sender, EventArgs e)
        {
            cube.Rotate(Axis.yAxis, false, +1);
            textBox1.Text = cube.ToString();
        }
        private void rotateFrontInvertedButton_Click(object sender, EventArgs e)
        {
            cube.Rotate(Axis.yAxis, true, +1);
            textBox1.Text = cube.ToString();
        }

        // Rotate back
        private void rotateBackButton_Click(object sender, EventArgs e)
        {
            cube.Rotate(Axis.yAxis, false, -1);
            textBox1.Text = cube.ToString();
        }
        private void rotateBackInvertedButton_Click(object sender, EventArgs e)
        {
            cube.Rotate(Axis.yAxis, true, -1);
            textBox1.Text = cube.ToString();
        }


    }
}
