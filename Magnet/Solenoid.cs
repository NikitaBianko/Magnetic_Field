using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagneticFieldSimulator.Core
{
    public class Solenoid
    {
        public readonly SolenoidParams @params;
        private readonly Point position;
        private List<Сonductor> Сonductors = new List<Сonductor>();

        public Solenoid(SolenoidParams @params, Point position)
        {
            this.@params = @params;
            this.position = position;
            for (int i = 0; i < @params.NumberOfWindingTurns; i++)
            {
                Сonductors.Add(new Сonductor(new Point(position.X + @params.Width / 2, position.Y - @params.Height / 2 + @params.Height / @params.NumberOfWindingTurns), false));
                Сonductors.Add(new Сonductor(new Point(position.X + @params.Width / 2, position.Y - @params.Height / 2 + @params.Height / @params.NumberOfWindingTurns), true));
            }
        }

        public Vector MagneticInductionVector(Point p)
        {
            var B = new Vector(0, 0);

            foreach (Сonductor conductor in Сonductors) {
                var distance = p.Distance(conductor.Position);
                if(distance > 0)
                {
                    double Bscalar = 4 * Math.PI * 10e-6 * @params.NumberOfWindingTurns * @params.I / distance;
                    double k = Bscalar / distance;
                    B = new Vector(k * (p.Y - conductor.Position.Y), k * (p.X - conductor.Position.X));
                    if (conductor.IsReverse)
                    {
                        B *= -1;
                    }
                }
            }

            return B;
        }
    }
}