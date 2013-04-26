using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik
{
    public class Corner: Piece
    {
        public Corner(int x, int y, int z, PositionValue a, PositionValue b, PositionValue c)
            : base(x, y, z, a, b, c)
        {
        }

        /// <summary>
        /// Internal Rotation method. Performs rotation and piece internal rotation.
        /// </summary>
        /// <param name="axis">the axis that we rotate around</param>
        /// <param name="counterclockwise">true if rotation is counterclock wise</param>
        public override void Rotate(Axis axis, bool counterclockwise)
        {
            if (axis == Axis.zAxis)
            {
                if (this.X == this.Y)
                {
                    if (counterclockwise)
                    {
                        this.Y = -this.X;
                    }
                    else
                    {
                        this.X = -this.Y;
                    }
                }
                else
                {
                    if (counterclockwise)
                    {
                        this.X = this.Y;
                    }
                    else
                    {
                        this.Y = this.X;                        
                    }
                }

                this.X = this.X * this.Z;
                this.Y = this.Y * this.Z;

                PositionValue bval = this.B;
                this.B = this.A == null ? null : new PositionValue(this.Y, this.A.Val);
                this.A = bval == null ? null : new PositionValue(this.X, bval.Val);
            }
            if (axis == Axis.xAxis)
            {
                if (this.Y == this.Z)
                {
                    if (counterclockwise)
                    {
                        this.Z = -this.Y;
                    }
                    else
                    {
                        this.Y = -this.Z;
                    }
                }
                else
                {
                    if (counterclockwise)
                    {
                        this.Y = this.Z;
                    }
                    else
                    {
                        this.Z = this.Y;
                    }
                }

                this.Y = this.Y * this.X;
                this.Z = this.Z * this.X;

                PositionValue bval = this.B;
                this.B = this.C == null ? null : new PositionValue(this.Y, this.C.Val);
                this.C = bval == null ? null : new PositionValue(this.Z, bval.Val);
            }
            if (axis == Axis.yAxis)
            {
                if (this.X == this.Z)
                {
                    if (counterclockwise)
                    {
                        this.Z = -this.X;
                    }
                    else
                    {
                        this.X = -this.Z;
                    }
                }
                else
                {
                    if (counterclockwise)
                    {
                        this.X = this.Z;
                    }
                    else
                    {
                        this.Z = this.X;
                    }
                }

                this.X = this.X * -this.Y;
                this.Z = this.Z * -this.Y;

                PositionValue aval = this.A;
                this.A = this.C == null ? null : new PositionValue(this.X, this.C.Val);
                this.C = aval == null ? null : new PositionValue(this.Z, aval.Val);
            }
        }        
    }
}
