using Rubik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubikController
{
    /// <summary>
    /// Solver Controller for Rubiks cube.
    /// </summary>
    public class RubikSolvController
    {
        private Cubev2 cube;
        Cubev2 clonedCube;
        private List<IPhaseSolvable> phases = new List<IPhaseSolvable>();
        List<Rotation> rotations = new List<Rotation>();
        int counter = -1;

        public RubikSolvController(Cubev2 cube)
        {
            this.cube = cube;
            phases.Add(new Phase1(cube));
            phases.Add(new Phase2(cube));
            phases.Add(new Phase3(cube));
            phases.Add(new Phase4());
            phases.Add(new Phase5());
            phases.Add(new Phase6());
        }

        /// <summary>
        /// Solves each of the phases and concatenates the results into one Result List
        /// </summary>
        /// <param name="cube"></param>
        public void Solve(Cubev2 cube)
        {
            rotations = new List<Rotation>();
            Cubev2 clonedCube = (Cubev2)cube.Clone();
            foreach (IPhaseSolvable phase in phases)
            {
                List<Rotation> newRotations = phase.Solve(clonedCube);
                rotations.AddRange(newRotations);                
            }
        }

        /// <summary>
        /// Gets the next Rotation in the List
        /// </summary>
        /// <returns></returns>
        public Rotation Step()
        {
            if (counter < rotations.Count)
            {
                counter++;
                Rotation rotation = rotations[counter];                
                return rotation;
            }
            return null;
        }

        /// <summary>
        /// Gets the previous rotation in the list.
        /// </summary>
        /// <returns></returns>
        public Rotation PrevStep()
        {
            if (counter < rotations.Count)
            {
                counter--;
                if (counter >= 0)
                {
                    Rotation rotation = rotations[counter];
                    return rotation;
                }
                else
                {
                    counter++;
                }
            }
            return null;
        }
    }
}
