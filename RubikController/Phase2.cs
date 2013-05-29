using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubikModel
{
    /// <summary>
    /// Phase 2: Bring corners of top layer in Position
    /// </summary>
    public class Phase2 : IPhaseSolvable
    {
        // instance of the cube to be solved
        private Cubev2 cube;
        // color of the face on top (usually white)
        private String topColor;
        // color in north direction
        private String northColor;
        // color in south direction
        private String southColor;
        // color in west direction
        private String westColor;
        // color in east direction
        private String eastColor;

        // rotations to reconstruct phase one
        private List<Rotation> rotations = new List<Rotation>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cube">the cube to be solved</param>
        public Phase2(Cubev2 cube)
        {
            this.cube = cube;
            init();
        }

        /// <summary>
        /// Initialization: Determine colors of adjacend sides
        /// </summary>
        private void init()
        {
            topColor = cube.Pieces.Where(p => p.Z == 1 && p is Middle).SingleOrDefault().C.Val;
            // determine colors of adjacent pieces
            northColor = cube.Pieces.Where(p => p.Y == -1 && p is Middle).SingleOrDefault().B.Val;
            southColor = cube.Pieces.Where(p => p.Y == +1 && p is Middle).SingleOrDefault().B.Val;
            westColor = cube.Pieces.Where(p => p.X == -1 && p is Middle).SingleOrDefault().A.Val;
            eastColor = cube.Pieces.Where(p => p.X == 1 && p is Middle).SingleOrDefault().A.Val;
        }

        /// <summary>
        /// Solve phase two
        /// </summary>
        /// <param name="cube">the cube to be solved</param>
        /// <returns></returns>
        public List<Rotation> Solve(Cubev2 cube)
        {
            //change refrence of this cube to new one
            this.cube = cube;
            init();
            //Clear list of rotations
            rotations = new List<Rotation>();
            //add event for on rotated
            cube.Rotated += cube_Rotated;
            Piece piece = GetCornerOnLowestLevel();
            if (piece == null)
            {
                piece = GetCornerWrongOnTopLevel();
                if (piece != null)
                {
                    Axis rotateAxis = Axis.yAxis;
                    int rotateValue = piece.Y;
                    bool counterclockwise = piece.X == 1 ? true : false;
                    cube.Rotate(rotateAxis, counterclockwise, rotateValue);
                    cube.Rotate(Axis.zAxis, false, -1);
                    cube.Rotate(rotateAxis, !counterclockwise, rotateValue);
                }
            }
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
                        Axis rotateAxis = Axis.yAxis;                       
                        int rotateValue = piece.Y;
                        bool counterclockwise = piece.X == 1 ? true : false;
                        cube.Rotate(rotateAxis, counterclockwise, rotateValue);
                        cube.Rotate(Axis.zAxis, false, -1);
                        cube.Rotate(rotateAxis, !counterclockwise, rotateValue);
                    }
                }
            }
            cube.Rotated -= cube_Rotated;
            return rotations;
        }

        /// <summary>
        /// Event handler that adds the rotations to this handler whenever it happens.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        /// <param name="rotation"></param>
        void cube_Rotated(object sender, EventArgs data, Rotation rotation)
        {
            rotations.Add(rotation);
        }

        /// <summary>
        /// Get if avaiable the Corner on LowestLevel which belongs on top.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Get Corners that are wrong on the Toplevel.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// checks if a piece is correct on top
        /// </summary>
        /// <param name="piece"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Rotates the piece on lowest level till it has reached the correct position
        /// Where its faces match the two sides
        /// </summary>
        /// <param name="piece"></param>
        private void RotateCubeTilLowestLevelCornerMatches(Piece piece)
        {
            List<string> colorsOfPiece = piece.GetColors();
            Piece middleone = this.cube.Pieces.Where(p => p.X == piece.X && p is Middle).SingleOrDefault();
            Piece middletwo = this.cube.Pieces.Where(p => p.Y == piece.Y && p is Middle).SingleOrDefault();
            while (!colorsOfPiece.Contains(middleone.A.Val) || !colorsOfPiece.Contains(middletwo.B.Val))
            {
                cube.Rotate(Axis.zAxis, false, -1);
                middleone = this.cube.Pieces.Where(p => p.X == piece.X && p is Middle).SingleOrDefault();
                middletwo = this.cube.Pieces.Where(p => p.Y == piece.Y && p is Middle).SingleOrDefault();
            }
        }

        /// <summary>
        /// Rotates a corner to the top
        /// </summary>
        /// <param name="piece"></param>
        private void RotateToTop(Piece piece)
        {
            Piece middleone = this.cube.Pieces.Where(p => p.X == piece.X && p is Middle).SingleOrDefault();
            Piece middletwo = this.cube.Pieces.Where(p => p.Y == piece.Y && p is Middle).SingleOrDefault();
            while (!middleone.A.Val.Equals(piece.A.Val) || !middletwo.B.Val.Equals(piece.B.Val) || piece.Z != 1)
            {

                Axis rotateAxis = (piece.Y == piece.X) ? Axis.yAxis : Axis.xAxis;
                bool isNotNegative;
                int rotateValue = 0;
                if (rotateAxis == Axis.yAxis)
                {
                    isNotNegative =false ;
                    rotateValue = piece.Y;
                }
                else
                {
                    isNotNegative = false;
                    rotateValue = piece.X;
                }
                cube.Rotate(rotateAxis, isNotNegative, rotateValue);
                cube.Rotate(Axis.zAxis, false, -1);
                cube.Rotate(rotateAxis, !isNotNegative, rotateValue);
                cube.Rotate(Axis.zAxis, true, -1);
                if (piece.Z == 1 && (!middleone.A.Val.Equals(piece.A.Val) || !middletwo.B.Val.Equals(piece.B.Val)))
                {
                    cube.Rotate(rotateAxis, isNotNegative, rotateValue);
                    cube.Rotate(Axis.zAxis, false, -1);
                    cube.Rotate(rotateAxis, !isNotNegative, rotateValue);
                    RotateCubeTilLowestLevelCornerMatches(piece);
                }


            }
        }
    }
}
