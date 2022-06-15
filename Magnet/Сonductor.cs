using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagneticFieldSimulator.Core
{
    internal class Сonductor
    {
        public readonly Point Position;
        public readonly bool IsReverse;

        public Сonductor(Point position, bool isReverse)
        {
            Position = position;
            IsReverse = isReverse;
        }
    }
}
