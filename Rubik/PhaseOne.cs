using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik
{
    /// <summary>
    /// Solve Phase one of a rubik's cube by creating the cross on the top (usually white) layer
    /// </summary>
    public class PhaseOne : IPhaseSolvable
    {
        private Boolean stepFinished;
        private Cubev2 cube;
        private String topColor;
        private String northColor;
        private String southColor;
        private String westColor;
        private String eastColor;

        private List<Rotation> rotations = new List<Rotation>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cube"></param>
        public PhaseOne(Cubev2 cube)
        {
            this.cube = cube;
            init();
        }

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

        public bool finished
        {
            get
            {
                List<Piece> toppieces = cube.Pieces.Where(p => p.Z == 1 && p is Edge).ToList();                
                foreach (Piece toppiece in toppieces)
                {
                    //Check if topcolors match
                    if (!toppiece.C.Val.Equals(topColor))
                    {                        
                        return false;
                    }
                    //Check if east and west values match
                    Piece othermiddle = null; 
                    if(toppiece.A.Key != 0){
                        othermiddle = cube.Pieces.Where(p=> p.X == toppiece.A.Key && p is Middle).SingleOrDefault();
                        if(!toppiece.A.Val.Equals(othermiddle.A.Val)){
                            return false;
                        }
                    }else{
                        //Check if front and back values match.
                        othermiddle = cube.Pieces.Where(p=> p.Y == toppiece.B.Key && p is Middle).SingleOrDefault();
                        if(!toppiece.B.Val.Equals(othermiddle.B.Val)){
                            return false;
                        }
                    }                    

                }
                return false;
            }
        }

        /// <summary>
        /// Solve this phase
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

            return rotations;
        }

        /// <summary>
        /// Rotate north edge into correct position
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
        /// 
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
        /// 
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
        /// 
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
        /// 
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
        /// Match edge in top Layer with the correct side
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
        /// Swap colors of an edge that is already
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
        /// Get edges for toplayer
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
        /// 
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
