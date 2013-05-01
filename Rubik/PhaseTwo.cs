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
                Piece middleone = this.cube.Pieces.Where(p => p.X == piece.A.Key && p is Middle).SingleOrDefault();
                Piece middletwo = this.cube.Pieces.Where(p => p.Y == piece.B.Key && p is Middle).SingleOrDefault();
                RotateCubeTilLowestLevelCornerMatches(piece);
                RotateToTop(piece);
                piece = GetCornerOnLowestLevel();
            }
        }

        private Piece GetCornerOnLowestLevel()
        {
            return cube.Pieces.Where(p => p.Z == -1 && p is Corner && p.ContainsColor(topColor)).First();
        }

        private void RotateCubeTilLowestLevelCornerMatches(Piece piece)
        {
            List<string> colorsOfPiece = piece.GetColors();
            Piece middleone = this.cube.Pieces.Where(p => p.X == piece.A.Key && p is Middle).SingleOrDefault();
            Piece middletwo = this.cube.Pieces.Where(p => p.Y == piece.B.Key && p is Middle).SingleOrDefault();
            while(!colorsOfPiece.Contains(middleone.A.Val) || !colorsOfPiece.Contains(middletwo.B.Val))
            {
                cube.Rotate(Axis.zAxis, false, -1);
                middleone = this.cube.Pieces.Where(p => p.X == piece.A.Key && p is Middle).SingleOrDefault();
                middletwo = this.cube.Pieces.Where(p => p.Y == piece.B.Key && p is Middle).SingleOrDefault();
            }
        }

        private void RotateToTop(Piece piece)
        {
            Piece middleone = this.cube.Pieces.Where(p => p.X == piece.A.Key && p is Middle).SingleOrDefault();
            Piece middletwo = this.cube.Pieces.Where(p => p.Y == piece.B.Key && p is Middle).SingleOrDefault();
            while (!middleone.A.Val.Equals(piece.A.Val) || !middletwo.B.Val.Equals(piece.B.Val))
            {
                cube.Rotate(Axis.xAxis, true, middleone.X);
                cube.Rotate(Axis.zAxis, true, -1);
                cube.Rotate(Axis.xAxis, false, middleone.X);
                cube.Rotate(Axis.zAxis, false, -1);
                //break;
            }
        }
    }
}
