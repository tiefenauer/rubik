using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik
{
    public class Phase5 : IPhaseSolvable
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
        private void init(){
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
        public List<Rotation> Solve(Cubev2 cube){
            this.cube = cube;
            init();

            // step 1: Make cross
            makeCross();
            // step 2: Finish corners
            finishCorners();

            return rotations;
        }

        /// <summary>
        /// Step 1 of Phase 5: make cross in bottom layer
        /// </summary>
        private void makeCross()
        {
            // case 1: bottom cross is already there
            if (bottomCrossCreated())
            {
                // nothing to do here
            }
            // case 2: according to pdf page 6
            else if (case2())
            {
                crossFromCase2();
            }
            // case 3: according to pdf page 6
            else if (case3())
            {
                crossFromCase3();
            }
            // case 4: according to pdf page 6
            else if (case4())
            {
                crossFromCase4();
            }
        }

        /// <summary>
        ///  step 2 of Phase 5: Finish Corners
        /// </summary>
        private void finishCorners()
        {
            while (!finished)
            {
                adjustBottomLayer();
                // R
                cube.Rotate(Axis.xAxis, false, -1);
                // U
                cube.Rotate(Axis.zAxis, false, -1);
                // Ri
                cube.Rotate(Axis.xAxis, true, -1);
                // U
                cube.Rotate(Axis.zAxis, false, -1);
                // R
                cube.Rotate(Axis.xAxis, false, -1);
                // U
                cube.Rotate(Axis.zAxis, false, -1);
                // U
                cube.Rotate(Axis.zAxis, false, -1);
                // Ri
                cube.Rotate(Axis.xAxis, true, -1);

            }
        }

        /// <summary>
        /// Rotate bottom layer until one of the positions on p. 7 is reached
        /// </summary>
        private void adjustBottomLayer(){
            Corner southRightCorner = corners.Where(p => p.X == 1 && p.Y == 1 & p.Z == -1).SingleOrDefault();

            IEnumerable<Corner> finishedCorners = corners.Where(p => p.C.Val == bottomColor);
            switch (finishedCorners.Count())
            {
                // Position 1 erstellen
                case 0:
                    while (southRightCorner.B.Val != eastColor)
                    {
                        cube.Rotate(Axis.zAxis, false, -1);
                        southRightCorner = corners.Where(p => p.X == 1 && p.Y == 1 & p.Z == -1).SingleOrDefault();
                    }
                    break;

                // Position 2 erstellen
                case 1:
                    while (southRightCorner.C.Val != bottomColor)
                    {
                        cube.Rotate(Axis.zAxis, false, -1);
                        southRightCorner = corners.Where(p => p.X == 1 && p.Y == 1 & p.Z == -1).SingleOrDefault();
                    }

                    break;

                // Position 3 erstellen
                case 2:
                    while (southRightCorner.B.Val != bottomColor)
                    {
                        cube.Rotate(Axis.zAxis, false, -1);
                        southRightCorner = corners.Where(p => p.X == 1 && p.Y == 1 & p.Z == -1).SingleOrDefault();
                    }

                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Boolean bottomCrossCreated(){
            foreach (Edge edge in edges){
                if (!isInPlace(edge))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Boolean case2(){
            IEnumerable<Piece> topLayerPieces = cube.Pieces.Where(p => p.Z == -1 && !(p is Middle));
            foreach (Piece piece in topLayerPieces){
                if (piece.C.Val == bottomColor)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Boolean case3(){
            foreach (Edge edge in edges){
                if (edge.C.Val == bottomColor){
                    IEnumerable<Edge> otherEdges;
                    if (edge.X == 0){
                        otherEdges = edges.Where(p => p.C.Val == bottomColor && p.Y == 0 && (p.X == -1 || p.X == 1));
                    }
                    else{
                        otherEdges = edges.Where(p => p.C.Val == bottomColor && p.X == 0 && (p.Y == -1 || p.Y == 1));
                    }
                    if (otherEdges.Count() != 0)
                        return true;
                    
                }
            }
            return false;
        }

        private Boolean case4(){
            IEnumerable<Edge> lineX = edges.Where(p => p.X == 0);
            IEnumerable<Edge> lineY = edges.Where(p => p.Y == 0);

            return (lineX.Count() == 2 || lineY.Count() == 2);

            //foreach (Edge edge in edges){
            //    if (edge.C.Val == bottomColor)
            //    {
            //        IEnumerable<Edge> otherEdges;
            //        if (edge.Y == 0)
            //            otherEdges = edges.Where(p => p.Y == edge.Y);
            //        else
            //            otherEdges = edges.Where(p => p.X == edge.X);
            //        if (otherEdges.Count() == 2)
            //            return true;
            //    }
            //}
            //return false;
        }

        /// <summary>
        /// 
        /// </summary>
        private void crossFromCase2(){
            // F
            cube.Rotate(Axis.yAxis, false, 1);
            // R
            cube.Rotate(Axis.xAxis, false, -1);
            // U
            cube.Rotate(Axis.zAxis, false, -1);
            // Ri
            cube.Rotate(Axis.xAxis, true, -1);
            // Ui
            cube.Rotate(Axis.zAxis, true, -1);
            // Fi
            cube.Rotate(Axis.yAxis, true, 1);

            if (case3())
            {
                crossFromCase3();
            }
            else if (case4())
            {
                crossFromCase4();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void crossFromCase3(){
            // Boden rotieren bis Winkel wie in Fall 3 S. 6 ist
            Edge firstEdge = edges.Where(p => p.C.Val == bottomColor).First(); // first edge
            Edge secondEdge = edges.Where(p => p.C.Val == bottomColor && p != firstEdge).First(); // second edge. NOTE: a third edge cannot exist, because then it would be case 4!
            while (!(firstEdge.X == -1 && secondEdge.Y == 1) && !(secondEdge.X == -1 && firstEdge.Y == 1)){
                cube.Rotate(Axis.zAxis, false, -1);
            }

            //// Step 1: f
            cube.Rotate(Axis.yAxis, false, -1);
            // step 2: R
            cube.Rotate(Axis.zAxis, false, -1);
            // step 3: U
            cube.Rotate(Axis.xAxis, false, 1);
            // step 4: Ri
            cube.Rotate(Axis.zAxis, true, -1);
            // step 5: Ui
            cube.Rotate(Axis.xAxis, true, 1);
            // step 6: fi
            cube.Rotate(Axis.yAxis, true, -1);
        }

        /// <summary>
        /// 
        /// </summary>
        private void crossFromCase4(){
            // Boden rotieren bis Gerade wie in Fall 4 S. 6 ist
            IEnumerable<Edge> lineX = edges.Where(p => p.X == 0);
            IEnumerable<Edge> lineY = edges.Where(p => p.Y == 0);

            if( lineX.Count() == 2){
                cube.Rotate(Axis.zAxis, false, -1);
            }

            // F
            cube.Rotate(Axis.yAxis, false, 1);
            // R 
            cube.Rotate(Axis.xAxis, false, -1);
            // U
            cube.Rotate(Axis.zAxis, false, -1);
            // Ri
            cube.Rotate(Axis.xAxis, true, -1);
            // Ui
            cube.Rotate(Axis.zAxis, true, -1);
            // Fi
            cube.Rotate(Axis.yAxis, true, 1);
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

        /// <summary>
        /// Helper function to check if an edge is already in its correct place in the bottom layer
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        private Boolean isInPlace(Edge edge)
        {
            if (edge.X == 1)
                return edge.A.Val == eastColor;
            if (edge.X == -1)
                return edge.A.Val == westColor;
            if (edge.Y == 1)
                return edge.B.Val == northColor;
            
            return edge.B.Val == southColor;
        }

        /// <summary>
        /// Get edges for bottom layer
        /// </summary>
        /// <returns></returns>
        private IList<Edge> edges
        {
            get
            {
                IEnumerable<Edge> allEdges = getPieces<Edge>(cube); //.Where(p => (p.A != null && p.A.Val.Equals(topColor)) || (p.B != null && p.B.Val.Equals(topColor)) || (p.C != null && p.C.Val.Equals(topColor)));
                // in foreach-loop ausgelagert für bessere Lesbarkeit
                List<Edge> result = new List<Edge>();
                foreach (Edge edge in allEdges)
                {
                    if ((edge.A != null && edge.A.Val.Equals(bottomColor)) ||
                        (edge.B != null && edge.B.Val.Equals(bottomColor)) ||
                        (edge.C != null && edge.C.Val.Equals(bottomColor)))
                        result.Add(edge);
                }
                return result;
            }
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

        private Boolean finished
        {
            get
            {
                return cube.Pieces.Where(p => p.Z == -1 && p.C.Val == bottomColor).Count() == 9;
            }
        }

    }


}
