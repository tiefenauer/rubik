using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik
{
    /// <summary>
    /// This interface determines wheter the Implementing class definies a Solve Method for a cube.
    /// </summary>
    public interface IPhaseSolvable
    {
        List<Rotation> Solve(Cubev2 cube);        
    }
}
