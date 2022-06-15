using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagneticFieldSimulator.Core
{
    public class SolenoidParams
    {
        public double Height { get; private set; }
        public double Width { get; private set; } 
        public double I { get; private set; }
        public uint NumberOfWindingTurns { get; private set; }  
        public SolenoidParams(double height, double width, double i, uint numberOfWindingTurns)
        {
            Height = height;
            Width = width;
            I = i;
            NumberOfWindingTurns = numberOfWindingTurns;
        }
    }
}
