using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik
{
    public class Phase6 : IPhaseSolvable
    {

        private Cubev2 cube;
        private List<Rotation> rotations = new List<Rotation>();

        private String bottomColor;
        private String northColor;
        private String southColor;
        private String westColor;
        private String eastColor;

        /// <summary>
        /// Initialization
        /// </summary>
        private void init()
        {
            cube.Rotated += cube_Rotated;
            rotations = new List<Rotation>();

            bottomColor = cube.Pieces.Where(p => p.Z == -1 && p is Middle).SingleOrDefault().C.Val;
            // determine colors of adjacent pieces
            northColor = cube.Pieces.Where(p => p.Y == -1 && p is Middle).SingleOrDefault().B.Val;
            southColor = cube.Pieces.Where(p => p.Y == +1 && p is Middle).SingleOrDefault().B.Val;
            westColor = cube.Pieces.Where(p => p.X == -1 && p is Middle).SingleOrDefault().A.Val;
            eastColor = cube.Pieces.Where(p => p.X == 1 && p is Middle).SingleOrDefault().A.Val;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cube"></param>
        /// <returns></returns>
        public List<Rotation> Solve(Cubev2 cube)
        {
            this.cube = cube;
            init();

            // step 1: adjust corners
            adjustCorners();
            // step 2: Finish corners
            finish();

            return rotations;
        }

        /// <summary>
        /// rotate bottom so that two corners match
        /// </summary>
        private void adjustCorners()
        {
            // Step 1: rotate bottom until two corners match
            while (!twoCornersMatch()){
                cube.Rotate(Axis.zAxis, false, -1);
            }

            List<Corner> matchingCorners = new List<Corner>();
            foreach (Corner corner in corners){
                if (cornerMatches(corner))
                    matchingCorners.Add(corner);
            }
            
            // step 2: check if extra step has to be performed (if matching corners are diagonal)
            if (matchingCorners.Count() == 2){
                Corner firstCorner = matchingCorners.ElementAt(0);
                Corner secondCorner = matchingCorners.ElementAt(1);

                if (firstCorner.X != secondCorner.X &&
                    firstCorner.Y != secondCorner.Y)
                {
                    // Ri => Li
                    cube.Rotate(Axis.xAxis, true, -1);
                    // F => F
                    cube.Rotate(Axis.yAxis, false, 1);
                    // Ri => Li
                    cube.Rotate(Axis.xAxis, true, -1);
                    // B => B
                    cube.Rotate(Axis.yAxis, false, -1);
                    // B => B
                    cube.Rotate(Axis.yAxis, false, -1);
                    // R => L
                    cube.Rotate(Axis.xAxis, false, -1);
                    // Fi => Fi
                    cube.Rotate(Axis.yAxis, true, 1);
                    // Ri => Li
                    cube.Rotate(Axis.xAxis, true, -1);
                    // B => B
                    cube.Rotate(Axis.yAxis, false, -1);
                    // B => B
                    cube.Rotate(Axis.yAxis, false, -1);
                    // R => L
                    cube.Rotate(Axis.xAxis, false, -1);
                    // R => L
                    cube.Rotate(Axis.xAxis, false, -1);
                    // Ui => Di
                    cube.Rotate(Axis.zAxis, true, -1);

                    // rotate until two cornres watch
                    while (!twoCornersMatch())
                    {
                        cube.Rotate(Axis.zAxis, false, -1);
                    }
                }
            }


        }

        /// <summary>
        /// Rotate cube according to page 8
        /// </summary>
        private void finish()
        {
            List<Corner> matchingCorners = new List<Corner>();
            foreach (Corner corner in corners){
                if (cornerMatches(corner))
                    matchingCorners.Add(corner);
            }

            Corner firstCorner = matchingCorners.ElementAt(0);
            Corner secondCorner = matchingCorners.ElementAt(1);

            // matching corners are in the back
            if (firstCorner.Y == secondCorner.Y && firstCorner.Y == -1)
            {
                // Ri => Li
                cube.Rotate(Axis.xAxis, true, -1);
                // F
                cube.Rotate(Axis.yAxis, false, 1);
                // Ri => Li
                cube.Rotate(Axis.xAxis, true, -1);
                // B
                cube.Rotate(Axis.yAxis, false, -1);
                // B
                cube.Rotate(Axis.yAxis, false, -1);
                // R => Li
                cube.Rotate(Axis.xAxis, false, -1);
                // Fi
                cube.Rotate(Axis.yAxis, true, 1);
                // Ri => Li
                cube.Rotate(Axis.xAxis, true, -1);
                // B
                cube.Rotate(Axis.yAxis, false, -1);
                // B
                cube.Rotate(Axis.yAxis, false, -1);
                // R => L
                cube.Rotate(Axis.xAxis, false, -1);
                // R => L
                cube.Rotate(Axis.xAxis, false, -1);
                // Ui => Di
                cube.Rotate(Axis.zAxis, true, -1);
            }
            //matching corners are to the right
            else if (firstCorner.X == secondCorner.X && firstCorner.X == 1)
            {
                // Ri ==> Bi
                cube.Rotate(Axis.yAxis, true, -1);
                // F ==> L
                cube.Rotate(Axis.xAxis, false, -1);
                // Ri ==> Bi
                cube.Rotate(Axis.yAxis, true, -1);
                // B => R
                cube.Rotate(Axis.xAxis, false, 1);
                // B => R
                cube.Rotate(Axis.xAxis, false, 1);
                // R ==> B
                cube.Rotate(Axis.yAxis, false, -1);
                // Fi => Li
                cube.Rotate(Axis.xAxis, true, -1);
                // Ri ==> Bi
                cube.Rotate(Axis.yAxis, true, -1);
                // B => R
                cube.Rotate(Axis.xAxis, false, 1);
                // B => R
                cube.Rotate(Axis.xAxis, false, 1);
                // R ==> B
                cube.Rotate(Axis.yAxis, false, -1);
                // R ==> B
                cube.Rotate(Axis.yAxis, false, -1);
                // Ui => Di
                cube.Rotate(Axis.zAxis, true, -1);
            }
            //matching corners are in the front
            else if (firstCorner.Y == secondCorner.Y && firstCorner.Y == 1)
            {
                // Ri => Ri
                cube.Rotate(Axis.xAxis, true, 1);
                // F => B
                cube.Rotate(Axis.yAxis, false, -1);
                // Ri => Ri
                cube.Rotate(Axis.xAxis, true, 1);
                // B => F
                cube.Rotate(Axis.yAxis, false, 1);
                // B => F
                cube.Rotate(Axis.yAxis, false, 1);
                // R => R
                cube.Rotate(Axis.xAxis, false, 1);
                // Fi => Bi
                cube.Rotate(Axis.yAxis, true, -1);
                // Ri => Ri
                cube.Rotate(Axis.xAxis, true, 1);
                // B => F
                cube.Rotate(Axis.yAxis, false, 1);
                // B => F
                cube.Rotate(Axis.yAxis, false, 1);
                // R => R
                cube.Rotate(Axis.xAxis, false, 1);
                // R => R
                cube.Rotate(Axis.xAxis, false, 1);
                // Ui = Di
                cube.Rotate(Axis.zAxis, true, -1);
            }
            //matching corners are to the left
            else if (firstCorner.X == secondCorner.X && firstCorner.X == -1)
            {
                // Ri => Fi
                cube.Rotate(Axis.yAxis, true, 1);
                // F => R
                cube.Rotate(Axis.xAxis, false, 1);
                // Ri => Fi
                cube.Rotate(Axis.yAxis, true, 1);
                // B ==> L
                cube.Rotate(Axis.xAxis, false, -1);
                // B ==> L
                cube.Rotate(Axis.xAxis, false, -1);
                // R => F
                cube.Rotate(Axis.yAxis, false, 1);
                // Fi => Ri
                cube.Rotate(Axis.xAxis, true, 1);
                // Ri => Fi
                cube.Rotate(Axis.yAxis, true, 1);
                // B ==> L
                cube.Rotate(Axis.xAxis, false, -1);
                // B ==> L
                cube.Rotate(Axis.xAxis, false, -1);
                // R => F
                cube.Rotate(Axis.yAxis, false, 1);
                // R => F
                cube.Rotate(Axis.yAxis, false, 1);
                // Ui
                cube.Rotate(Axis.zAxis, true, -1);
            }
        }

        private Boolean twoCornersMatch()
        {
            int matches = 0;
            foreach (Corner corner in corners){
                if (cornerMatches(corner))
                {
                    matches++;
                }
            }
            return matches >= 2;
        }

        /// <summary>
        /// Helper function to determine if a corner is in the right position
        /// </summary>
        /// <param name="corner"></param>
        /// <returns></returns>
        private Boolean cornerMatches(Corner corner)
        {
            if (corner.C.Val == bottomColor)  {
                if (corner.X == 1 && corner.Y == -1)
                    return corner.A.Val == eastColor && corner.B.Val == northColor;
                if (corner.X == 1 && corner.Y == 1)
                    return corner.A.Val == eastColor && corner.B.Val == southColor;
                if (corner.X == -1 && corner.Y == -1)
                    return corner.A.Val == westColor && corner.B.Val == northColor;
                if (corner.X == -1 && corner.Y == 1)
                    return corner.A.Val == westColor && corner.B.Val == southColor;
                                        
            }
            return false;
        }

        private IList<T> getPieces<T>(Cubev2 cube) where T : Piece
        {
            return cube.Pieces.OfType<T>().ToList();
        }

        private IList<Corner> corners
        {
            get
            {
                IEnumerable<Corner> allCorners = getPieces<Corner>(cube);
                List<Corner> result = new List<Corner>();
                foreach (Corner corner in allCorners)
                {
                    if ((corner.A != null && corner.A.Val.Equals(bottomColor)) ||
                        (corner.B != null && corner.B.Val.Equals(bottomColor)) ||
                        (corner.C != null && corner.C.Val.Equals(bottomColor)))
                        result.Add(corner);
                }
                return result;
            }

        }

        /// <summary>
        /// Event handler for cube rotation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        /// <param name="rotation"></param>
        void cube_Rotated(object sender, EventArgs data, Rotation rotation)
        {
            rotations.Add(rotation);
        }

        private Boolean finished
        {
            get{
                foreach(Corner corner in corners){
                    if(!cornerMatches(corner))
                        return false;
                }
                return true;
            }
        }


    }
}
