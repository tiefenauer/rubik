using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik
{
    /// <summary>
    /// Axis Enum
    /// </summary>
    public enum Axis
    {
        xAxis,
        yAxis,
        zAxis
    };

    public delegate void ChangedEventHandler(object sender, EventArgs e);
    /// <summary>
    /// Event that is fired if the cube performs a rotation.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="data"></param>
    /// <param name="rotation"></param>
    public delegate void RotatedEventHandler (object sender,  EventArgs data, Rotation rotation);
  

    [Serializable]
    public class Cubev2 : ICloneable
    {       
        /// <summary>
        /// Rotation event.
        /// </summary>
        public event RotatedEventHandler Rotated;
        protected void OnRotated(object sender, EventArgs e, Rotation rotation)
        {            
            if (Rotated != null)
            {                
                Rotated(this, e,rotation);
            }
        }

        public event ChangedEventHandler Changed;
        protected virtual void OnChanged(EventArgs e)
        {
            if (Changed != null)
                Changed(this, e);
        }

        /// <summary>
        /// List that contains all of the 26 pieces of the cube
        /// </summary>
        List<Piece> pieces = new List<Piece>();

        
        public List<Piece> Pieces
        {
            get { return pieces; }
            set { pieces = value; }
        }

        /// <summary>
        /// front yellow. Top white.
        /// </summary>
        public Cubev2(){
            
        }

        /// <summary>
        /// Init Method instead of a constructor.
        /// Because else we get doubled values for pieces on deserialization (because constructor runs anyways)
        /// </summary>
        public void InitPieces()
        {
            //TopLayer
            pieces.Add(new Middle(0, 0, 1, null, null, new PositionValue(1, "w")));
            pieces.Add(new Corner(-1, 1, 1, new PositionValue(-1, "r"), new PositionValue(1, "y"), new PositionValue(1, "b")));
            pieces.Add(new Edge(0, 1, 1, null, new PositionValue(1, "w"), new PositionValue(1, "b")));
            pieces.Add(new Corner(1, 1, 1, new PositionValue(1, "w"), new PositionValue(1, "b"), new PositionValue(1, "r")));
            pieces.Add(new Edge(-1, 0, 1, new PositionValue(-1, "w"), null, new PositionValue(1, "o")));
            pieces.Add(new Edge(1, 0, 1, new PositionValue(1, "g"), null, new PositionValue(1, "y")));
            pieces.Add(new Corner(-1, -1, 1, new PositionValue(-1, "b"), new PositionValue(-1, "w"), new PositionValue(1, "o")));
            pieces.Add(new Edge(0, -1, 1, null, new PositionValue(-1, "b"), new PositionValue(1, "r")));
            pieces.Add(new Corner(1, -1, 1, new PositionValue(1, "r"), new PositionValue(-1, "g"), new PositionValue(1, "y")));            
            //pieces.Add(new Middle(0, 0, 1, null, null, new PositionValue(1, "w")));
            //pieces.Add(new Corner(-1, 1, 1, new PositionValue(-1, "o"), new PositionValue(1, "y"), new PositionValue(1, "w")));
            //pieces.Add(new Edge(0, 1, 1, null, new PositionValue(1, "y"), new PositionValue(1, "w")));
            //pieces.Add(new Corner(1, 1, 1, new PositionValue(1, "r"), new PositionValue(1, "y"), new PositionValue(1, "w")));
            //pieces.Add(new Edge(-1, 0, 1, new PositionValue(-1, "o"), null, new PositionValue(1, "w")));
            //pieces.Add(new Edge(1, 0, 1, new PositionValue(1, "r"), null, new PositionValue(1, "w")));
            //pieces.Add(new Corner(-1, -1, 1, new PositionValue(-1, "o"), new PositionValue(-1, "g"), new PositionValue(1, "w")));
            //pieces.Add(new Edge(0, -1, 1, null, new PositionValue(-1, "g"), new PositionValue(1, "w")));
            //pieces.Add(new Corner(1, -1, 1, new PositionValue(1, "r"), new PositionValue(-1, "g"), new PositionValue(1, "w")));


            //Front Layer
            pieces.Add(new Middle(0, 1, 0, null, new PositionValue(1, "r"), null));
            pieces.Add(new Edge(-1, 1, 0, new PositionValue(-1, "r"), new PositionValue(1, "g"), null));
            pieces.Add(new Edge(1, 1, 0, new PositionValue(1, "r"), new PositionValue(1, "w"), null));
            pieces.Add(new Corner(-1, 1, -1, new PositionValue(-1, "w"), new PositionValue(1, "r"), new PositionValue(-1, "g")));
            pieces.Add(new Edge(0, 1, -1, null, new PositionValue(1, "o"), new PositionValue(-1, "y")));
            pieces.Add(new Corner(1, 1, -1, new PositionValue(1, "g"), new PositionValue(1, "w"), new PositionValue(-1, "o")));
            
            //pieces.Add(new Middle(0, 1, 0, null, new PositionValue(1, "y"), null));
            //pieces.Add(new Edge(-1, 1, 0, new PositionValue(-1, "o"), new PositionValue(1, "y"), null));
            //pieces.Add(new Edge(1, 1, 0, new PositionValue(1, "r"), new PositionValue(1, "y"), null));
            //pieces.Add(new Corner(-1, 1, -1, new PositionValue(-1, "o"), new PositionValue(1, "y"), new PositionValue(-1, "b")));
            //pieces.Add(new Edge(0, 1, -1, null, new PositionValue(1, "y"), new PositionValue(-1, "b")));
            //pieces.Add(new Corner(1, 1, -1, new PositionValue(1, "r"), new PositionValue(1, "y"), new PositionValue(-1, "b")));
            

            //Left Layer
            pieces.Add(new Middle(-1, 0, 0, new PositionValue(-1, "g"), null, null));
            pieces.Add(new Edge(-1, -1, 0, new PositionValue(-1, "w"), new PositionValue(-1, "g"), null));
            pieces.Add(new Corner(-1, -1, -1, new PositionValue(-1, "y"), new PositionValue(-1, "o"), new PositionValue(-1, "b")));
            pieces.Add(new Edge(-1, 0, -1, new PositionValue(-1, "o"), null, new PositionValue(-1, "g")));            
            //pieces.Add(new Middle(-1, 0, 0, new PositionValue(-1, "o"), null, null));
            //pieces.Add(new Edge(-1, -1, 0, new PositionValue(-1, "o"), new PositionValue(-1, "g"), null));
            //pieces.Add(new Corner(-1, -1, -1, new PositionValue(-1, "o"), new PositionValue(-1, "g"), new PositionValue(-1, "b")));
            //pieces.Add(new Edge(-1, 0, -1, new PositionValue(-1, "o"), null, new PositionValue(-1, "b")));           
            

            //Right Layer
            pieces.Add(new Middle(1, 0, 0, new PositionValue(1, "b"), null, null));
            pieces.Add(new Edge(1, -1, 0, new PositionValue(1, "r"), new PositionValue(-1, "y"), null));
            pieces.Add(new Edge(1, 0, -1, new PositionValue(1, "b"), null, new PositionValue(-1, "o")));
            pieces.Add(new Corner(1, -1, -1, new PositionValue(1, "y"), new PositionValue(-1, "o"), new PositionValue(-1, "g")));           
            //pieces.Add(new Middle(1, 0, 0, new PositionValue(1, "r"), null, null));
            //pieces.Add(new Edge(1, -1, 0, new PositionValue(1, "r"), new PositionValue(-1, "g"), null));
            //pieces.Add(new Edge(1, 0, -1, new PositionValue(1, "r"), null, new PositionValue(-1, "b")));
            //pieces.Add(new Corner(1, -1, -1, new PositionValue(-1, "r"), new PositionValue(-1, "g"), new PositionValue(-1, "b")));           

            //Bottom Layer
            pieces.Add(new Middle(0, 0, -1, null, null, new PositionValue(-1, "y")));
            //pieces.Add(new Middle(0, 0, -1, null, null, new PositionValue(-1, "b")));

            //Back Layer
            pieces.Add(new Middle(0, -1, 0, null, new PositionValue(-1, "o"), null));
            pieces.Add(new Edge(0, -1, -1, null, new PositionValue(-1, "y"), new PositionValue(-1, "b")));            
            //pieces.Add(new Middle(0, -1, 0, null, new PositionValue(-1, "g"), null));
            //pieces.Add(new Edge(0, -1, -1, null, new PositionValue(-1, "g"), new PositionValue(-1, "b")));                        
        }

        /// <summary>
        /// Rotates one part of the cube
        /// </summary>
        /// <param name="axis">the axis that we rotate around</param>
        /// <param name="counterclockwise">set true if counterclockwise</param>
        /// <param name="val">Either 1 or -1 by convention, we will not move the middle layers.</param>
        public void Rotate(Axis axis, bool counterclockwise, int val)
        {
            if (val == 0)
            {
                //Middle layer will not be moved in order to keep our fixed axis.
                return;
            }

            switch (axis)
            {
                case Axis.xAxis:
                    foreach(Piece piece in pieces.Where(p=>p.X == val)){
                        piece.Rotate(axis, counterclockwise);
                    }
                    break;
                case Axis.yAxis:
                    foreach (Piece piece in pieces.Where(p => p.Y == val))
                    {
                        piece.Rotate(axis, counterclockwise);
                    }
                    break;
                case Axis.zAxis:
                    IEnumerable<Piece> test = pieces.Where(p => p.Z == val);
                    foreach (Piece piece in pieces.Where(p => p.Z == val))
                    {
                        piece.Rotate(axis, counterclockwise);
                    }
                    break;
            }
            OnChanged(EventArgs.Empty);
            OnRotated(this,EventArgs.Empty, new Rotation(axis,counterclockwise,val));
        }

        /// <summary>
        /// Rotates one part of the cube
        /// </summary>
        /// <param name="axis">the axis that we rotate around</param>
        /// <param name="counterclockwise">set true if counterclockwise</param>
        /// <param name="val">Either 1 or -1 by convention, we will not move the middle layers.</param>
        public void Rotate(Rotation rotation)
        {
            if (rotation.Value == 0)
            {
                //Middle layer will not be moved in order to keep our fixed axis.
                return;
            }

            switch (rotation.Axis)
            {
                case Axis.xAxis:
                    foreach (Piece piece in pieces.Where(p => p.X == rotation.Value))
                    {
                        piece.Rotate(rotation.Axis, rotation.Counterclockwise);
                    }
                    break;
                case Axis.yAxis:
                    foreach (Piece piece in pieces.Where(p => p.Y == rotation.Value))
                    {
                        piece.Rotate(rotation.Axis, rotation.Counterclockwise);
                    }
                    break;
                case Axis.zAxis:
                    foreach (Piece piece in pieces.Where(p => p.Z == rotation.Value))
                    {
                        piece.Rotate(rotation.Axis, rotation.Counterclockwise);
                    }
                    break;
            }
            OnChanged(EventArgs.Empty);
            OnRotated(this, EventArgs.Empty,rotation);
        }

        /// <summary>
        /// Clones the cube. (Implements IClonable Interface)
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            Cubev2 cube = new Cubev2();
            cube.pieces = new List<Piece>();
            foreach (Piece oldpiece in this.pieces)
            {
                if (oldpiece is Middle)
                {
                    cube.pieces.Add(new Middle(oldpiece.X, oldpiece.Y, oldpiece.Z, oldpiece.A, oldpiece.B, oldpiece.C));          
                }else if (oldpiece is Edge)
                {
                    cube.pieces.Add(new Edge(oldpiece.X, oldpiece.Y, oldpiece.Z, oldpiece.A, oldpiece.B, oldpiece.C));
                }
                else if (oldpiece is Corner)
                {
                    cube.pieces.Add(new Corner(oldpiece.X, oldpiece.Y, oldpiece.Z, oldpiece.A, oldpiece.B, oldpiece.C));
                }
            }            
            return cube;
        }
    }
}
