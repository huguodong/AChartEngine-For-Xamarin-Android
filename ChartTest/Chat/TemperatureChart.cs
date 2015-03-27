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
    public class TemperatureChart : AbstractDemoChart
    {
        public override string Name
        {
            get { return "Temperature range chart"; }
        }

        public override string Desc
        {
            get { return "The monthly temperature (vertical range chart)"; }
        }

        public override Android.Content.Intent Execute(Android.Content.Context context)
        {
            double[] minValues = new double[] { -24, -19, -10, -1, 7, 12, 15, 14, 9, 1, -11, -16 };
            double[] maxValues = new double[] { 7, 12, 24, 28, 33, 35, 37, 36, 28, 19, 11, 4 };

            XYMultipleSeriesDataset dataset = new XYMultipleSeriesDataset();
            RangeCategorySeries series = new RangeCategorySeries("Temperature");
            int length = minValues.Length;
            for (int k = 0; k < length; k++)
            {
                series.Add(minValues[k], maxValues[k]);
            }
            dataset.AddSeries(series.ToXYSeries());
            int[] colors = new int[] { Color.Cyan };
            XYMultipleSeriesRenderer renderer = BuildBarRenderer(colors);
            SetChartSettings(renderer, "Monthly temperature range", "Month", "Celsius degrees", 0.5, 12.5, -30, 45, Color.Gray, Color.LightGray);
            renderer.BarSpacing = 0.5;
            renderer.XLabels = 0;
            renderer.YLabels = 10;
            renderer.AddXTextLabel(1, "Jan");
            renderer.AddXTextLabel(3, "Mar");
            renderer.AddXTextLabel(5, "May");
            renderer.AddXTextLabel(7, "Jul");
            renderer.AddXTextLabel(10, "Oct");
            renderer.AddXTextLabel(12, "Dec");
            renderer.AddYTextLabel(-25, "Very cold");
            renderer.AddYTextLabel(-10, "Cold");
            renderer.AddYTextLabel(5, "OK");
            renderer.AddYTextLabel(20, "Noci");
            renderer.SetMargins(new int[] { 30, 70, 10, 0 });
            renderer.SetYLabelsAlign(Android.Graphics.Paint.Align.Right);
            SimpleSeriesRenderer r = renderer.GetSeriesRendererAt(0);
            r.DisplayChartValues = true;
            r.ChartValuesTextSize = 12;
            r.ChartValuesSpacing = 3;
            r.GradientEnabled = true;
            r.SetGradientStart(-20, Color.Blue);
            r.SetGradientStop(20, Color.Green);
            return ChartFactory.GetRangeBarChartIntent(context, dataset, renderer, BarChart.Type.Default, "Temperatur range");
        }
    }
}
