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
    public class SalesBarChart : AbstractDemoChart
    {

        public override string Name
        {
            get { return "Sales horizontal bar chart"; }
        }

        public override string Desc
        {
            get { return "The monthly sales for the last 2 years ()"; }
        }

        public override Android.Content.Intent Execute(Android.Content.Context context)
        {
            String[] titles = new String[] { "2007", "2008" };
            IList<double[]> values = new List<double[]>();
            values.Add(new double[] { 5230, 7300, 9240, 10540, 7900, 9200, 12030, 11200, 9500, 10500, 11600, 13500 });
            values.Add(new double[] { 14230, 12300, 14240, 15244, 15900, 19200, 22030, 21200, 19500, 15500, 12600, 14000 });
            int[] colors = new int[] { Color.Cyan, Color.Blue };
            XYMultipleSeriesRenderer renderer = BuildBarRenderer(colors);
            renderer.SetOrientation(XYMultipleSeriesRenderer.Orientation.Vertical);
            SetChartSettings(renderer, "Monthly sales in the last 2 years", "Month", "Units sold", 0.5, 12.5, 0, 24000, Color.Cyan, Color.LightGray);
            renderer.XLabels = 1;
            renderer.YLabels = 10;
            renderer.AddXTextLabel(1, "Jan");
            renderer.AddXTextLabel(3, "Mar");
            renderer.AddXTextLabel(5, "May");
            renderer.AddXTextLabel(7, "Jul");
            renderer.AddXTextLabel(10, "Oct");
            renderer.AddXTextLabel(12, "Dec");
            int length = renderer.SeriesRendererCount;
            for (int i = 0; i < length; i++)
            {
                SimpleSeriesRenderer seriesRenderer = renderer.GetSeriesRendererAt(i);
                seriesRenderer.DisplayChartValues = true;
            }
            return ChartFactory.GetBarChartIntent(context, BuildBarDataset(titles, values), renderer, BarChart.Type.Default);
        }
    }
}
