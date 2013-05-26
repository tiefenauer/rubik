using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik
{
    public class PhaseThree: IPhaseSolvable
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
            while (piece != null)
            {                
                RotateUntilEdgeIsAtCorrectPosition(piece);
                string middleleft;
                string middleright;
                if (piece.X == 0)
                {
                    if (piece.Y == 1)
                    {
                        middleleft = cube.Pieces.Where(p => p.X == -1 && p is Middle).SingleOrDefault().A.Val;
                        middleright = cube.Pieces.Where(p => p.X == 1 && p is Middle).SingleOrDefault().A.Val;
                    }
                    else
                    {
                        middleright = cube.Pieces.Where(p => p.X == -1 && p is Middle).SingleOrDefault().A.Val;
                        middleleft = cube.Pieces.Where(p => p.X == 1 && p is Middle).SingleOrDefault().A.Val;
                    }
                }
                else
                {
                    if (piece.X == 1)
                    {
                        middleright = cube.Pieces.Where(p => p.Y == -1 && p is Middle).SingleOrDefault().B.Val;
                        middleleft = cube.Pieces.Where(p => p.Y == 1 && p is Middle).SingleOrDefault().B.Val;
                    }
                    else
                    {
                        middleleft = cube.Pieces.Where(p => p.Y == -1 && p is Middle).SingleOrDefault().B.Val;
                        middleright = cube.Pieces.Where(p => p.Y == 1 && p is Middle).SingleOrDefault().B.Val;
                    }
                }
                if (piece.C.Val.Equals(middleleft))
                {
                    RotateToMiddleLeft(piece);
                }
                else if(piece.C.Val.Equals(middleright))
                {
                    RotateToMiddleRight(piece);
                }
                piece = GetEdgeOnLowest();
                
            }
            //while (piece != null)
            //{
                //piece = GetEdgeOnLowest();

            //}
            return rotations;
        }

        private Piece GetEdgeOnLowest(){
            return cube.Pieces.Where(p => p.Z == -1 && !p.ContainsColor(bottomColor) && p is Edge).FirstOrDefault();
        }

        private void RotateUntilEdgeIsAtCorrectPosition(Piece piece)
        {
            if (piece.X != 0)
            {
                Piece middleone = cube.Pieces.Where(p => p.X == piece.X && p is Middle).SingleOrDefault();
                if (!piece.A.Val.Equals(middleone.A.Val))
                {
                    cube.Rotate(Axis.zAxis, false, -1);
                    RotateUntilEdgeIsAtCorrectPosition(piece);
                }
            } else if (piece.Y != 0)
            {
                Piece middleone = cube.Pieces.Where(p => p.Y == piece.Y && p is Middle).SingleOrDefault();
                if (!piece.B.Val.Equals(middleone.B.Val))
                {
                    cube.Rotate(Axis.zAxis, false, -1);
                    RotateUntilEdgeIsAtCorrectPosition(piece);
                }
            }
        }

        private void RotateToMiddleRight(Piece piece)
        {
            Piece middle = this.cube.Pieces.Where(p => p.X == piece.X && p.Y == piece.Y && p is Middle).SingleOrDefault();                     Axis rotateAxis = (piece.X == 0) ? Axis.xAxis : Axis.yAxis;
            Axis otherAxis = rotateAxis == Axis.xAxis? Axis.yAxis: Axis.xAxis;
            int rotateValue = piece.X == -1 || piece.Y == 1 ? 1 : -1;            
            int rotateValueOwn = rotateAxis == Axis.xAxis ? piece.Y : piece.X;
            bool counterclockwise = piece.X == 1 || piece.Y == 1 ? false : true;

            cube.Rotate(Axis.zAxis, true, -1);
            cube.Rotate(rotateAxis, counterclockwise, rotateValue);
            cube.Rotate(Axis.zAxis, false, -1);
            cube.Rotate(rotateAxis, !counterclockwise, rotateValue);
            cube.Rotate(Axis.zAxis, false, -1);
            cube.Rotate(otherAxis, false, rotateValueOwn);
            cube.Rotate(Axis.zAxis, true, -1);
            cube.Rotate(otherAxis, true, rotateValueOwn);                       
        }

        private void RotateToMiddleLeft(Piece piece)
        {
            Piece middle = this.cube.Pieces.Where(p => p.X == piece.X && p.Y == piece.Y && p is Middle).SingleOrDefault(); 
            Axis rotateAxis = (piece.X == 0) ? Axis.xAxis : Axis.yAxis;
            Axis otherAxis = rotateAxis == Axis.xAxis ? Axis.yAxis : Axis.xAxis;
            int rotateValue = piece.X == -1 || piece.Y == 1 ? -1 : 1;   
            int rotateValueOwn = rotateAxis == Axis.xAxis ? piece.Y : piece.X;
            bool counterclockwise = piece.X == 1 || piece.Y == 1 ? false : true;

            cube.Rotate(Axis.zAxis, false, -1);
            cube.Rotate(rotateAxis, counterclockwise, rotateValue);
            cube.Rotate(Axis.zAxis, true, -1);
            cube.Rotate(rotateAxis, !counterclockwise, rotateValue);
            cube.Rotate(Axis.zAxis, true, -1);
            cube.Rotate(otherAxis, true, rotateValueOwn);
            cube.Rotate(Axis.zAxis, false, -1);
            cube.Rotate(otherAxis, false, rotateValueOwn);
        }
    }
}
