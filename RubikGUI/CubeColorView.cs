﻿using Rubik;
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
    public partial class CubeColorView : Form
    {
        public Dictionary<string, Color> colormappings = new Dictionary<string, Color>();
        public Dictionary<string, Position> positionmappings = new Dictionary<string, Position>();

        public Cubev2 cube = new Cubev2();

        public CubeColorView()
        {            
            InitializeComponent();
            PrepareMappings();            
            PaintCurrentCube();
        }

        public void PrepareMappings()
        {
            colormappings.Add("w", Color.White);
            colormappings.Add("b", Color.Blue);
            colormappings.Add("g", Color.Green);
            colormappings.Add("y", Color.Yellow);
            colormappings.Add("o", Color.Orange);
            colormappings.Add("r", Color.Red);

            positionmappings.Add("pictureBox1", new Position(-1, -1, -1, Rubik.Axis.xAxis));
            positionmappings.Add("pictureBox2", new Position(-1, 0, -1, Rubik.Axis.xAxis));
            positionmappings.Add("pictureBox3", new Position(-1, 1, -1, Rubik.Axis.xAxis));
            positionmappings.Add("pictureBox4", new Position(-1, 1, 0, Rubik.Axis.xAxis));
            positionmappings.Add("pictureBox5", new Position(-1, 0, 0, Rubik.Axis.xAxis));
            positionmappings.Add("pictureBox6", new Position(-1, -1, 0, Rubik.Axis.xAxis));
            positionmappings.Add("pictureBox7", new Position(-1, 1, 1, Rubik.Axis.xAxis));
            positionmappings.Add("pictureBox8", new Position(-1, 0, 1, Rubik.Axis.xAxis));
            positionmappings.Add("pictureBox9", new Position(-1, -1, 1, Rubik.Axis.xAxis));

            positionmappings.Add("pictureBox10", new Position(-1, 1, 1, Rubik.Axis.zAxis));
            positionmappings.Add("pictureBox11", new Position(-1, 0, 1, Rubik.Axis.zAxis));
            positionmappings.Add("pictureBox12", new Position(-1, -1, 1, Rubik.Axis.zAxis));
            positionmappings.Add("pictureBox22", new Position(0, 1, 1, Rubik.Axis.zAxis));
            positionmappings.Add("pictureBox23", new Position(0, 0, 1, Rubik.Axis.zAxis));
            positionmappings.Add("pictureBox24", new Position(0, -1, 1, Rubik.Axis.zAxis));
            positionmappings.Add("pictureBox19", new Position(1, 1, 1, Rubik.Axis.zAxis));
            positionmappings.Add("pictureBox20", new Position(1, 0, 1, Rubik.Axis.zAxis));
            positionmappings.Add("pictureBox21", new Position(1, -1, 1, Rubik.Axis.zAxis));

            positionmappings.Add("pictureBox34", new Position(-1, -1, 1, Rubik.Axis.yAxis));
            positionmappings.Add("pictureBox35", new Position(-1, -1, 0, Rubik.Axis.yAxis));
            positionmappings.Add("pictureBox36", new Position(-1, -1, -1, Rubik.Axis.yAxis));
            positionmappings.Add("pictureBox31", new Position(0, -1, 1, Rubik.Axis.yAxis));
            positionmappings.Add("pictureBox32", new Position(0, -1, 0, Rubik.Axis.yAxis));
            positionmappings.Add("pictureBox33", new Position(0, -1, -1, Rubik.Axis.yAxis));
            positionmappings.Add("pictureBox28", new Position(1, -1, 1, Rubik.Axis.yAxis));
            positionmappings.Add("pictureBox29", new Position(1, -1, 0, Rubik.Axis.yAxis));
            positionmappings.Add("pictureBox30", new Position(1, -1, -1, Rubik.Axis.yAxis));

            positionmappings.Add("pictureBox45", new Position(-1, 1, 1, Rubik.Axis.yAxis));
            positionmappings.Add("pictureBox44", new Position(-1, 1, 0, Rubik.Axis.yAxis));
            positionmappings.Add("pictureBox43", new Position(-1, 1, -1, Rubik.Axis.yAxis));
            positionmappings.Add("pictureBox42", new Position(0, 1, 1, Rubik.Axis.yAxis));
            positionmappings.Add("pictureBox41", new Position(0, 1, 0, Rubik.Axis.yAxis));
            positionmappings.Add("pictureBox40", new Position(0, 1, -1, Rubik.Axis.yAxis));
            positionmappings.Add("pictureBox39", new Position(1, 1, 1, Rubik.Axis.yAxis));
            positionmappings.Add("pictureBox38", new Position(1, 1, 0, Rubik.Axis.yAxis));
            positionmappings.Add("pictureBox37", new Position(1, 1, -1, Rubik.Axis.yAxis));

            positionmappings.Add("pictureBox16", new Position(1, 1, 1, Rubik.Axis.xAxis));
            positionmappings.Add("pictureBox17", new Position(1, 0, 1, Rubik.Axis.xAxis));
            positionmappings.Add("pictureBox18", new Position(1, -1, 1, Rubik.Axis.xAxis));
            positionmappings.Add("pictureBox15", new Position(1, -1, 0, Rubik.Axis.xAxis));
            positionmappings.Add("pictureBox14", new Position(1, 0, 0, Rubik.Axis.xAxis));
            positionmappings.Add("pictureBox13", new Position(1, 1, 0, Rubik.Axis.xAxis));
            positionmappings.Add("pictureBox27", new Position(1, -1, -1, Rubik.Axis.xAxis));
            positionmappings.Add("pictureBox26", new Position(1, 0, -1, Rubik.Axis.xAxis));
            positionmappings.Add("pictureBox25", new Position(1, 1, -1, Rubik.Axis.xAxis));

            positionmappings.Add("pictureBox46", new Position(-1, 1, -1, Rubik.Axis.zAxis));
            positionmappings.Add("pictureBox47", new Position(-1, 0, -1, Rubik.Axis.zAxis));
            positionmappings.Add("pictureBox48", new Position(-1, -1, -1, Rubik.Axis.zAxis));
            positionmappings.Add("pictureBox49", new Position(0, 1, -1, Rubik.Axis.zAxis));
            positionmappings.Add("pictureBox50", new Position(0, 0, -1, Rubik.Axis.zAxis));
            positionmappings.Add("pictureBox51", new Position(0, -1, -1, Rubik.Axis.zAxis));
            positionmappings.Add("pictureBox52", new Position(1, 1, -1, Rubik.Axis.zAxis));
            positionmappings.Add("pictureBox53", new Position(1, 0, -1, Rubik.Axis.zAxis));
            positionmappings.Add("pictureBox54", new Position(1, -1, -1, Rubik.Axis.zAxis));
        }

        public void PaintCurrentCube(){
            foreach(KeyValuePair<string, Position> positionmapping in positionmappings){
                Piece piece = cube.Pieces.Where(p => p.X.Equals(positionmapping.Value.X) && p.Y.Equals(positionmapping.Value.Y) && p.Z.Equals(positionmapping.Value.Z)).SingleOrDefault();   
                switch(positionmapping.Value.Axis){    
                    case Axis.xAxis:
                        (this.Controls.Find(positionmapping.Key, false)[0] as PictureBox).BackColor = colormappings[piece.A.Val];
                    break;
                    case Axis.yAxis:
                        (this.Controls.Find(positionmapping.Key, false)[0] as PictureBox).BackColor = colormappings[piece.B.Val];
                    break;
                    case Axis.zAxis:
                        (this.Controls.Find(positionmapping.Key, false)[0] as PictureBox).BackColor = colormappings[piece.C.Val];
                    break;
                }
            }
        }
    }
}