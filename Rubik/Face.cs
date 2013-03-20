using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik
{
    public enum FacePosition
    {
        Bottom,
        Top,
        Front,
        Left,
        Right,
        Back
    }
    public class Face
    {
        Row top;
        Row center;
        Row bottom;
        FacePosition pos;

        public FacePosition Pos
        {
            get { return pos; }
            set { pos = value; }
        }


        public Face(string name, FacePosition pos){
            Pos = pos;
            Top = new Row(name);
            Center = new Row(name);
            Bottom = new Row(name);
        }

        public Row Top
        {
            get { return top; }
            set { top = value; }
        }        

        public Row Center
        {
            get { return center; }
            set { center = value; }
        }
        
        public Row Bottom
        {
            get { return bottom; }
            set { bottom = value; }
        }

        public void rotate(bool counterclockwise)
        {
        }

    }
}
