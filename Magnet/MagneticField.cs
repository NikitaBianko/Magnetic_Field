using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MagneticFieldSimulator.Core
{
    public class MagneticField
    {
        public uint Height { get; }
        public uint Width { get; }
        public List<Solenoid> Solenoids { get; private set; } = new List<Solenoid>();
        public Vector[,] Field { get; private set; }
        public bool isCalculated { get; private set; } = false;

        public MagneticField(uint height, uint width)
        {
            Height = height;
            Width = width;
            Field = new Vector[height, width];
        }

        public void AddSolenoid(SolenoidParams @params, Point coordinates, double rotation = 0)
        {
            Solenoids.Add(new Solenoid(@params, coordinates, rotation));
            isCalculated = false;
        }
        public IEnumerable<int> SteppedIntegerList(int startIndex,
            int endEndex, int stepSize)
        {
            for (int i = startIndex; i < endEndex; i += stepSize)
            {
                yield return i;
            }
        }
        public async Task CalculationAsync(int StepSize = 1)
        {
            await Task.Run(() => Calculation(StepSize));
        }

        public void Calculation(int StepSize = 1)
        {
            Parallel.For(0, Width, x =>
            {
                Parallel.For(0, Height, y =>
                {
                    Field[x, y] = new Vector(0, 0);
                });
            });

            Parallel.ForEach(SteppedIntegerList(StepSize, (int)Width, StepSize), x =>
            {
                Parallel.ForEach(SteppedIntegerList(StepSize, (int)Height, StepSize), y =>
                {
                    Parallel.ForEach(Solenoids, solenoid =>
                    {
                        Field[x, y] += solenoid.MagneticInductionVector(new Point((int)x, (int)y));
                    });
                });
            });
            isCalculated = true;
        }
    }
}
