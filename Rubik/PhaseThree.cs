using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik
{
    public class PhaseThree
    {
        Cubev2 cube = null;
        private String topColor;
        private String northColor;
        private String southColor;
        private String westColor;
        private String eastColor;
        private string bottomColor;
        private List<Rotation> rotations = new List<Rotation>();

        public PhaseThree(Cubev2 cube)
        {
            this.cube = cube;
            init();
        }

        void cube_Rotated(object sender, EventArgs data, Rotation rotation)
        {
            rotations.Add(rotation);
        }

        private void init()
        {
            topColor = cube.Pieces.Where(p => p.Z == 1 && p is Middle).SingleOrDefault().C.Val;
            bottomColor = cube.Pieces.Where(p => p.Z == -1 && p is Middle).SingleOrDefault().C.Val;
            // determine colors of adjacent pieces
            northColor = cube.Pieces.Where(p => p.Y == -1 && p is Middle).SingleOrDefault().B.Val;
            southColor = cube.Pieces.Where(p => p.Y == +1 && p is Middle).SingleOrDefault().B.Val;
            westColor = cube.Pieces.Where(p => p.X == -1 && p is Middle).SingleOrDefault().A.Val;
            eastColor = cube.Pieces.Where(p => p.X == 1 && p is Middle).SingleOrDefault().A.Val;
        }


        public List<Rotation> Solve(Cubev2 cube)
        {
            this.cube = cube;
            init();
            //Clear list of rotations
            rotations = new List<Rotation>();
            //add event for on rotated
            cube.Rotated += cube_Rotated;
            Piece piece = GetEdgeOnLowest();
            RotateUntilEdgeIsAtCorrectPosition(piece);
            //while (piece != null)
            //{
                //piece = GetEdgeOnLowest();

            //}
            return rotations;
        }

        private Piece GetEdgeOnLowest(){
            return cube.Pieces.Where(p => p.Z == -1 && !p.C.Val.Equals(bottomColor) && p is Edge).FirstOrDefault();
        }

        private void RotateUntilEdgeIsAtCorrectPosition(Piece piece)
        {
            if (piece.A != null)
            {
                Piece middleone = cube.Pieces.Where(p => p.X == piece.A.Key && p is Middle).SingleOrDefault();
                if (!piece.A.Val.Equals(middleone.A.Val))
                {
                    cube.Rotate(Axis.zAxis, false, -1);
                    RotateUntilEdgeIsAtCorrectPosition(piece);
                }
            } else if (piece.B != null)
            {
                Piece middleone = cube.Pieces.Where(p => p.Y == piece.B.Key && p is Middle).SingleOrDefault();
                if (!piece.B.Val.Equals(middleone.B.Val))
                {
                    cube.Rotate(Axis.zAxis, false, -1);
                    RotateUntilEdgeIsAtCorrectPosition(piece);
                }
            }
        }
    }
}
