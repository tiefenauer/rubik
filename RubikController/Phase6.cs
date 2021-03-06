﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubikModel
{
	/// <summary>
	/// Phase 6: Bring edges in bottom layer into position (final phase)
	/// </summary>
	public class Phase6 : IPhaseSolvable
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
		// color in bottom
		private String bottomColor;

		// rotations to reconstruct phase one
		private List<Rotation> rotations = new List<Rotation>();

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
		/// Solve phase 6
		/// </summary>
		/// <param name="cube">the cube to be solved</param>
		/// <returns>a list of rotations applied to this cube</returns>
		public List<Rotation> Solve(Cubev2 cube)
		{
			this.cube = cube;
			init();

			// step 1: match one edge (if not already done)
			if (!finished && matchingEdges.Count == 0){
				matchOneEdge();
			}            
			// step 2: Finish cube
            if(!finished)
			    finish();

			return rotations;
		}

		/// <summary>
		/// Create a matching edge
		/// </summary>
		private void matchOneEdge(){
			// F
			cube.Rotate(Axis.yAxis, false, 1);
			// F
			cube.Rotate(Axis.yAxis, false, 1);
			// U ==> D
			cube.Rotate(Axis.zAxis, false, -1);
			// L => R
			cube.Rotate(Axis.xAxis, false, 1);
			// Ri => Li
			cube.Rotate(Axis.xAxis, true, -1);
			// F
			cube.Rotate(Axis.yAxis, false, 1);
			// F
			cube.Rotate(Axis.yAxis, false, 1);
			// Li => Ri
			cube.Rotate(Axis.xAxis, true, 1);
			// R => L
			cube.Rotate(Axis.xAxis, false, -1);
			// U ==> D
			cube.Rotate(Axis.zAxis, false, -1);
			// F
			cube.Rotate(Axis.yAxis, false, 1);
			// F
			cube.Rotate(Axis.yAxis, false, 1);
		}

		/// <summary>
		/// Rotate edges until cube is finished
		/// </summary>
		private void finish()
		{
			Boolean counterclockwise = checkRotationDirection();
			while(!finished)
				rotateEdges(counterclockwise);
		}

		/// <summary>
		/// Check direction of rotation of edges
		/// </summary>
		/// <returns></returns>
		private Boolean checkRotationDirection()
		{
			IEnumerable<Edge> lineX = unMatchingEdges.Where(p => p.X == 0);
			IEnumerable<Edge> lineY = unMatchingEdges.Where(p => p.Y == 0);

			if (lineX.Count() == 2)
			{
				Edge firstEdge = lineX.Where(p => p.X == 1).SingleOrDefault();
				Edge secondEdge = lineY.ElementAt(0);
				if (secondEdge.Y == 1 && firstEdge.A.Val == secondEdge.B.Val)
					return true;
			}
			else
			{
				Edge firstEdge = lineY.Where(p => p.Y == 1).SingleOrDefault();
				Edge secondEdge = lineX.ElementAt(0);
				if (secondEdge.X == -1 && firstEdge.B.Val == secondEdge.A.Val)
					return true;
			}
			return false;
		}

		/// <summary>
		/// Rotate edges clockwise/counterclockwise
		/// </summary>
		/// <param name="counterclockwise"></param>
		private void rotateEdges(Boolean counterclockwise)
		{
			// matching edge is in back
			if (matchingEdges.ElementAt(0).Y == -1)
			{
				// F
				cube.Rotate(Axis.yAxis, false, 1);
				// F
				cube.Rotate(Axis.yAxis, false, 1);
				if (counterclockwise)
				{
					// Ui => Di
					cube.Rotate(Axis.zAxis, true, -1);
				}
				else
				{
					// U => D
					cube.Rotate(Axis.zAxis, false, -1);
				}
				// L => R
				cube.Rotate(Axis.xAxis, false, 1);
				// Ri => Li
				cube.Rotate(Axis.xAxis, true, -1);
				// F
				cube.Rotate(Axis.yAxis, false, 1);
				// F
				cube.Rotate(Axis.yAxis, false, 1);
				// Li => Ri
				cube.Rotate(Axis.xAxis, true, 1);
				// R => L
				cube.Rotate(Axis.xAxis, false, -1);
				if (counterclockwise)
				{
					// Ui => Di
					cube.Rotate(Axis.zAxis, true, -1);
				}
				else
				{
					// U => D
					cube.Rotate(Axis.zAxis, false, -1);
				}                // F
				cube.Rotate(Axis.yAxis, false, 1);
				// F
				cube.Rotate(Axis.yAxis, false, 1);
			}
			// matching edge is in front
			else if (matchingEdges.ElementAt(0).Y == 1)
			{
				// F => B
				cube.Rotate(Axis.yAxis, false, -1);
				// F => B
				cube.Rotate(Axis.yAxis, false, -1);
				if (counterclockwise)
				{
					// Ui => Di
					cube.Rotate(Axis.zAxis, true, -1);
				}
				else
				{
					// U => D
					cube.Rotate(Axis.zAxis, false, -1);
				}                
				// L => L
				cube.Rotate(Axis.xAxis, false, -1);
				// Ri => Ri
				cube.Rotate(Axis.xAxis, true, 1);
				// F => B
				cube.Rotate(Axis.yAxis, false, -1);
				// F => B
				cube.Rotate(Axis.yAxis, false, -1);
				// Li => Li
				cube.Rotate(Axis.xAxis, true, -1);
				// R => R
				cube.Rotate(Axis.xAxis, false, 1);
				if (counterclockwise)
				{
					// Ui => Di
					cube.Rotate(Axis.zAxis, true, -1);
				}
				else
				{
					// U => D
					cube.Rotate(Axis.zAxis, false, -1);
				}                // F => B
				cube.Rotate(Axis.yAxis, false, -1);
				// F => B
				cube.Rotate(Axis.yAxis, false, -1);

			}
			// matching edge is on right side
			else if (matchingEdges.ElementAt(0).X == 1)
			{
				// F => L
				cube.Rotate(Axis.xAxis, false, -1);
				// F => L
				cube.Rotate(Axis.xAxis, false, -1);
				if (counterclockwise)
				{
					// Ui => Di
					cube.Rotate(Axis.zAxis, true, -1);
				}
				else
				{
					// U => D
					cube.Rotate(Axis.zAxis, false, -1);
				}                // L => F
				cube.Rotate(Axis.yAxis, false, 1);
				// Ri => Bi
				cube.Rotate(Axis.yAxis, true, -1);
				// F => L
				cube.Rotate(Axis.xAxis, false, -1);
				// F => L
				cube.Rotate(Axis.xAxis, false, -1);
				// Li => Fi
				cube.Rotate(Axis.yAxis, true, 1);
				// R => B
				cube.Rotate(Axis.yAxis, false, -1);
				if (counterclockwise)
				{
					// Ui => Di
					cube.Rotate(Axis.zAxis, true, -1);
				}
				else
				{
					// U => D
					cube.Rotate(Axis.zAxis, false, -1);
				}                // F => L
				cube.Rotate(Axis.xAxis, false, -1);
				// F => L
				cube.Rotate(Axis.xAxis, false, -1);

			}
			// matching edge is on left side
			else if (matchingEdges.ElementAt(0).X == -1)
			{
				// F => R
				cube.Rotate(Axis.xAxis, false, 1);
				// F => R
				cube.Rotate(Axis.xAxis, false, 1);
				if (counterclockwise)
				{
					// Ui => Di
					cube.Rotate(Axis.zAxis, true, -1);
				}
				else
				{
					// U => D
					cube.Rotate(Axis.zAxis, false, -1);
				}                // L => B
				cube.Rotate(Axis.yAxis, false, -1);
				// Ri => Fi
				cube.Rotate(Axis.yAxis, true, 1);
				// F => R
				cube.Rotate(Axis.xAxis, false, 1);
				// F => R
				cube.Rotate(Axis.xAxis, false, 1);
				// Li => Bi
				cube.Rotate(Axis.yAxis, true, -1);
				// R => F
				cube.Rotate(Axis.yAxis, false, 1);
				if (counterclockwise)
				{
					// Ui => Di
					cube.Rotate(Axis.zAxis, true, -1);
				}
				else
				{
					// U => D
					cube.Rotate(Axis.zAxis, false, -1);
				}                // F => R
				cube.Rotate(Axis.xAxis, false, 1);
				// F => R
				cube.Rotate(Axis.xAxis, false, 1);
			}

		}

		/// <summary>
		/// Check if Phase 6 (cube) is finished
		/// </summary>
		/// <returns></returns>
		private Boolean finished
		{
			get
			{
				return matchingEdges.Count() == 4;
			}

		}

		/// <summary>
		/// Helper funciton to get all matching edges
		/// </summary>
		private List<Edge> matchingEdges
		{
			get
			{
				List<Edge> result = new List<Edge>();
				foreach (Edge edge in edges)
				{
					if (isInPlace(edge))
						result.Add(edge);
				}
				return result;
			}
		}


		/// <summary>
		/// Helper funciton to get all non-matching edges
		/// </summary>
		private List<Edge> unMatchingEdges
		{
			get
			{
				List<Edge> result = new List<Edge>();
				foreach (Edge edge in edges)
				{
					if (!isInPlace(edge))
						result.Add(edge);
				}
				return result;
			}
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
				return edge.B.Val == southColor;

			return edge.B.Val == northColor;
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
	}
}
