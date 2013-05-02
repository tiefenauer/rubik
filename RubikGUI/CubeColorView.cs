using Rubik;
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

        public CubeColorView()
        {
            cube.InitPieces();
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

            // TO DO: XML-Serialization
            /*
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(List<Piece>));
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"defaultcube.xml");
            writer.Serialize(file, pieces);
            file.Close();
             */
        }

        private void SerializeConfigToCurrentDirectory()
        {
            XmlSerializer writer = new XmlSerializer(typeof(Cubev2), new Type[] { typeof(List<Piece>) });
            using (StreamWriter file = new StreamWriter(@"defaultcube.xml"))
            {
                writer.Serialize(file, this.cube);
            }
        }

        private void DeSerializeConfigToCurrentDirectory()
        {
            XmlSerializer reader = new XmlSerializer(typeof(Cubev2), new Type[] { typeof(List<Piece>), typeof(Edge), typeof(Middle), typeof(Corner) });
            using (XmlReader file = new XmlTextReader(@"defaultcube.xml"))
            {
                try
                {
                   this.cube = (Cubev2) reader.Deserialize(file);                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + ": " + ex.StackTrace);
                }
            }            
        }

        private void saveButtonClick(object sender, EventArgs e)
        {
            SaveCubeConfigToCube();
            PaintCurrentCube();
            SerializeConfigToCurrentDirectory();
        }

        private void solveButtonClick(object sender, EventArgs e)
        {
            //cube.Changed += new ChangedEventHandler(CubeChanged);
            //cube.solveStep();
            PhaseTwo two = new PhaseTwo(this.cube);
            two.Solve();
            PaintCurrentCube();
        }

        private void CubeChanged(object sender, EventArgs e)
        {
            PaintCurrentCube();
        }

        private void loadButtonClick(object sender, EventArgs e)
        {
            DeSerializeConfigToCurrentDirectory();
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

        
    }
}
