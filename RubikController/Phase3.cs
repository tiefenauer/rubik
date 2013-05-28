using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik
{
    /// <summary>
    /// Phase 3: Finish middle layer
    /// </summary>
    public class Phase3: IPhaseSolvable
    {
        Cubev2 cube = null;
        private String topColor;
        private String northColor;
        private String southColor;
        private String westColor;
        private String eastColor;
        private string bottomColor;
        private List<Rotation> rotations = new List<Rotation>();

        public Phase3(Cubev2 cube)
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
            cube.Rotated +=cube_Rotated;
            // step 1: bring edges from top layer to middle layer
            Edge nextEdge = getNextMiddleEdgeInTopLayer();
            while (nextEdge != null)
            {
                topToMiddle(nextEdge);
                nextEdge = getNextMiddleEdgeInTopLayer();
            }

            // step 2: If middle layer contains still wrong edges, swap them one by one with one from the top layer and then continue as in step 1
            while (!finished){
                // get next edge which is already in middle layer, but in the wrong place
                nextEdge = getNextUnmatchedMiddleLayerEdge();
                // get dummy edge to swap with this edge
                Edge dummyEdge = getDummyEdge(nextEdge);
                // swap dummy edge ==> target edge is in top
                swapEdge(dummyEdge, false);
                topToMiddle(nextEdge);

            }
            cube.Rotated -= cube_Rotated;
            return rotations;
        }

        /// <summary>
        /// Bring a Middle layer edge from the top (i.e. bottom) to the middle
        /// </summary>
        /// <param name="edge"></param>
        private void topToMiddle(Edge edge){
            // rotate edge until it is aligned with correct side
            while (!isAligned(edge)) {
                cube.Rotate(Axis.zAxis, false, -1);
            }
            // check swapping direction
            Boolean left = checkSwapDirection(edge);
            swapEdge(edge, left);
        }

        /// <summary>
        /// Swap an edge in top layer with an edge in middle layer (left or right)
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="left"></param>
        private void swapEdge(Edge edge, Boolean left){
            // edge is in south
            if (edge.Y == 1){
                // U/Ui
                cube.Rotate(Axis.zAxis, left, -1);
                // R/Li => L/Ri
                if(left)
                    cube.Rotate(Axis.xAxis, true, 1);
                else
                    cube.Rotate(Axis.xAxis, false, -1);
                // Ui/U
                cube.Rotate(Axis.zAxis, !left, -1);
                // Ri/L
                if(left)
                    cube.Rotate(Axis.xAxis, false, 1);
                else
                    cube.Rotate(Axis.xAxis, true, -1);
                // Ui/U
                cube.Rotate(Axis.zAxis, !left, -1);
                // Fi/F
                cube.Rotate(Axis.yAxis, !left, 1);
                // U/Ui
                cube.Rotate(Axis.zAxis, left, -1);
                // F/Fi
                cube.Rotate(Axis.yAxis, left, 1);
            }
            // edge is in east
            else if (edge.X == 1){
                // U/Ui
                cube.Rotate(Axis.zAxis, left, -1);
                // R/Li => F/Bi
                if (left)
                    cube.Rotate(Axis.yAxis, true, -1);
                else
                    cube.Rotate(Axis.yAxis, false, 1);
                // Ui/U
                cube.Rotate(Axis.zAxis, !left, -1);
                // Ri/L => Bi/F
                if (left)
                    cube.Rotate(Axis.yAxis, false, -1);
                else
                    cube.Rotate(Axis.yAxis, true, 1);
                // Ui/U
                cube.Rotate(Axis.zAxis, !left, -1);
                // Fi/F => Ri/R
                cube.Rotate(Axis.xAxis, !left, 1);
                // U/Ui
                cube.Rotate(Axis.zAxis, left, -1);
                // F/Fi => R/Ri
                cube.Rotate(Axis.xAxis, left, 1);
            }
            // edge is in north
            else if (edge.Y == -1)
            {
                // U/Ui
                cube.Rotate(Axis.zAxis, left, -1);
                // R/Li => R/Li
                if (left)
                    cube.Rotate(Axis.xAxis, true, -1);
                else
                    cube.Rotate(Axis.xAxis, false, 1);
                // Ui/U
                cube.Rotate(Axis.zAxis, !left, -1);
                // Ri/L => Li/R
                if (left)
                    cube.Rotate(Axis.xAxis, false, -1);
                else
                    cube.Rotate(Axis.xAxis, true, 1);
                // Ui/U
                cube.Rotate(Axis.zAxis, !left, -1);
                // Fi/F => Bi/B
                cube.Rotate(Axis.yAxis, !left, -1);
                // U/Ui
                cube.Rotate(Axis.zAxis, left, -1);
                // F/Fi => Bi/B
                cube.Rotate(Axis.yAxis, left, -1);
            }
            // edge is in west
            else
            {
                // U/Ui
                cube.Rotate(Axis.zAxis, left, -1);
                // R/Li => B/Fi
                if (left)
                    cube.Rotate(Axis.yAxis, true, 1);
                else
                    cube.Rotate(Axis.yAxis, false, -1);
                // Ui/U
                cube.Rotate(Axis.zAxis, !left, -1);
                // Ri/L => Bi/F
                if (left)
                    cube.Rotate(Axis.yAxis, false, 1);
                else
                    cube.Rotate(Axis.yAxis, true, -1);
                // Ui/U
                cube.Rotate(Axis.zAxis, !left, -1);
                // Fi/F => Li/L
                cube.Rotate(Axis.xAxis, !left, -1);
                // U/Ui
                cube.Rotate(Axis.zAxis, left, -1);
                // F/Fi => L/Li
                cube.Rotate(Axis.xAxis, left, -1);
            }
        }

        /// <summary>
        /// Get next edge for middle layer, that lies in top layer and can be rotated to middle layer
        /// </summary>
        /// <returns></returns>
        private Edge getNextMiddleEdgeInTopLayer(){
            foreach(Edge edge in edges){
                if (!containsYellow(edge) && edge.Z == -1)
                    return edge;
            }
            return null;
        }

        /// <summary>
        /// Get next edge which belongs to the middle layer but is in the wrong place
        /// </summary>
        /// <returns></returns>
        private Edge getNextUnmatchedMiddleLayerEdge()
        {
            foreach (Edge edge in edges){
                if (!containsYellow(edge) && edge.Z == 0 && !isInPosition(edge))
                    return edge;
            }
            return null;
        }

        /// <summary>
        /// Get dummy edge for a middle-layer-edge to swap with ==> alway use the one to the left
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        private Edge getDummyEdge(Edge edge){
            // edge is in the southeast
            if (edge.X == 1 && edge.Y == 1)
                return edges.Where(p => p.X == 1 && p.Y == 0 & p.Z == -1).First();
            // edge is in the northeast
            if (edge.X == 1 && edge.Y == -1)
                return edges.Where(p => p.X == 0 && p.Y == -1 && p.Z == -1).First();
            // edge is in the northwest
            if (edge.X == -1 && edge.Y ==-1)
                return edges.Where(p => p.X == -1 && p.Y == 0 && p.Z == -1).First();
            // edge is in the southwest
            return edges.Where(p => p.X == 0 && p.Y == 1 && p.Z == -1).First();
        }

        /// <summary>
        /// Check direction in which an edge in the top layer has to be swapped.
        /// </summary>
        /// <param name="edge"></param>
        /// <returns>true if the edge has to be swapped to the left, false if it has to be swapped to the right</returns>
        private Boolean checkSwapDirection(Edge edge){
            // edge is in south
            if (edge.Y == 1)
                return edge.C.Val == eastColor;
            // edge is in east
            if (edge.X == 1)
                return edge.C.Val == northColor;
            // edge is in north
            if (edge.Y == -1)
                return edge.C.Val == westColor;
            
            // edge is in west
            return edge.C.Val == southColor;

        }

        /// <summary>
        /// Check if an edge contains yellow/bottom color
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        private Boolean containsYellow(Edge edge)
        {
            if (edge.A != null && edge.A.Val == bottomColor)
                return true;
            if (edge.B != null && edge.B.Val == bottomColor)
                return true;
            if (edge.C != null && edge.C.Val == bottomColor)
                return true;
            return false;
        }

        /// <summary>
        /// Check if an edge  in top layer is aligned with its cube side
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        private Boolean isAligned(Edge edge)
        {
            if (edge.Z != -1)
                return false;
            if (edge.X == 1 && edge.A.Val != eastColor)
                return false;
            if (edge.X == -1 && edge.A.Val != westColor)
                return false;
            if (edge.Y == 1 && edge.B.Val != southColor)
                return false;
            if (edge.Y == -1 && edge.B.Val != northColor)
                return false;
            return true;
        }

        /// <summary>
        /// Check if an edge is in position (i.e. in its final position)
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        private Boolean isInPosition(Edge edge)
        {
            // edge is in the southeast
            if (edge.X == 1 && edge.Y == 1)
                return edge.A.Val == eastColor && edge.B.Val == southColor;
            // edge is in the northeast
            if (edge.X == 1 && edge.Y == -1)
                return edge.A.Val == eastColor && edge.B.Val == northColor;
            // edge is in the northwest
            if (edge.X == -1 && edge.Y == -1)
                return edge.A.Val == westColor && edge.B.Val == northColor;
            // edge is in the southwest
            return edge.A.Val == westColor && edge.B.Val == southColor;
        }

        /// <summary>
        /// Get edges for middle layer
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
                    if (edge.Z == 0 || edge.Z == -1)
                        result.Add(edge);
                }
                return result;
            }
        }

        private IList<T> getPieces<T>(Cubev2 cube) where T : Piece
        {
            return cube.Pieces.OfType<T>().ToList();
        }

        public Boolean finished
        {
            get
            {
                foreach (Edge edge in edges.Where(p => p.Z == 0))
                {
                    if (!isInPosition(edge))
                        return false;
                }
                return true;
            }
        }

    }
}
