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
    public class ScatterChart : AbstractDemoChart
    {

        public override string Name
        {
            get { return "Scatter chart"; }
        }

        public override string Desc
        {
            get { return "Randomly generated values for the scatter chart"; }
        }

        public override Android.Content.Intent Execute(Android.Content.Context context)
        {
            String[] titles = new String[] { "Series 1", "Series 2", "Series 3", "Series 4", "Series 5" };
            IList<double[]> x = new List<double[]>();
            IList<double[]> values = new List<double[]>();
            int count = 20;
            int length = titles.Length;
            Random r = new Random();
            for (int i = 0; i < length; i++)
            {
                double[] xValues = new double[count];
                double[] yValues = new double[count];
                for (int k = 0; k < count; k++)
                {
                    xValues[k] = k + r.Next() % 10;
                    yValues[k] = k * 2 + r.Next() % 10;
                }
                x.Add(xValues);
                values.Add(yValues);
            }
            int[] colors = new int[] { Color.Blue, Color.Cyan, Color.Magenta, Color.LightGray, Color.Green };
            PointStyle[] styles = new PointStyle[] { PointStyle.X, PointStyle.Diamond, PointStyle.Triangle, PointStyle.Square, PointStyle.Circle };
            XYMultipleSeriesRenderer renderer = BuildRenderer(colors, styles);
            SetChartSettings(renderer, "Scatter chart", "X", "Y", -10, 30, -10, 51, Color.Gray, Color.LightGray);
            renderer.XLabels = 10;
            renderer.YLabels = 10;
            length = renderer.SeriesRendererCount;
            for (int i = 0; i < length; i++)
            {
                ((XYSeriesRenderer)renderer.GetSeriesRendererAt(i)).FillPoints = true;
            }
            return ChartFactory.GetScatterChartIntent(context, BuildDataset(titles, x, values), renderer);
        }
    }
}
