using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik
{
    public class Cubev2
    {
        List<Piece> pieces = new List<Piece>();

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
            
            pieces.Add(new Middle(0, 1, 0, null, new PositionValue(1, "y"), null));
            pieces.Add(new Edge(-1, 1, 0, new PositionValue(-1, "o"), new PositionValue(1, "y"), null));
            pieces.Add(new Edge(1, 1, 0, new PositionValue(1, "r"), new PositionValue(1, "y"), null));
            pieces.Add(new Corner(-1, 1, -1, new PositionValue(-1, "o"), new PositionValue(1, "y"), new PositionValue(-1, "b")));
            pieces.Add(new Edge(0, 1, 0, null, new PositionValue(1, "y"), new PositionValue(-1, "b")));
            pieces.Add(new Corner(1, 1, -1, new PositionValue(1, "r"), new PositionValue(1, "y"), new PositionValue(-1, "b")));

            pieces.Add(new Middle(-1, 0, 0, new PositionValue(-1, "o"), null, null));
            pieces.Add(new Edge(-1, -1, 0, new PositionValue(-1, "o"), new PositionValue(-1, "g"), null));
            pieces.Add(new Corner(-1, -1, -1, new PositionValue(-1, "o"), new PositionValue(-1, "g"), new PositionValue(-1, "b")));
            pieces.Add(new Edge(-1, 0, -1, new PositionValue(-1, "o"), null, new PositionValue(-1, "b")));
            pieces.Add(new Corner(-1, 1, -1, new PositionValue(-1, "o"), new PositionValue(1, "y"), new PositionValue(-1, "b")));

            pieces.Add(new Middle(1, 0, 0, new PositionValue(1, "r"), null, null));
            pieces.Add(new Edge(1, -1, 0, new PositionValue(1, "r"), new PositionValue(-1, "g"), null));
            pieces.Add(new Edge(1, 0, -1, new PositionValue(1, "r"), null, new PositionValue(-1, "b")));
            pieces.Add(new Corner(1, -1, -1, new PositionValue(-1, "r"), new PositionValue(-1, "g"), new PositionValue(-1, "b")));

            pieces.Add(new Middle(0, 0, -1, null, null, new PositionValue(-1, "b")));
            pieces.Add(new Middle(0, -1, 0, null, new PositionValue(-1, "g"), null));
            pieces.Add(new Edge(0, -1, -1, null, new PositionValue(-1, "g"), new PositionValue(-1, "b")));            
            
            
        }

        public override string ToString()
        {

           List<Piece> firstpieces = pieces.Where(p=>p.Y == -1 && p.Z == -1).OrderBy(p=>p.X).ToList();
           string line1 = "000" + firstpieces[0].B.Val + firstpieces[1].B.Val + firstpieces[2].B.Val + "000000";
           List<Piece> secondpieces = pieces.Where(p => p.Y == -1 && p.Z == 0).OrderBy(p => p.X).ToList();
           string line2 = "000" + secondpieces[0].B.Val + secondpieces[1].B.Val + secondpieces[2].B.Val + "000000";
           List<Piece> thirdpieces = pieces.Where(p => p.Y == -1 && p.Z == 1).OrderBy(p => p.X).ToList();
           string line3 = "000" + thirdpieces[0].B.Val + thirdpieces[1].B.Val + thirdpieces[2].B.Val + "000000";
            //Left
           List<Piece> pieces4 = pieces.Where(p => p.X == -1 && p.Y == -1).OrderBy(p => p.Z).ToList();
            //Top
           List<Piece> pieces41 = pieces.Where(p => p.Y == -1 && p.Z == 1).OrderBy(p => p.X).ToList();
            //Right
           List<Piece> pieces42WrongOrder = pieces.Where(p => p.X == 1 && p.Y == -1).OrderBy(p => p.Z).ToList();
            //Bottom
           List<Piece> pieces43WrongOrder = pieces.Where(p => p.Z == -1 && p.Y == -1).OrderBy(p => p.X).ToList();
           string line4 = pieces4[0].A.Val + pieces4[1].A.Val + pieces4[2].A.Val + pieces41[0].C.Val + pieces41[1].C.Val + pieces41[2].C.Val + pieces42WrongOrder[2].A.Val + pieces42WrongOrder[1].A.Val + pieces42WrongOrder[0].A.Val + pieces43WrongOrder[2].C.Val + pieces43WrongOrder[1].C.Val + pieces43WrongOrder[0].C.Val;

           //Left
           List<Piece> pieces5 = pieces.Where(p => p.X == -1 && p.Y == 0).OrderBy(p => p.Z).ToList();
           //Top
           List<Piece> pieces51 = pieces.Where(p => p.Y == 0 && p.Z == 1).OrderBy(p => p.X).ToList();
           //Right
           List<Piece> pieces52WrongOrder = pieces.Where(p => p.X == 1 && p.Y == 0).OrderBy(p => p.Z).ToList();
           //Bottom
           List<Piece> pieces53WrongOrder = pieces.Where(p => p.Z == -1 && p.Y == 0).OrderBy(p => p.X).ToList();
           string line5 = pieces5[0].A.Val + pieces5[1].A.Val + pieces5[2].A.Val + pieces51[0].C.Val + pieces51[1].C.Val + pieces51[2].C.Val + pieces52WrongOrder[2].A.Val + pieces52WrongOrder[1].A.Val + pieces52WrongOrder[0].A.Val + pieces53WrongOrder[2].C.Val + pieces53WrongOrder[1].C.Val + pieces53WrongOrder[0].C.Val;

           //Left
           List<Piece> pieces6 = pieces.Where(p => p.X == -1 && p.Y == 1).OrderBy(p => p.Z).ToList();
           //Top
           List<Piece> pieces61 = pieces.Where(p => p.Y == 1 && p.Z == 1).OrderBy(p => p.X).ToList();
           //Right
           List<Piece> pieces62WrongOrder = pieces.Where(p => p.X == 1 && p.Y == 1).OrderBy(p => p.Z).ToList();
           //Bottom
           List<Piece> pieces63WrongOrder = pieces.Where(p => p.Z == -1 && p.Y == 1).OrderBy(p => p.X).ToList();
           string line6 = pieces6[0].A.Val + pieces6[1].A.Val + pieces6[2].A.Val + pieces61[0].C.Val + pieces61[1].C.Val + pieces61[2].C.Val + pieces62WrongOrder[2].A.Val + pieces62WrongOrder[1].A.Val + pieces62WrongOrder[0].A.Val + pieces63WrongOrder[2].C.Val + pieces63WrongOrder[1].C.Val + pieces63WrongOrder[0].C.Val;

           List<Piece> pieces7 = pieces.Where(p => p.Y == 1 && p.Z == 1).OrderBy(p => p.X).ToList();
           string line7 = "000" + pieces7[0].B.Val + pieces7[1].B.Val + pieces7[2].B.Val + "000000";
           List<Piece> pieces8 = pieces.Where(p => p.Y == 1 && p.Z == 0).OrderBy(p => p.X).ToList();
           string line8 = "000" + pieces8[0].B.Val + pieces8[1].B.Val + pieces8[2].B.Val + "000000";
           List<Piece> pieces9 = pieces.Where(p => p.Y == 1 && p.Z == -1).OrderBy(p => p.X).ToList();
           string line9 = "000" + pieces9[0].B.Val + pieces9[1].B.Val + pieces9[2].B.Val + "000000";

           string value = line1 + Environment.NewLine + line2 + Environment.NewLine + line3 + Environment.NewLine + line4 + Environment.NewLine + line5 + Environment.NewLine + line6 + Environment.NewLine + line7 + Environment.NewLine + line8 + Environment.NewLine + line9;
           return value; 
        }
    }
}
