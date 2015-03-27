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
    public class SalesComparisonChart : AbstractDemoChart
    {

        public override string Name
        {
            get { return "Sales comparison"; }
        }

        public override string Desc
        {
            get { return "Monthly sales advance for 2 years (interpolated line and area charts)"; }
        }

        public override Android.Content.Intent Execute(Android.Content.Context context)
        {
            String[] titles = new String[] { "Sales for 2008", "Sales for 2007", "Difference between 2008 and 2007 sales" };
            IList<double[]> values = new List<double[]>();
            values.Add(new double[] { 14230, 12300, 14240, 15244, 14900, 12200, 11030, 12000, 12500, 15500, 14600, 15000 });
            values.Add(new double[] { 10230, 10900, 11240, 12540, 13500, 14200, 12530, 11200, 10500, 12500, 11600, 13500 });
            int length = values[0].Length;
            double[] diff = new double[length];
            for (int i = 0; i < length; i++)
            {
                diff[i] = values[0][i] - values[1][i];
            }
            values.Add(diff);
            int[] colors = new int[] { Color.Blue, Color.Cyan, Color.Green };
            PointStyle[] styles = new PointStyle[] { PointStyle.Point, PointStyle.Point, PointStyle.Point };
            XYMultipleSeriesRenderer renderer = BuildRenderer(colors, styles);
            SetChartSettings(renderer, "Monthly sales int the last 2 years", "Month", "Units sold", 0.75, 12.25, -5000, 19000, Color.Gray, Color.LightGray);
            renderer.XLabels = 12;
            renderer.YLabels = 10;
            renderer.ChartTitleTextSize = 20;
            renderer.SetTextTypeface("sans_serif", (int)TypefaceStyle.Bold);
            renderer.LabelsTextSize = 14f;
            renderer.AxisTitleTextSize = 15;
            renderer.LegendTextSize = 15;
            length = renderer.SeriesRendererCount;

            for (int i = 0; i < length; i++)
            {
                XYSeriesRenderer seriesRenderer = (XYSeriesRenderer)renderer.GetSeriesRendererAt(i);
                if (i == length - 1)
                {
                    Org.Achartengine.Renderer.XYSeriesRenderer.FillOutsideLine fill = new XYSeriesRenderer.FillOutsideLine(Org.Achartengine.Renderer.XYSeriesRenderer.FillOutsideLine.Type.BoundsAll);
                    fill.Color = Color.Green;
                    seriesRenderer.AddFillOutsideLine(fill);
                }
                seriesRenderer.LineWidth = 2.5f;
                seriesRenderer.DisplayChartValues = true;
                seriesRenderer.ChartValuesTextSize = 10f;
            }
            return ChartFactory.GetCubicLineChartIntent(context, BuildBarDataset(titles, values), renderer, 0.5f);
        }
    }
}
