using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik
{
    public class Edge: Piece
    {
        public Edge(int x, int y, int z, PositionValue a, PositionValue b, PositionValue c)
            : base(x, y, z, a, b, c)
        {
        }

        /// <summary>
        /// Performs rotation on Edge Piece
        /// </summary>
        /// <param name="axis">the axis that we rotate around</param>
        /// <param name="counterclockwise">true if rotation is counterclock wise</param>
        public override void Rotate(Axis axis, bool counterclockwise)
        {
            if (axis == Axis.zAxis)
            {
                int help = this.Y;                
                if (counterclockwise)
                {
                    this.Y = help == 0 ? -this.X : this.X;
                    this.X = help;
                }else{
                    int helpx = this.X;
                    this.X = this.X == 0 ? -help : help;
                    this.Y = helpx;
                }

                //Internal Rotation
                PositionValue bval = this.B;
                this.B = this.A == null ? null: new PositionValue(this.Y, this.A.Val);
                this.A = bval == null ? null : new PositionValue(this.X, bval.Val);
            }
            if (axis == Axis.xAxis)
            {
                int help = this.Z;

                if (counterclockwise)
                {
                    this.Z = help == 0 ? -this.Y : this.Y;
                    this.Y = help;
                }
                else
                {
                    int helpx = this.Y;
                    this.Y = this.Y == 0 ? -help : help;
                    this.Z = helpx;
                }

                //Internal Rotation
                PositionValue bval = this.B;
                this.B = this.C == null ? null : new PositionValue(this.Y, this.C.Val);
                this.C = bval == null ? null : new PositionValue(this.Z, bval.Val);
            }
            if (axis == Axis.yAxis)
            {
                int help = this.Z;

                if (counterclockwise)
                {
                    this.Z = help == 0 ? -this.X : this.X;
                    this.X = help;
                }
                else
                {
                    int helpx = this.X;
                    this.X = this.X == 0 ? -help : help;
                    this.Z = helpx;
                }

                //Internal Rotation
                PositionValue aval = this.A;
                this.A = this.C == null ? null : new PositionValue(this.X, this.C.Val);
                this.C = aval == null ? null : new PositionValue(this.Z, aval.Val);
            }
        }
    }
}
