using Android.Content;
using Android.Graphics;
using Org.Achartengine;
using Org.Achartengine.Chart;
using Org.Achartengine.Model;
using Org.Achartengine.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChartTest.Chat
{
    public class AverageTemperatureChart : AbstractDemoChart
    {
        public override string Name
        {
            get { return "Average temperature"; }
        }

        public override string Desc
        {
            get { return "The average temperature in 4 Greek islands (line chart)"; }
        }

        public override Android.Content.Intent Execute(Android.Content.Context context)
        {
            String[] titles = new String[] { "Crete", "Corfu", "Thassos", "Skiathos" };
            IList<double[]> x = new List<double[]>();
            for (int i = 0; i < titles.Length; i++)
            {
                x.Add(new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 });
            }
            IList<double[]> values = new List<double[]>();
            values.Add(new double[] { 12.3, 12.5, 13.8, 16.8, 20.4, 24.4, 26.4, 26.1, 23.6, 20.3, 17.2, 13.9 });
            values.Add(new double[] { 10, 10, 12, 15, 20, 24, 26, 26, 23, 18, 14, 11 });
            values.Add(new double[] { 5, 5.3, 8, 12, 17, 22, 24.2, 24, 19, 15, 9, 6 });
            values.Add(new double[] { 9, 10, 11, 15, 19, 23, 26, 25, 22, 18, 13, 10 });
            int[] colors = new int[] { Color.Blue, Color.Green, Color.Cyan, Color.Yellow };
            PointStyle[] styles = new PointStyle[] { PointStyle.Circle, PointStyle.Diamond, PointStyle.Triangle, PointStyle.Square };
            XYMultipleSeriesRenderer renderer = BuildRenderer(colors, styles);
            int length = renderer.SeriesRendererCount;
            for (int i = 0; i < length; i++)
            {
                ((XYSeriesRenderer)renderer.GetSeriesRendererAt(i)).FillPoints = true;
            }
            SetChartSettings(renderer, "Average temperature", "Month", "Temperature", 0.5, 12.5, -10, 40, Color.Gray, Color.Gray);
            renderer.XLabels = 12;
            renderer.YLabels = 10;
            renderer.SetShowGrid(true);
            renderer.ZoomButtonsVisible = true;
            renderer.SetPanLimits(new double[] { -10, 20, -10, 40 });
            renderer.SetZoomLimits(new double[] { -10, 20, -10, 40 });

            XYMultipleSeriesDataset dataset = BuildDataset(titles, x, values);
            XYSeries series = dataset.GetSeriesAt(0);
            series.AddAnnotation("Vacation", 6, 30);
            Intent intent = ChartFactory.GetLineChartIntent(context, dataset, renderer, "Average temperature");
            return intent;
        }
    }
}
