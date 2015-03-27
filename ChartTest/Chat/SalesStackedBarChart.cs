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
    public class SalesStackedBarChart : AbstractDemoChart
    {

        public override string Name
        {
            get { return "Sales stacked bar chart"; }
        }

        public override string Desc
        {
            get { return "The monthly sales for the last 2 years (stacked bar chart)"; }
        }

        public override Android.Content.Intent Execute(Android.Content.Context context)
        {
            String[] titles = new String[] { "2008", "2007" };
            IList<double[]> values = new List<double[]>();
            values.Add(new double[] { 14230, 12300, 14240, 15244, 15900, 19200, 22030, 21200, 19500, 15500, 12600, 14000 });
            values.Add(new double[] { 5230, 7300, 9240, 10540, 7900, 9200, 12030, 11200, 9500, 10500, 11600, 13500 });
            int[] colors = new int[] { Color.Blue, Color.Cyan };
            XYMultipleSeriesRenderer renderer = BuildBarRenderer(colors);
            SetChartSettings(renderer, "Monthly sales in the last 2 years", "Month", "Units sold", 0.5, 12.5, 0, 24000, Color.Gray, Color.LightGray);
            renderer.GetSeriesRendererAt(0).DisplayChartValues = true;
            renderer.GetSeriesRendererAt(1).DisplayChartValues = true;
            renderer.XLabels = 12;
            renderer.YLabels = 10;
            renderer.XLabelsAlign = Android.Graphics.Paint.Align.Left;
            renderer.SetYLabelsAlign(Android.Graphics.Paint.Align.Left);
            renderer.ZoomRate = 1.1f;
            renderer.BarSpacing = 0.5f;
            return ChartFactory.GetBarChartIntent(context, BuildBarDataset(titles, values), renderer, BarChart.Type.Stacked);
        }
    }
}
