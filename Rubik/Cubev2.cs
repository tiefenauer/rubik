using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik
{
    public enum Axis
    {
        xAxis,
        yAxis,
        zAxis
    };

    public class Cubev2
    {
        List<Piece> pieces = new List<Piece>();

        public List<Piece> Pieces
        {
            get { return pieces; }
            set { pieces = value; }
        }

        /// <summary>
        /// front yellow. Top white.
        /// </summary>
        public Cubev2(){
            //TopLayer
            pieces.Add(new Middle(0, 0, 1, null, null, new PositionValue(1, "w")));
            pieces.Add(new Corner(-1, 1, 1, new PositionValue(-1, "o"), new PositionValue(1, "y"), new PositionValue(1, "w")));
            pieces.Add(new Edge(0, 1, 1, null, new PositionValue(1, "y"), new PositionValue(1, "w")));
            pieces.Add(new Corner(1, 1, 1, new PositionValue(1, "r"), new PositionValue(1, "y"), new PositionValue(1, "w")));
            pieces.Add(new Edge(-1, 0, 1, new PositionValue(-1, "o"), null, new PositionValue(1, "w")));
            pieces.Add(new Edge(1, 0, 1, new PositionValue(1, "r"), null, new PositionValue(1, "w")));
            pieces.Add(new Corner(-1, -1, 1, new PositionValue(-1, "o"), new PositionValue(-1, "g"), new PositionValue(1, "w")));
            pieces.Add(new Edge(0, -1, 1, null, new PositionValue(-1, "g"), new PositionValue(1, "w")));
            pieces.Add(new Corner(1, -1, 1, new PositionValue(1, "r"), new PositionValue(-1, "g"), new PositionValue(1, "w")));
            
            //Front Layer
            pieces.Add(new Middle(0, 1, 0, null, new PositionValue(1, "y"), null));
            pieces.Add(new Edge(-1, 1, 0, new PositionValue(-1, "o"), new PositionValue(1, "y"), null));
            pieces.Add(new Edge(1, 1, 0, new PositionValue(1, "r"), new PositionValue(1, "y"), null));
            pieces.Add(new Corner(-1, 1, -1, new PositionValue(-1, "o"), new PositionValue(1, "y"), new PositionValue(-1, "b")));
            pieces.Add(new Edge(0, 1, -1, null, new PositionValue(1, "y"), new PositionValue(-1, "b")));
            pieces.Add(new Corner(1, 1, -1, new PositionValue(1, "r"), new PositionValue(1, "y"), new PositionValue(-1, "b")));

            //Left Layer
            pieces.Add(new Middle(-1, 0, 0, new PositionValue(-1, "o"), null, null));
            pieces.Add(new Edge(-1, -1, 0, new PositionValue(-1, "o"), new PositionValue(-1, "g"), null));
            pieces.Add(new Corner(-1, -1, -1, new PositionValue(-1, "o"), new PositionValue(-1, "g"), new PositionValue(-1, "b")));
            pieces.Add(new Edge(-1, 0, -1, new PositionValue(-1, "o"), null, new PositionValue(-1, "b")));           

            //Right Layer
            pieces.Add(new Middle(1, 0, 0, new PositionValue(1, "r"), null, null));
            pieces.Add(new Edge(1, -1, 0, new PositionValue(1, "r"), new PositionValue(-1, "g"), null));
            pieces.Add(new Edge(1, 0, -1, new PositionValue(1, "r"), null, new PositionValue(-1, "b")));
            pieces.Add(new Corner(1, -1, -1, new PositionValue(-1, "r"), new PositionValue(-1, "g"), new PositionValue(-1, "b")));

            //Bottom Layer
            pieces.Add(new Middle(0, 0, -1, null, null, new PositionValue(-1, "b")));

            //Back Layer
            pieces.Add(new Middle(0, -1, 0, null, new PositionValue(-1, "g"), null));
            pieces.Add(new Edge(0, -1, -1, null, new PositionValue(-1, "g"), new PositionValue(-1, "b")));            
            
            
        }

        /// <summary>
        /// Quick and dirty toString override.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {

            string tab = "\t";
           List<Piece> firstpieces = pieces.Where(p=>p.Y == -1 && p.Z == -1).OrderBy(p=>p.X).ToList();
           string line1 = " \t \t \t" + firstpieces[0].B.Val + tab + firstpieces[1].B.Val + tab + firstpieces[2].B.Val + "\t \t \t \t \t \t ";
           List<Piece> secondpieces = pieces.Where(p => p.Y == -1 && p.Z == 0).OrderBy(p => p.X).ToList();
           string line2 = " \t \t \t" + secondpieces[0].B.Val + tab + secondpieces[1].B.Val + tab + secondpieces[2].B.Val + "\t \t \t \t \t \t ";
           List<Piece> thirdpieces = pieces.Where(p => p.Y == -1 && p.Z == 1).OrderBy(p => p.X).ToList();
           string line3 = " \t \t \t" + thirdpieces[0].B.Val + tab + thirdpieces[1].B.Val + tab + thirdpieces[2].B.Val + "\t \t \t \t \t \t ";
            //Left
           List<Piece> pieces4 = pieces.Where(p => p.X == -1 && p.Y == -1).OrderBy(p => p.Z).ToList();
            //Top
           List<Piece> pieces41 = pieces.Where(p => p.Y == -1 && p.Z == 1).OrderBy(p => p.X).ToList();
            //Right
           List<Piece> pieces42WrongOrder = pieces.Where(p => p.X == 1 && p.Y == -1).OrderBy(p => p.Z).ToList();
            //Bottom
           List<Piece> pieces43WrongOrder = pieces.Where(p => p.Z == -1 && p.Y == -1).OrderBy(p => p.X).ToList();
           string line4 = pieces4[0].A.Val + tab + pieces4[1].A.Val + tab + pieces4[2].A.Val + tab + pieces41[0].C.Val + tab + pieces41[1].C.Val + tab + pieces41[2].C.Val + tab + pieces42WrongOrder[2].A.Val + tab + pieces42WrongOrder[1].A.Val + tab + pieces42WrongOrder[0].A.Val + tab + pieces43WrongOrder[2].C.Val + tab + pieces43WrongOrder[1].C.Val + tab + pieces43WrongOrder[0].C.Val;

           //Left
           List<Piece> pieces5 = pieces.Where(p => p.X == -1 && p.Y == 0).OrderBy(p => p.Z).ToList();
           //Top
           List<Piece> pieces51 = pieces.Where(p => p.Y == 0 && p.Z == 1).OrderBy(p => p.X).ToList();
           //Right
           List<Piece> pieces52WrongOrder = pieces.Where(p => p.X == 1 && p.Y == 0).OrderBy(p => p.Z).ToList();
           //Bottom
           List<Piece> pieces53WrongOrder = pieces.Where(p => p.Z == -1 && p.Y == 0).OrderBy(p => p.X).ToList();
           string line5 = pieces5[0].A.Val + tab + pieces5[1].A.Val + tab + pieces5[2].A.Val + tab + pieces51[0].C.Val + tab + pieces51[1].C.Val + tab + pieces51[2].C.Val + tab + pieces52WrongOrder[2].A.Val + tab + pieces52WrongOrder[1].A.Val + tab + pieces52WrongOrder[0].A.Val + tab + pieces53WrongOrder[2].C.Val + tab + pieces53WrongOrder[1].C.Val + tab + pieces53WrongOrder[0].C.Val;

           //Left
           List<Piece> pieces6 = pieces.Where(p => p.X == -1 && p.Y == 1).OrderBy(p => p.Z).ToList();
           //Top
           List<Piece> pieces61 = pieces.Where(p => p.Y == 1 && p.Z == 1).OrderBy(p => p.X).ToList();
           //Right
           List<Piece> pieces62WrongOrder = pieces.Where(p => p.X == 1 && p.Y == 1).OrderBy(p => p.Z).ToList();
           //Bottom
           List<Piece> pieces63WrongOrder = pieces.Where(p => p.Z == -1 && p.Y == 1).OrderBy(p => p.X).ToList();
           string line6 = pieces6[0].A.Val + tab + pieces6[1].A.Val + tab + pieces6[2].A.Val + tab + pieces61[0].C.Val + tab + pieces61[1].C.Val + tab + pieces61[2].C.Val + tab + pieces62WrongOrder[2].A.Val + tab + pieces62WrongOrder[1].A.Val + tab + pieces62WrongOrder[0].A.Val + tab + pieces63WrongOrder[2].C.Val + tab + pieces63WrongOrder[1].C.Val + tab + pieces63WrongOrder[0].C.Val;

            //Front
           List<Piece> pieces7 = pieces.Where(p => p.Y == 1 && p.Z == 1).OrderBy(p => p.X).ToList();
           string line7 = " \t \t \t" + pieces7[0].B.Val + tab + pieces7[1].B.Val + tab + pieces7[2].B.Val + "\t \t \t \t \t \t ";
           List<Piece> pieces8 = pieces.Where(p => p.Y == 1 && p.Z == 0).OrderBy(p => p.X).ToList();
           string line8 = " \t \t \t" + pieces8[0].B.Val + tab + pieces8[1].B.Val + tab + pieces8[2].B.Val + "\t \t \t \t \t \t ";
           List<Piece> pieces9 = pieces.Where(p => p.Y == 1 && p.Z == -1).OrderBy(p => p.X).ToList();
           string line9 = " \t \t \t" + pieces9[0].B.Val + tab + pieces9[1].B.Val + tab + pieces9[2].B.Val + "\t \t \t \t \t \t ";

           string value = line1 + Environment.NewLine + line2 + Environment.NewLine + line3 + Environment.NewLine + line4 + Environment.NewLine + line5 + Environment.NewLine + line6 + Environment.NewLine + line7 + Environment.NewLine + line8 + Environment.NewLine + line9;
           return value; 
        }

        /// <summary>
        /// Rotates one part of the cube
        /// </summary>
        /// <param name="axis">the axis that we rotate around</param>
        /// <param name="counterclockwise">set true if counterclockwise</param>
        /// <param name="val">Either 1 or -1 by convention, we will not move the middle layers.</param>
        public void Rotate(Axis axis, bool counterclockwise, int val)
        {
            if (val == 0)
            {
                //Middle layer will not be moved in order to keep our fixed axis.
                return;
            }

            switch (axis)
            {
                case Axis.xAxis:
                    foreach(Piece piece in pieces.Where(p=>p.X == val)){
                        piece.Rotate(axis, counterclockwise);
                    }
                    break;
                case Axis.yAxis:
                    foreach (Piece piece in pieces.Where(p => p.Y == val))
                    {
                        piece.Rotate(axis, counterclockwise);
                    }
                    break;
                case Axis.zAxis:
                    foreach (Piece piece in pieces.Where(p => p.Z == val))
                    {
                        piece.Rotate(axis, counterclockwise);
                    }
                    break;
            }
        }
    }
}
