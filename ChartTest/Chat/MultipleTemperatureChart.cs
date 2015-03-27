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
    public class MultipleTemperatureChart : AbstractDemoChart
    {

        public override string Name
        {
            get { return "Temperature and sunshine"; }
        }

        public override string Desc
        {
            get { return "The average temperature and hous of sunshine in Crete (line chart with multiple y scales and axis)"; }
        }

        public override Android.Content.Intent Execute(Android.Content.Context context)
        {
            String[] titles = new String[] { "Air temperature" };
            IList<double[]> x = new List<double[]>();
            for (int i = 0; i < titles.Length; i++)
            {
                x.Add(new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 });
            }
            IList<double[]> values = new List<double[]>();
            values.Add(new double[] { 12.3, 12.5, 13.8, 16.8, 20.4, 24.4, 26.4, 26.1, 23.6, 20.3, 17.2, 13.9 });
            int[] colors = new int[] { Color.Blue, Color.Yellow };
            PointStyle[] styles = new PointStyle[] { PointStyle.Point, PointStyle.Point };
            XYMultipleSeriesRenderer renderer = new XYMultipleSeriesRenderer(2);
            SetRenderer(renderer, colors, styles);
            int length = renderer.SeriesRendererCount;
            for (int i = 0; i < length; i++)
            {
                XYSeriesRenderer r = (XYSeriesRenderer)renderer.GetSeriesRendererAt(i);
                r.LineWidth = 3f;
            }
            SetChartSettings(renderer, "Average temperaturs", "Month", "Temperature", 0.5, 12.5, 0, 32, Color.LightGray, Color.LightGray);
            renderer.XLabels = 12;
            renderer.YLabels = 10;
            renderer.SetShowGrid(true);
            renderer.XLabelsAlign = Android.Graphics.Paint.Align.Right;
            renderer.SetYLabelsAlign(Android.Graphics.Paint.Align.Right);
            renderer.ZoomButtonsVisible = true;
            renderer.SetPanLimits(new double[] { -10, 20, -10, 40 });
            renderer.SetZoomLimits(new double[] { -10, 20, -10, 40 });
            renderer.ZoomRate = 1f;
            renderer.LabelsColor = Color.White;
            renderer.XLabelsColor = Color.Green;
            renderer.SetYLabelsColor(0, colors[0]);
            renderer.SetYLabelsColor(0, colors[1]);

            renderer.SetYTitle("Hours", 1);
            renderer.SetYAxisAlign(Android.Graphics.Paint.Align.Right, 1);
            renderer.SetYLabelsAlign(Android.Graphics.Paint.Align.Left, 1);

            XYMultipleSeriesDataset dataset = BuildDataset(titles, x, values);
            values.Clear();
            values.Add(new double[] { 4.3, 4.9, 5.9, 8.8, 10.8, 11.9, 13.6, 12.8, 11.4, 9.5, 7.5, 5.5 });
            AddXYSeries(dataset, new string[] { "Sunshine hours" }, x, values, 1);

            Intent intent = ChartFactory.GetCubicLineChartIntent(context, dataset, renderer, 0.3f, "Average temperature");
            return intent;
        }
    }
}
