using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagneticFieldSimulator.Core
{
    public class MagneticField
    {
        public uint Height { get; }
        public uint Width { get; }
        public List<Solenoid> Solenoids { get; private set; } = new List<Solenoid>();
        public Vector[,] Field { get; private set; }

        public MagneticField(uint height, uint width)
        {
            Height = height;
            Width = width;
            Field = new Vector[height, width];
        }

        public void AddMagnet(SolenoidParams @params, Point coordinates)
        {
            Solenoids.Add(new Solenoid(@params, coordinates));
        }

        public void Calculation()
        {
            for (uint x = 0; x < Width; x++)
            {
                for (uint y = 0; y < Height; y++)
                {
                    Field[x, y] = new Vector(0, 0);
                    foreach (Solenoid solenoid in Solenoids)
                    {
                        Field[x, y] += solenoid.MagneticInductionVector(new Point((int)x, (int)y));
                    }
                }
            }
        }
    }
}
