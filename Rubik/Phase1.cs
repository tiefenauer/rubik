using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik
{
    /// <summary>
    /// Solve Phase one of a rubik's cube:
    /// Create the cross on the top (usually white) layer
    /// </summary>
    public class Phase1 : IPhaseSolvable
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
        /// <param name="cube"></param>
        public Phase1(Cubev2 cube)
        {
            this.cube = cube;
            init();
        }

        /// <summary>
        /// Initialization
        /// </summary>
        private void init()
        {
            cube.Rotated += cube_Rotated;
            rotations = new List<Rotation>();

            topColor = cube.Pieces.Where(p => p.Z == 1 && p is Middle).SingleOrDefault().C.Val;
            // determine colors of adjacent pieces
            northColor = cube.Pieces.Where(p => p.Y == -1 && p is Middle).SingleOrDefault().B.Val;
            southColor = cube.Pieces.Where(p => p.Y == +1 && p is Middle).SingleOrDefault().B.Val;
            westColor = cube.Pieces.Where(p => p.X == -1 && p is Middle).SingleOrDefault().A.Val;
            eastColor = cube.Pieces.Where(p => p.X == 1 && p is Middle).SingleOrDefault().A.Val;

        }

        /// <summary>
        /// Solve phase one
        /// </summary>
        public List<Rotation> Solve(Cubev2 cube)
        {
            this.cube = cube;
            init();

            // rotate each edge according to its position so that its side with the same color as top color is on top
            rotateEdge(northEdge);
            rotateEdge(southEdge);
            rotateEdge(westEdge);
            rotateEdge(eastEdge);
            this.cube.Rotated -= cube_Rotated;
            return rotations;
        }

        /// <summary>
        /// Rotate north edge into its correct position
        /// </summary>
        /// <param name="edge"></param>
        private void rotateEdge(Edge edge)
        {
            // step one: bring edge to bottom
            switch (edge.Z)
            {
                // edge is in top layer
                case 1:
                    topToBottom(edge);
                    break;
                // edge is in middle layer
                case 0:
                    middleToBottom(edge);
                    break;
                // edge is in bottom layer
                default:
                    break;
            }
            // step two: match with correct side
            matchEdge(edge);
            // step three: bring edge to top
            bottomToTop(edge);
            // step four: swap colors, if necessary
            if (!edgeColorsMatch(edge)) 
                swapColors(edge);
        }

        /// <summary>
        /// Rotate edge in top layer to bottom layer
        /// </summary>
        /// <param name="edge"></param>
        private void topToBottom(Edge edge)
        {
            Axis axis = (edge.X != 0)?Axis.xAxis:Axis.yAxis;
            int val = (axis == Axis.xAxis)? edge.X:edge.Y;

            cube.Rotate(axis, true, val);
            cube.Rotate(axis, true, val);
        }

        /// <summary>
        /// Rotate edge in middle layer to bottom layer
        /// </summary>
        /// <param name="edge"></param>
        private void middleToBottom(Edge edge)
        {
            Axis axis = Axis.xAxis;
            int val = edge.X;
            Boolean counterclockwise = false;
            if (val == -1 && edge.Y == -1){
                counterclockwise = true;
            }
            if (val == 1 && edge.Y == 1){
                counterclockwise = true;
            }

            cube.Rotate(axis, counterclockwise, val); // zum Boden rotieren
            cube.Rotate(Axis.zAxis, true, -1); // Teil Wegrotieren
            cube.Rotate(axis, !counterclockwise, val); // Fläche wieder zurückrotieren
        }

        /// <summary>
        /// rotate edge in bottom layer to top layer
        /// </summary>
        /// <param name="?"></param>
        private void bottomToTop(Edge edge)
        {
            Axis axis = (edge.X != 0) ? Axis.xAxis : Axis.yAxis;
            int val = (axis == Axis.xAxis) ? edge.X : edge.Y;

            cube.Rotate(axis, true, val);
            cube.Rotate(axis, true, val);
        }

        /// <summary>
        /// check if an edge is matched against the face of the cube.
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        private Boolean isMatched(Edge edge)
        {
            if (edge.X == 1 && (edge.A.Val == eastColor || edge.C.Val == eastColor))
                return true;
            if (edge.X == -1 && (edge.A.Val == westColor|| edge.C.Val == westColor))
                return true;
            if (edge.Y == -1 && (edge.B.Val == northColor|| edge.C.Val == northColor))
                return true;
            if (edge.Y == 1 && (edge.B.Val == southColor || edge.C.Val == southColor))
                return true;

            return false;
        }

        /// <summary>
        /// Check if edge is in position
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        private Boolean edgeColorsMatch(Edge edge)
        {
            if (edge.C.Val == topColor){
                if (edge.X == 1){
                    return (edge.A.Val == eastColor);
                }
                else if (edge.X == -1){
                    return (edge.A.Val == westColor);
                }
                else if (edge.Y == 1){
                    return (edge.B.Val == southColor);
                }
                else if (edge.Y == -1){
                    return (edge.B.Val == northColor);
                }            
            }
            return false;
        }

        /// <summary>
        /// Match edge in bottom Layer with the correct side. Colors do not have to align.
        /// </summary>
        /// <param name="edge"></param>
        private void matchEdge(Edge edge)
        {
            while (!isMatched(edge))
            {
                cube.Rotate(Axis.zAxis, false, -1);
            }
        }

        /// <summary>
        /// Swap colors of an edge that is already in top layer, but with wrong orientation
        /// </summary>
        /// <param name="edge"></param>
        private void swapColors(Edge edge)
        {
            // get axis and value of edge
            Axis axis = (edge.X == 0) ? Axis.yAxis : Axis.xAxis;
            int val = (edge.X == 0) ? edge.Y : edge.X;

            // rotate the axis (axis will change), then rotate the new axis again ==> piece will be in north layer again, but with correct side up
            cube.Rotate(axis, true, val);
            cube.Rotate(Axis.zAxis, false, 1);

            // set val
            if ((val == 1 && axis == Axis.xAxis) || (val == -1 && axis == Axis.yAxis))
                val = 1;
            else
                val = -1;

            axis = (axis == Axis.xAxis) ? Axis.yAxis : Axis.xAxis; // swap axis

            cube.Rotate(axis, true, val);
            cube.Rotate(Axis.zAxis, true, 1);

        }

        /// <summary>
        /// Get all four edges for toplayer
        /// </summary>
        /// <returns></returns>
        private IList<Edge> edges
        {
            get {
                IEnumerable<Edge> allEdges = getPieces<Edge>(cube); //.Where(p => (p.A != null && p.A.Val.Equals(topColor)) || (p.B != null && p.B.Val.Equals(topColor)) || (p.C != null && p.C.Val.Equals(topColor)));
                // in foreach-loop ausgelagert für bessere Lesbarkeit
                List<Edge> result = new List<Edge>();
                foreach (Edge edge in allEdges)
                {
                    if ((edge.A != null && edge.A.Val.Equals(topColor)) ||
                        (edge.B != null && edge.B.Val.Equals(topColor)) ||
                        (edge.C != null && edge.C.Val.Equals(topColor)))
                        result.Add(edge);
                }
                return result;
            }
            
        }

        /// <summary>
        /// Event listener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        /// <param name="rotation"></param>
        void cube_Rotated(object sender, EventArgs data, Rotation rotation)
        {
            rotations.Add(rotation);
        } 

        /// <summary>
        /// get Pieces of a certain type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cube"></param>
        /// <returns></returns>
        private IList<T> getPieces<T>(Cubev2 cube) where T : Edge
        {
            return cube.Pieces.OfType<T>().ToList();
        }

        private Edge northEdge
        {
            get
            {
                return edges.Where(p => p.A != null && p.A.Val.Equals(northColor) || p.B != null && p.B.Val.Equals(northColor) || p.C != null && p.C.Val.Equals(northColor)).SingleOrDefault();
            }
        }
        private Edge southEdge
        {
            get
            {
                return edges.Where(p => p.A != null && p.A.Val.Equals(southColor) || p.B != null && p.B.Val.Equals(southColor) || p.C != null && p.C.Val.Equals(southColor)).SingleOrDefault();
            }
        }

        private Edge westEdge
        {
            get
            {
                return edges.Where(p => p.A != null && p.A.Val.Equals(westColor) || p.B != null && p.B.Val.Equals(westColor) || p.C != null && p.C.Val.Equals(westColor)).SingleOrDefault();
            }
        }

        private Edge eastEdge
        {
            get
            {
                return edges.Where(p => p.A != null && p.A.Val.Equals(eastColor) || p.B != null && p.B.Val.Equals(eastColor) || p.C != null && p.C.Val.Equals(eastColor)).SingleOrDefault();
            }
        }
    }
}
