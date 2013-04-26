﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik
{
    /// <summary>
    /// Solve Phase one of a rubik's cube by creating the cross on the top (usually white) layer
    /// </summary>
    class PhaseOne
    {
        private Cubev2 cube;
        private String topColor;
        private String northColor;
        private String southColor;
        private String westColor;
        private String eastColor;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cube"></param>
        public PhaseOne(Cubev2 cube)
        {
            this.cube = cube;
        }

        /// <summary>
        /// Solve this phase
        /// </summary>
        public void solve()
        {
            topColor = cube.Pieces.Where(p => p.X == 0 && p.Y == 0 && p.Z == 1).SingleOrDefault().C.Val;
            // determine colors of adjacent pieces
            northColor = cube.Pieces.Where(p => p.X == 0 && p.Y == -1 && p.Z == 0).SingleOrDefault().B.Val;
            southColor = cube.Pieces.Where(p => p.X == 0 && p.Y == +1 && p.Z == 0).SingleOrDefault().B.Val;
            westColor = cube.Pieces.Where(p => p.X == -1 && p.Y == 0 && p.Z == 0).SingleOrDefault().A.Val;
            eastColor = cube.Pieces.Where(p => p.X == 1 && p.Y == 0 && p.Z == 0).SingleOrDefault().A.Val;

            IList<Edge> edges = getEdges();
            
            Edge southEdge = edges.Where(p => p.A != null && p.A.Val.Equals(southColor) || p.B != null && p.B.Val.Equals(southColor) || p.C != null && p.C.Val.Equals(southColor)).SingleOrDefault();
            Edge westEdge = edges.Where(p => p.A != null && p.A.Val.Equals(westColor) || p.B != null && p.B.Val.Equals(westColor) || p.C != null && p.C.Val.Equals(westColor)).SingleOrDefault();
            Edge eastEdge = edges.Where(p => p.A != null && p.A.Val.Equals(eastColor) || p.B != null && p.B.Val.Equals(eastColor) || p.C != null && p.C.Val.Equals(eastColor)).SingleOrDefault();

            // rotate each edge according to its position so that its side with the same color as top color is on top
            rotateNorthEdge(edges);
            rotateSouthEdge(southEdge);
            rotateWestEdge(westEdge);
            rotateEastEdge(eastEdge);
        }

        /// <summary>
        /// Rotate north edge into correct position
        /// </summary>
        /// <param name="edge"></param>
        private void rotateNorthEdge(IList<Edge> edges)
        {
            // find north edge
            Edge northEdge = edges.Where(p => p.A != null && p.A.Val.Equals(northColor) || p.B != null && p.B.Val.Equals(northColor) || p.C != null && p.C.Val.Equals(northColor)).SingleOrDefault();
            
            switch (northEdge.Z)
            {
                // edge is already in top layer
                case 1:
                    matchEdge(northEdge);
                    break;
                // edge is in middle layer ==> bring to top
                case 0:
                    break;
                // edge is in bottom layer ==> bring to top
                case -1:

                    break;
            }
            // edge is already in place
            if (northEdge.X == 0 && northEdge.Y == -1 && northEdge.Z == 1 && northEdge.C.Val.Equals(northColor))
            {
                return;
            }
        }

        /// <summary>
        /// Match edge in top Layer with the correct side
        /// </summary>
        /// <param name="edge"></param>
        private void matchEdge(Edge edge)
        {

            // detect other color of edge (one of both is top color)
            /*
            String otherColor;
            if (edge.C.Val.Equals(topColor)){
                otherColor = (edge.A != null) ? edge.A.Val : edge.B.Val;
            }
            else{
                otherColor = edge.C.Val;
            }
            */

            // rotate until other color lines up with correct side
            if(edge.X == 1 || edge.X == -1) {
                if(edge.C.Val.Equals(northColor) || edge.A.Val.Equals(northColor)){
                    cube.Rotate(Axis.zAxis, edge.X == 1, 1);                
                }
                else if(edge.C.Val.Equals(northColor) || edge.A.Val.Equals(southColor)){
                    cube.Rotate(Axis.zAxis, edge.X == -1, 1);
                }
                else if( (edge.X == 1 && (edge.C.Val.Equals(northColor) || edge.A.Val.Equals(westColor))) || (edge.X == -1 && (edge.C.Val.Equals(northColor) || edge.A.Val.Equals(eastColor)) ) ) {
                    cube.Rotate(Axis.zAxis, true, 1);
                    cube.Rotate(Axis.zAxis, true, 1);
                }
            }
            else if (edge.Y == 1 || edge.Y == -1){
                if(edge.B.Val.Equals(eastColor)){
                    cube.Rotate(Axis.zAxis, edge.Y == 1, 1);
                }
                else if(edge.B.Val.Equals(westColor)){
                    cube.Rotate(Axis.zAxis, edge.Y == -1, 1);
                }
                else if( (edge.Y == 1 && (edge.C.Val.Equals(northColor) || edge.B.Val.Equals(northColor))) || (edge.Y == -1 && (edge.C.Val.Equals(northColor) || edge.B.Val.Equals(southColor))) ){
                    cube.Rotate(Axis.zAxis, true, 1);
                    cube.Rotate(Axis.zAxis, true, 1);
                }
            }

            // check if we have to swap colors so that the top color matches
            if(!edge.C.Val.Equals(topColor)){
                swapColors(edge);
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
            cube.Rotate(Axis.zAxis, true, -1);

        }

        /// <summary>
        /// Rotate south edge into correct position
        /// </summary>
        /// <param name="edge"></param>
        private void rotateSouthEdge(Edge edge)
        {

        }

        /// <summary>
        /// Rotate west edge into correct position
        /// </summary>
        /// <param name="edge"></param>
        private void rotateWestEdge(Edge edge)
        {

        }

        /// <summary>
        /// Rotate east edge into correct position
        /// </summary>
        /// <param name="edge"></param>
        private void rotateEastEdge(Edge edge)
        {

        }

        /// <summary>
        /// Get edges for toplayer
        /// </summary>
        /// <returns></returns>
        private IList<Edge> getEdges()
        {
            IEnumerable<Edge> edges = getPieces<Edge>(cube); //.Where(p => (p.A != null && p.A.Val.Equals(topColor)) || (p.B != null && p.B.Val.Equals(topColor)) || (p.C != null && p.C.Val.Equals(topColor)));
            // in foreach-loop ausgelagert für bessere Lesbarkeit
            List<Edge> result = new List<Edge>();
            foreach (Edge edge in edges)
            {
                if ((edge.A != null && edge.A.Val.Equals(topColor)) ||
                    (edge.B != null && edge.B.Val.Equals(topColor)) ||
                    (edge.C != null && edge.C.Val.Equals(topColor)))
                    result.Add(edge);
            }
            return result;
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
    }
}
