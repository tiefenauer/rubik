using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik
{
    public interface IPhaseSolvable
    {
        List<Rotation> Solve(Cubev2 cube);        
    }
}
