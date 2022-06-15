using MagneticFieldSimulator.Core;

var field = new MagneticField(10, 10);

field.AddSolenoid(new SolenoidParams(4, 2, 1, 4), new Point(5, 5));

field.Calculation();

Console.WriteLine("ok!");