﻿using RubikModel;
using RubikController;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace RubikGUI
{
    public partial class CubeColorView : Form
    {
        public Dictionary<string, Color> colormappings = new Dictionary<string, Color>();
        public Dictionary<string, Position> positionmappings = new Dictionary<string, Position>();

        public Cubev2 cube = new Cubev2();        
        private PictureBox selectedColor = null;

        private RubikSolvController solver;
        private Rotation lastRotation;

        public CubeColorView()
        {
            cube.InitPieces();
            InitializeComponent();
            PrepareMappings();            
            PaintCurrentCube();
            cube.Rotated += cube_Rotated;
            solver = new RubikSolvController(cube);
        }

        void cube_Rotated(object sender, EventArgs data, Rotation rotation)
        {
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

            positionmappings.Add("pictureBox1", new Position(-1, -1, -1, RubikModel.Axis.xAxis));
            positionmappings.Add("pictureBox2", new Position(-1, 0, -1, RubikModel.Axis.xAxis));
            positionmappings.Add("pictureBox3", new Position(-1, 1, -1, RubikModel.Axis.xAxis));
            positionmappings.Add("pictureBox4", new Position(-1, 1, 0, RubikModel.Axis.xAxis));
            positionmappings.Add("pictureBox5", new Position(-1, 0, 0, RubikModel.Axis.xAxis));
            positionmappings.Add("pictureBox6", new Position(-1, -1, 0, RubikModel.Axis.xAxis));
            positionmappings.Add("pictureBox7", new Position(-1, 1, 1, RubikModel.Axis.xAxis));
            positionmappings.Add("pictureBox8", new Position(-1, 0, 1, RubikModel.Axis.xAxis));
            positionmappings.Add("pictureBox9", new Position(-1, -1, 1, RubikModel.Axis.xAxis));

            positionmappings.Add("pictureBox10", new Position(-1, 1, 1, RubikModel.Axis.zAxis));
            positionmappings.Add("pictureBox11", new Position(-1, 0, 1, RubikModel.Axis.zAxis));
            positionmappings.Add("pictureBox12", new Position(-1, -1, 1, RubikModel.Axis.zAxis));
            positionmappings.Add("pictureBox22", new Position(0, 1, 1, RubikModel.Axis.zAxis));
            positionmappings.Add("pictureBox23", new Position(0, 0, 1, RubikModel.Axis.zAxis));
            positionmappings.Add("pictureBox24", new Position(0, -1, 1, RubikModel.Axis.zAxis));
            positionmappings.Add("pictureBox19", new Position(1, 1, 1, RubikModel.Axis.zAxis));
            positionmappings.Add("pictureBox20", new Position(1, 0, 1, RubikModel.Axis.zAxis));
            positionmappings.Add("pictureBox21", new Position(1, -1, 1, RubikModel.Axis.zAxis));

            positionmappings.Add("pictureBox34", new Position(-1, -1, 1, RubikModel.Axis.yAxis));
            positionmappings.Add("pictureBox35", new Position(-1, -1, 0, RubikModel.Axis.yAxis));
            positionmappings.Add("pictureBox36", new Position(-1, -1, -1, RubikModel.Axis.yAxis));
            positionmappings.Add("pictureBox31", new Position(0, -1, 1, RubikModel.Axis.yAxis));
            positionmappings.Add("pictureBox32", new Position(0, -1, 0, RubikModel.Axis.yAxis));
            positionmappings.Add("pictureBox33", new Position(0, -1, -1, RubikModel.Axis.yAxis));
            positionmappings.Add("pictureBox28", new Position(1, -1, 1, RubikModel.Axis.yAxis));
            positionmappings.Add("pictureBox29", new Position(1, -1, 0, RubikModel.Axis.yAxis));
            positionmappings.Add("pictureBox30", new Position(1, -1, -1, RubikModel.Axis.yAxis));

            positionmappings.Add("pictureBox45", new Position(-1, 1, 1, RubikModel.Axis.yAxis));
            positionmappings.Add("pictureBox44", new Position(-1, 1, 0, RubikModel.Axis.yAxis));
            positionmappings.Add("pictureBox43", new Position(-1, 1, -1, RubikModel.Axis.yAxis));
            positionmappings.Add("pictureBox42", new Position(0, 1, 1, RubikModel.Axis.yAxis));
            positionmappings.Add("pictureBox41", new Position(0, 1, 0, RubikModel.Axis.yAxis));
            positionmappings.Add("pictureBox40", new Position(0, 1, -1, RubikModel.Axis.yAxis));
            positionmappings.Add("pictureBox39", new Position(1, 1, 1, RubikModel.Axis.yAxis));
            positionmappings.Add("pictureBox38", new Position(1, 1, 0, RubikModel.Axis.yAxis));
            positionmappings.Add("pictureBox37", new Position(1, 1, -1, RubikModel.Axis.yAxis));

            positionmappings.Add("pictureBox16", new Position(1, 1, 1, RubikModel.Axis.xAxis));
            positionmappings.Add("pictureBox17", new Position(1, 0, 1, RubikModel.Axis.xAxis));
            positionmappings.Add("pictureBox18", new Position(1, -1, 1, RubikModel.Axis.xAxis));
            positionmappings.Add("pictureBox15", new Position(1, -1, 0, RubikModel.Axis.xAxis));
            positionmappings.Add("pictureBox14", new Position(1, 0, 0, RubikModel.Axis.xAxis));
            positionmappings.Add("pictureBox13", new Position(1, 1, 0, RubikModel.Axis.xAxis));
            positionmappings.Add("pictureBox27", new Position(1, -1, -1, RubikModel.Axis.xAxis));
            positionmappings.Add("pictureBox26", new Position(1, 0, -1, RubikModel.Axis.xAxis));
            positionmappings.Add("pictureBox25", new Position(1, 1, -1, RubikModel.Axis.xAxis));

            positionmappings.Add("pictureBox46", new Position(-1, 1, -1, RubikModel.Axis.zAxis));
            positionmappings.Add("pictureBox47", new Position(-1, 0, -1, RubikModel.Axis.zAxis));
            positionmappings.Add("pictureBox48", new Position(-1, -1, -1, RubikModel.Axis.zAxis));
            positionmappings.Add("pictureBox49", new Position(0, 1, -1, RubikModel.Axis.zAxis));
            positionmappings.Add("pictureBox50", new Position(0, 0, -1, RubikModel.Axis.zAxis));
            positionmappings.Add("pictureBox51", new Position(0, -1, -1, RubikModel.Axis.zAxis));
            positionmappings.Add("pictureBox52", new Position(1, 1, -1, RubikModel.Axis.zAxis));
            positionmappings.Add("pictureBox53", new Position(1, 0, -1, RubikModel.Axis.zAxis));
            positionmappings.Add("pictureBox54", new Position(1, -1, -1, RubikModel.Axis.zAxis));
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

        private void tileClick(object sender, EventArgs e)
        {
            (sender as PictureBox).BackColor = selectedColor.BackColor;
        }

        private void Color_Click(object sender, EventArgs e)
        {
            if (selectedColor != null)
                selectedColor.BorderStyle = BorderStyle.None;
            selectedColor = sender as PictureBox;
            selectedColor.BorderStyle = BorderStyle.FixedSingle;
        }

        private void SaveCubeConfigToCube(){
            List<Piece> pieces = new List<Piece>();

            foreach (KeyValuePair<string, Position> positionmapping in positionmappings)
            {
                Piece piece = cube.Pieces.Where(p => p.X.Equals(positionmapping.Value.X) && p.Y.Equals(positionmapping.Value.Y) && p.Z.Equals(positionmapping.Value.Z)).SingleOrDefault();
                switch (positionmapping.Value.Axis)
                {
                    case Axis.xAxis:
                        piece.A = new PositionValue(piece.X, colormappings.Where(cm => cm.Value.Equals((this.Controls.Find(positionmapping.Key, false)[0] as PictureBox).BackColor)).SingleOrDefault().Key);                            
                        break;
                    case Axis.yAxis:
                        piece.B = new PositionValue(piece.Y, colormappings.Where(cm => cm.Value.Equals((this.Controls.Find(positionmapping.Key, false)[0] as PictureBox).BackColor)).SingleOrDefault().Key);
                        break;
                    case Axis.zAxis:
                        piece.C = new PositionValue(piece.Z, colormappings.Where(cm => cm.Value.Equals((this.Controls.Find(positionmapping.Key, false)[0] as PictureBox).BackColor)).SingleOrDefault().Key);
                        break;
                }
                pieces.Add(piece);
            }

        }

        private void SerializeConfigToCurrentDirectory()
        {
            XmlSerializer writer = new XmlSerializer(typeof(Cubev2), new Type[] { typeof(List<Piece>) });
            using (StreamWriter file = new StreamWriter(@"defaultcube.xml"))
            {
                writer.Serialize(file, this.cube);
            }
        }

        private void SerializeConfigToDirectory(string filename)
        {
            XmlSerializer writer = new XmlSerializer(typeof(Cubev2), new Type[] { typeof(List<Piece>) });
            using (StreamWriter file = new StreamWriter(@filename))
            {
                writer.Serialize(file, this.cube);
            }
        }

        private void DeSerializeConfigFromDirectory(string filename)
        {
            XmlSerializer reader = new XmlSerializer(typeof(Cubev2), new Type[] { typeof(List<Piece>), typeof(Edge), typeof(Middle), typeof(Corner) });
            using (XmlReader file = new XmlTextReader(@filename))
            {
                try
                {
                    this.cube = (Cubev2)reader.Deserialize(file);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + ": " + ex.StackTrace);
                }
            }
        }

        private void CubeChanged(object sender, EventArgs e)
        {
            PaintCurrentCube();
        }

        private void rotateT_Click(object sender, EventArgs e)
        {
            cube.Rotate(Axis.zAxis, false, 1);
            PaintCurrentCube();
        }

        private void rotateTi_Click(object sender, EventArgs e)
        {
            cube.Rotate(Axis.zAxis, true, 1);
            PaintCurrentCube();
        }

        private void rotateD_Click(object sender, EventArgs e)
        {
            cube.Rotate(Axis.zAxis, false, -1);
            PaintCurrentCube();
        }

        private void rotateDi_Click(object sender, EventArgs e)
        {
            cube.Rotate(Axis.zAxis, true, -1);
            PaintCurrentCube();
        }

        private void rotateL_Click(object sender, EventArgs e)
        {
            cube.Rotate(Axis.xAxis, false, -1);
            PaintCurrentCube();
        }

        private void rotateLi_Click(object sender, EventArgs e)
        {
            cube.Rotate(Axis.xAxis, true, -1);
            PaintCurrentCube();
        }

        private void rotateR_Click(object sender, EventArgs e)
        {
            cube.Rotate(Axis.xAxis, false, 1);
            PaintCurrentCube();
        }

        private void rotateRi_Click(object sender, EventArgs e)
        {
            cube.Rotate(Axis.xAxis, true, 1);
            PaintCurrentCube();
        }

        private void rotateB_Click(object sender, EventArgs e)
        {
            cube.Rotate(Axis.yAxis, false, -1);
            PaintCurrentCube();
        }

        private void rotateBi_Click(object sender, EventArgs e)
        {
            cube.Rotate(Axis.yAxis, true, -1);
            PaintCurrentCube();
        }

        private void rotateF_Click(object sender, EventArgs e)
        {
            cube.Rotate(Axis.yAxis, false, 1);
            PaintCurrentCube();
        }

        private void rotateFi_Click(object sender, EventArgs e)
        {
            cube.Rotate(Axis.yAxis, true, 1);
            PaintCurrentCube();
        }

        private void saveCubeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveCubeConfigToCube();
            SaveFileDialog fd = new SaveFileDialog();
            fd.AddExtension = true;
            fd.DefaultExt = "xml";
            fd.Filter = "XML file with cube definition|*.xml";
            DialogResult res = fd.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                string filename = fd.FileName;
                SerializeConfigToDirectory(filename);
            }
        }

        private void loadCubeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.AddExtension = true;
            fd.DefaultExt = "xml";
            fd.Filter = "XML file with cube definition|*.xml";
            DialogResult res = fd.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                DeSerializeConfigFromDirectory(fd.FileName);
            }
            PaintCurrentCube();
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveCubeConfigToCube();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveCubeConfigToCube();
            solver.Solve(cube);
            PaintCurrentCube();
            dgvRotations.DataSource = solver.Rotations;
            dgvRotations.Columns[2].HeaderText = "i";
            dgvRotations.AutoResizeColumns();
        }

        private void Step_Click(object sender, EventArgs e)
        {
            Rotation rotation = solver.Step();
            if (rotation == null)
            {
                return;
            }
            cube.Rotate(rotation);
            lastRotation = rotation;
            Piece rotatedPiece = null;            
            switch (rotation.Axis)
            {
                case Axis.xAxis:
                    rotatedPiece = cube.Pieces.Where(p=>p.X == rotation.Value && p is Middle).SingleOrDefault();
                    lblRotation.Text = rotatedPiece.X == 1 ? "R" : "L";
                    lblRotation.Text += rotation.Counterclockwise ? "i" : string.Empty;
                    break;
                case Axis.yAxis:
                    rotatedPiece = cube.Pieces.Where(p=>p.Y == rotation.Value && p is Middle).SingleOrDefault();
                    lblRotation.Text = rotatedPiece.Y == 1 ? "F" : "B";
                    lblRotation.Text += rotation.Counterclockwise ? "i" : string.Empty;
                    break;
                case Axis.zAxis:
                    rotatedPiece = cube.Pieces.Where(p=>p.Z == rotation.Value && p is Middle).SingleOrDefault();
                    lblRotation.Text = rotatedPiece.X == 1 ? "U" : "D";
                    lblRotation.Text += rotation.Counterclockwise ? "i" : string.Empty;
                    break;
            }
            pictureBox65.BackColor = colormappings[rotatedPiece.GetColors()[0]];
            lblStep.Text = string.Format("{0} / {1}", solver.Counter+1, solver.RotationsCount);
            PaintCurrentCube();
        }

        private void stepback_Click(object sender, EventArgs e)
        {
            Rotation rotation = solver.PrevStep();
            if (rotation == null)
            {
                return;
            }
            cube.Rotate(new Rotation(lastRotation.Axis, !lastRotation.Counterclockwise, lastRotation.Value));
            lastRotation = rotation;
            Piece rotatedPiece = null;
            switch (rotation.Axis)
            {
                case Axis.xAxis:
                    rotatedPiece = cube.Pieces.Where(p => p.X == rotation.Value && p is Middle).SingleOrDefault();
                    break;
                case Axis.yAxis:
                    rotatedPiece = cube.Pieces.Where(p => p.Y == rotation.Value && p is Middle).SingleOrDefault();
                    break;
                case Axis.zAxis:
                    rotatedPiece = cube.Pieces.Where(p => p.Z == rotation.Value && p is Middle).SingleOrDefault();
                    break;
            }
            pictureBox65.BackColor = colormappings[rotatedPiece.GetColors()[0]];
            lblStep.Text = string.Format("{0} / {1}", solver.Counter+1, solver.RotationsCount);
            PaintCurrentCube();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Rotation rotation = solver.Step();
            Piece rotatedPiece = null;
            while (rotation != null)
            {
                cube.Rotate(rotation);
                lastRotation = rotation;                
                switch (rotation.Axis)
                {
                    case Axis.xAxis:
                        rotatedPiece = cube.Pieces.Where(p => p.X == rotation.Value && p is Middle).SingleOrDefault();
                        lblRotation.Text = rotatedPiece.X == 1 ? "R" : "L";
                        lblRotation.Text += rotation.Counterclockwise ? "i" : string.Empty;
                        break;
                    case Axis.yAxis:
                        rotatedPiece = cube.Pieces.Where(p => p.Y == rotation.Value && p is Middle).SingleOrDefault();
                        lblRotation.Text = rotatedPiece.Y == 1 ? "F" : "B";
                        lblRotation.Text += rotation.Counterclockwise ? "i" : string.Empty;
                        break;
                    case Axis.zAxis:
                        rotatedPiece = cube.Pieces.Where(p => p.Z == rotation.Value && p is Middle).SingleOrDefault();
                        lblRotation.Text = rotatedPiece.X == 1 ? "U" : "D";
                        lblRotation.Text += rotation.Counterclockwise ? "i" : string.Empty;
                        break;
                }
                rotation = solver.Step();
            }
            if (rotatedPiece != null)
            {
                pictureBox65.BackColor = colormappings[rotatedPiece.GetColors()[0]];
            }
            lblStep.Text = string.Format("{0} / {1}", solver.Counter + 1, solver.RotationsCount);
            PaintCurrentCube();
        }        

        
    }
}
