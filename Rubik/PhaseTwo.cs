using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik
{
    public class PhaseTwo
    {
        Cubev2 cube = null;
        private String topColor;
        private String northColor;
        private String southColor;
        private String westColor;
        private String eastColor;

        public PhaseTwo(Cubev2 cube)
        {
            this.cube = cube;
            init();
        }

        private void init()
        {
            topColor = cube.Pieces.Where(p => p.Z == 1 && p is Middle).SingleOrDefault().C.Val;
            // determine colors of adjacent pieces
            northColor = cube.Pieces.Where(p => p.Y == -1 && p is Middle).SingleOrDefault().B.Val;
            southColor = cube.Pieces.Where(p => p.Y == +1 && p is Middle).SingleOrDefault().B.Val;
            westColor = cube.Pieces.Where(p => p.X == -1 && p is Middle).SingleOrDefault().A.Val;
            eastColor = cube.Pieces.Where(p => p.X == 1 && p is Middle).SingleOrDefault().A.Val;
        }

        public void Solve()
        {
            Piece piece = GetCornerOnLowestLevel();
            while (piece != null)
            {
                Piece middleone = this.cube.Pieces.Where(p => p.X == piece.X && p is Middle).SingleOrDefault();
                Piece middletwo = this.cube.Pieces.Where(p => p.Y == piece.Y && p is Middle).SingleOrDefault();
                RotateCubeTilLowestLevelCornerMatches(piece);
                RotateToTop(piece);
                piece = GetCornerOnLowestLevel();
                if (piece == null)
                {
                    piece = GetCornerWrongOnTopLevel();
                    if (piece != null)
                    {
                        int oldy = piece.Y;
                        cube.Rotate(Axis.yAxis, true, piece.Y);
                        cube.Rotate(Axis.zAxis, true, -1);
                        cube.Rotate(Axis.yAxis, false, oldy);
                    }
                }
            }            
        }

        private Piece GetCornerOnLowestLevel()
        {
            try
            {
                return cube.Pieces.Where(p => p.Z == -1 && p is Corner && p.ContainsColor(topColor)).First();
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        private Piece GetCornerWrongOnTopLevel()
        {
            try
            {
                return cube.Pieces.Where(p => p.Z == 1 && p is Corner && p.ContainsColor(topColor) && !isCorrectOnTop(p)).First();
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        private bool isCorrectOnTop(Piece piece)
        {
            Piece middleone = this.cube.Pieces.Where(p => p.X == piece.X && p is Middle).SingleOrDefault();
            Piece middletwo = this.cube.Pieces.Where(p => p.Y == piece.Y && p is Middle).SingleOrDefault();
            Piece middlethree = this.cube.Pieces.Where(p => p.Z == piece.Z && p is Middle).SingleOrDefault();

            if (piece.A.Val.Equals(middleone.A.Val) && piece.B.Val.Equals(middletwo.B.Val) && piece.C.Val.Equals(middlethree.C.Val))
            {
                return true;
            }
            return false;
        }

        private void RotateCubeTilLowestLevelCornerMatches(Piece piece)
        {
            List<string> colorsOfPiece = piece.GetColors();
            Piece middleone = this.cube.Pieces.Where(p => p.X == piece.X && p is Middle).SingleOrDefault();
            Piece middletwo = this.cube.Pieces.Where(p => p.Y == piece.Y && p is Middle).SingleOrDefault();
            while(!colorsOfPiece.Contains(middleone.A.Val) || !colorsOfPiece.Contains(middletwo.B.Val))
            {
                cube.Rotate(Axis.zAxis, false, -1);
                middleone = this.cube.Pieces.Where(p => p.X == piece.X && p is Middle).SingleOrDefault();
                middletwo = this.cube.Pieces.Where(p => p.Y == piece.Y && p is Middle).SingleOrDefault();
            }
        }

        private void RotateToTop(Piece piece)
        {
            Piece middleone = this.cube.Pieces.Where(p => p.X == piece.X && p is Middle).SingleOrDefault();
            Piece middletwo = this.cube.Pieces.Where(p => p.Y == piece.Y && p is Middle).SingleOrDefault();
            while (!middleone.A.Val.Equals(piece.A.Val) || !middletwo.B.Val.Equals(piece.B.Val) || piece.Z != 1)
            {
                cube.Rotate(Axis.yAxis, true, middletwo.Y);
                cube.Rotate(Axis.zAxis, true, -1);
                if (!middleone.A.Val.Equals(piece.A.Val) || !middletwo.B.Val.Equals(piece.B.Val) || piece.Z != 1)
                {
                    cube.Rotate(Axis.yAxis, false, middletwo.Y);
                    cube.Rotate(Axis.zAxis, false, -1);
                }
                //break;
            }
        }
    }
}
