using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MagneticFieldSimulator.Core;

namespace Magnetic_Line_Drawer
{
    public partial class MainWindow : Window
    {
        public MagneticField field;
        public int Step = 5;
        public MainWindow()
        {
            InitializeComponent();
            field = new MagneticField(1000, 1000);

            field.AddSolenoid(new SolenoidParams(150, 50, 50, 10000), new MagneticFieldSimulator.Core.Point(500, 500));
            
        }

        private async void RunSimulationBtn_Click(object sender, RoutedEventArgs e)
        {
            await field.CalculationAsync(Step);

            for (int x = Step; x < 1000; x += Step)
            {
                for (int y = Step; y < 1000; y += Step)
                {
                    MagneticFieldSimulator.Core.Vector v = field.Field[x, y];
                    if (v.Size() != 0)
                    {
                        v *= 1.0 / v.Size();
                    }

                    var myLine = new Line();
                    myLine.Stroke = System.Windows.Media.Brushes.Black;
                    myLine.Y1 = x;
                    myLine.Y2 = x + v.X * 10;
                    myLine.X1 = y;
                    myLine.X2 = y - v.Y * 10;
                    myLine.StrokeThickness = 1;
                    canvas.Children.Add(myLine);
                }
            }
        }
    }
}
