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
                PositionValue aval = this.A;
                this.A = this.C == null ? null : new PositionValue(this.X, this.C.Val);
                this.C = aval == null ? null : new PositionValue(this.Z, aval.Val);
            }
        }

        private void PerformInternalRotation()
        {

        }
    }
}
