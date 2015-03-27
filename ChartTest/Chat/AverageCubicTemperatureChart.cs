using Android.Content;
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
    public class AverageCubicTemperatureChart : AbstractDemoChart
    {
        public override string Name
        {
            get { return "Average temperature"; }
        }

        public override string Desc
        {
            get { return "The average temperature in 4 Greek islands (cubic line chart)"; }
        }

        public override Android.Content.Intent Execute(Android.Content.Context context)
        {
            String[] titles = new String[] { "Crete", "Corfu", "Thassos", "Skiathos" };
            IList<double[]> x = new List<double[]>();
            for (int i = 0; i < titles.Length; i++)
            {
                x.Add(new[] { 1d, 2d, 3d, 4d, 5d, 6d, 7d, 8d, 9d, 10d, 11d, 12d });
            }
            IList<double[]> values = new List<double[]>();
            values.Add(new double[] { 12.3, 12.5, 13.8, 16.8, 20.4, 24.4, 26.4, 26.1, 23.6, 20.3, 17.2, 13.9 });
            values.Add(new double[] { 10, 10, 12, 15, 20, 24, 26, 26, 23, 18, 14, 11 });
            values.Add(new double[] { 5, 5.3, 8, 12, 17, 23, 24.2, 24, 19, 15, 9, 6 });
            values.Add(new double[] { 9, 10, 11, 15, 19, 23, 26, 25, 22, 18, 13, 10 });
            int[] colors = new int[] { Color.Blue, Color.Green, Color.Cyan, Color.Yellow };
            PointStyle[] styles = new PointStyle[] { PointStyle.Circle, PointStyle.Diamond, PointStyle.Triangle, PointStyle.Square };
            XYMultipleSeriesRenderer renderer = BuildRenderer(colors, styles);
            int length = renderer.SeriesRendererCount;
            for (int i = 0; i < length; i++)
            {
                ((XYSeriesRenderer)renderer.GetSeriesRendererAt(i)).FillPoints = true;
            }
            SetChartSettings(renderer, "Average temperature", "Month", "Temperature", 0.5, 12.5, 0, 32, Color.Gray, Color.Gray);
            renderer.XLabels = 12;
            renderer.YLabels = 10;
            renderer.SetShowGrid(true);
            renderer.XLabelsAlign = Android.Graphics.Paint.Align.Right;
            renderer.SetYLabelsAlign(Android.Graphics.Paint.Align.Right);
            renderer.ZoomButtonsVisible = true;
            renderer.SetPanLimits(new double[] { -10, 20, -10, 40 });
            renderer.SetZoomLimits(new double[] { -10, 20, -10, 40 });
            Intent intent = ChartFactory.GetCubicLineChartIntent(context, BuildDataset(titles, x, values), renderer, 0.33f, "Average temperature");
            return intent;
        }
    }
}
