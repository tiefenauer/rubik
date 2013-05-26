using Rubik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubikController
{
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
            phases.Add(new PhaseOne(cube));
            phases.Add(new PhaseTwo(cube));
            phases.Add(new PhaseThree(cube));
            phases.Add(new Phase5());
            phases.Add(new Phase6());
            phases.Add(new Phase7());
        }

        public void Solve(Cubev2 cube)
        {
            rotations = new List<Rotation>();
            Cubev2 clonedCube = (Cubev2)cube.Clone();
            foreach (IPhaseSolvable phase in phases)
            {
                List<Rotation> newRotations = phase.Solve(clonedCube);
                rotations.Concat(newRotations);
            }
        }

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
