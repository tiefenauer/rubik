using RubikModel;
using RubikController;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RubikTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestRandomCubes()
        {
            Cubev2 solvedCube = DeSerializeConfigFromDirectory("solvedcube.xml");
            Random rand = new Random();

            int cubeCounter = 0;
            while (cubeCounter < 100)
            {
                Cubev2 cube = (Cubev2)solvedCube.Clone();
                int numRotations = rand.Next(15, 50);
                int i = 0;
                while (i < numRotations){
                    int axisIndex = rand.Next(0, 2);
                    int clockwiseIndex = rand.Next(0, 1);
                    int valInt = rand.Next(0, 1);

                    Axis axis = Axis.xAxis; ;
                    switch (axisIndex){
                        case 0:
                            axis = Axis.xAxis;
                            break;
                        case 1:
                            axis = Axis.yAxis;
                            break;
                        case 2:
                            axis = Axis.zAxis;
                            break;
                    }
                    Boolean clockwise = (clockwiseIndex == 0)?false:true;
                    int val = (valInt == 0) ? -1 : 1;
                    cube.Rotate(axis, clockwise, val);
                    i++;
                }
                RubikSolvController controller = new RubikSolvController(cube);
                controller.Solve(cube);
                cubeCounter++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SerializeConfigToCurrentDirectory()
        {
            XmlSerializer writer = new XmlSerializer(typeof(Cubev2), new Type[] { typeof(List<Piece>) });
            using (StreamWriter file = new StreamWriter(@"defaultcube.xml"))
            {
                //writer.Serialize(file, this.cube);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        private Cubev2 DeSerializeConfigFromDirectory(string filename)
        {
            XmlSerializer reader = new XmlSerializer(typeof(Cubev2), new Type[] { typeof(List<Piece>), typeof(Edge), typeof(Middle), typeof(Corner) });
            using (XmlReader file = new XmlTextReader(@filename))
            {
                try
                {
                    Cubev2 cube = (Cubev2)reader.Deserialize(file);
                    return cube;
                }
                catch (Exception ex)
                {
                }
            }
            return null;
        }
    }
}
