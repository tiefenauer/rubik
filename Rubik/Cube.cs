using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik
{
    public class Cube
    {

        public Cube()
        {
            Bottom = new Face("b",FacePosition.Bottom);
            Top = new Face("w",FacePosition.Top);
            Front = new Face("g",FacePosition.Front);
            Left = new Face("r", FacePosition.Left);
            Right = new Face("o", FacePosition.Right);
            Back = new Face("y", FacePosition.Back);
        }

        Face back;
        Face left;
        Face top;
        Face right;
        Face front;
        Face bottom;

        public Face Bottom
        {
            get { return bottom; }
            set { bottom = value; }
        }

        public Face Front
        {
            get { return front; }
            set { front = value; }
        }

        public Face Right
        {
            get { return right; }
            set { right = value; }
        }

        public Face Top
        {
            get { return top; }
            set { top = value; }
        }

        public Face Left
        {
            get { return left; }
            set { left = value; }
        }

        public Face Back
        {
            get { return back; }
            set { back = value; }
        }

        public void rotate(Face cubeface, bool counterclockwise)
        {
            
        }
    }
}
