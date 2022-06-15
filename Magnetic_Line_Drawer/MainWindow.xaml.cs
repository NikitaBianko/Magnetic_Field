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
        public MainWindow()
        {
            InitializeComponent();

            var field = new MagneticField(1000, 1000);

            field.AddMagnet(new SolenoidParams(150, 50, 50, 10), new MagneticFieldSimulator.Core.Point(250, 300));
            field.AddMagnet(new SolenoidParams(150, 50, 10, 25), new MagneticFieldSimulator.Core.Point(500, 500));
            field.AddMagnet(new SolenoidParams(150, 50, 100, 10), new MagneticFieldSimulator.Core.Point(750, 700));

            field.Calculation();

            for (int x = 0; x < 1000; x+=8)
            {
                for (int y = 0; y < 1000; y+=8)
                {
                    MagneticFieldSimulator.Core.Vector v = field.Field[x, y];
                    v *= 1.0 / v.Size();

                    var myLine = new Line();
                    myLine.Stroke = System.Windows.Media.Brushes.Black;
                    myLine.Y1 = x;
                    myLine.Y2 = x + v.X * 7;
                    myLine.X1 = y;
                    myLine.X2 = y - v.Y * 7;
                    myLine.StrokeThickness = 1.5;
                    canvas.Children.Add(myLine);
                }
            }

            
        }
    }
}
