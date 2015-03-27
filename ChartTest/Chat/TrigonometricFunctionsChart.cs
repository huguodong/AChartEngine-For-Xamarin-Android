using Android.Graphics;
using Org.Achartengine;
using Org.Achartengine.Chart;
using Org.Achartengine.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChartTest.Chat
{
    public class TrigonometricFunctionsChart : AbstractDemoChart
    {

        public override string Name
        {
            get { return "Trigonometric functions"; }
        }

        public override string Desc
        {
            get { return "The graphical repressentations of the sin and cos functions (line chart)"; }
        }

        public override Android.Content.Intent Execute(Android.Content.Context context)
        {
            String[] titles = new String[] { "sin", "cos" };
            IList<double[]> x = new List<double[]>();
            IList<double[]> values = new List<double[]>();
            int step = 4;
            int count = 360 / step + 1;
            x.Add(new double[count]);
            x.Add(new double[count]);
            double[] sinValues = new double[count];
            double[] cosValues = new double[count];
            values.Add(sinValues);
            values.Add(cosValues);
            for (int i = 0; i < count; i++)
            {
                int angle = i * step;
                x[0][i] = angle;
                x[1][i] = angle;
                double rAngle = Math.Tan(angle);
                sinValues[i] = Math.Sin(rAngle);
                cosValues[i] = Math.Cos(rAngle);
            }
            int[] colors = new int[] { Color.Blue, Color.Cyan };
            PointStyle[] styles = new PointStyle[] { PointStyle.Point, PointStyle.Point };
            XYMultipleSeriesRenderer renderer = BuildRenderer(colors, styles);
            SetChartSettings(renderer, "Trigonometric functions", "X (in degrees)", "Y", 0, 360, -1, 1, Color.Gray, Color.LightGray);
            renderer.XLabels = 20;
            renderer.YLabels = 10;
            return ChartFactory.GetLineChartIntent(context, BuildDataset(titles, x, values), renderer);
        }
    }
}
