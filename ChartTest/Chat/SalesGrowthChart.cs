using Android.Graphics;
using Java.Util;
using Org.Achartengine;
using Org.Achartengine.Chart;
using Org.Achartengine.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChartTest.Chat
{
    public class SalesGrowthChart : AbstractDemoChart
    {

        public override string Name
        {
            get { return "Sales growth"; }
        }

        public override string Desc
        {
            get { return "The sales growth across several years (time chart)"; }
        }

        public override Android.Content.Intent Execute(Android.Content.Context context)
        {
            String[] titles = new String[] { "Sales growth January 1995 to December 2000" };
            IList<Date[]> dates = new List<Date[]>();
            IList<double[]> values = new List<double[]>();
            Date[] dateValues = new Date[] { new Date(95,0,1),new Date(95,3,1),new Date(95,6,1),
              new Date(95,9,1), new Date(96, 0, 1), new Date(96, 3, 1), new Date(96, 6, 1),
              new Date(96, 9, 1), new Date(97, 0, 1), new Date(97, 3, 1), new Date(97, 6, 1),
              new Date(97, 9, 1), new Date(98, 0, 1), new Date(98, 3, 1), new Date(98, 6, 1),
              new Date(98, 9, 1), new Date(99, 0, 1), new Date(99, 3, 1), new Date(99, 6, 1),
              new Date(99, 9, 1), new Date(100, 0, 1), new Date(100, 3, 1), new Date(100, 6, 1),
              new Date(100, 9, 1), new Date(100, 11, 1) };
            dates.Add(dateValues);

            values.Add(new double[] { 4.9, 5.3, 3.2, 4.5, 6.5, 4.7, 5.8, 4.3, 4, 2.3, -0.5, -2.9, 3.2, 5.5,
                4.6, 9.4, 4.3, 1.2, 0, 0.4, 4.5, 3.4, 4.5, 4.3, 4});
            int[] colors = new int[] { Color.Blue };
            PointStyle[] styles = new PointStyle[] { PointStyle.Point };
            XYMultipleSeriesRenderer renderer = BuildRenderer(colors, styles);
            SetChartSettings(renderer, "Sales growth", "Date", "%", dateValues[0].Time, dateValues[dateValues.Length - 1].Time, -4, 11, Color.Gray, Color.LightGray);
            renderer.YLabels = 10;
            renderer.XRoundedLabels = false;
            XYSeriesRenderer xyRenderer = (XYSeriesRenderer)renderer.GetSeriesRendererAt(0);
            Org.Achartengine.Renderer.XYSeriesRenderer.FillOutsideLine fill = new XYSeriesRenderer.FillOutsideLine(Org.Achartengine.Renderer.XYSeriesRenderer.FillOutsideLine.Type.BoundsAbove);
            fill.Color = Color.Green;
            xyRenderer.AddFillOutsideLine(fill);
            fill = new XYSeriesRenderer.FillOutsideLine(XYSeriesRenderer.FillOutsideLine.Type.BoundsBelow);
            fill.Color = Color.Magenta;
            xyRenderer.AddFillOutsideLine(fill);
            fill = new XYSeriesRenderer.FillOutsideLine(XYSeriesRenderer.FillOutsideLine.Type.BoundsAbove);
            fill.Color = Color.Argb(255, 0, 200, 100);
            fill.SetFillRange(new int[] { 10, 19 });
            xyRenderer.AddFillOutsideLine(fill);

            return ChartFactory.GetTimeChartIntent(context, BuildDateDataset(titles, dates, values), renderer, "MMM yyyy");
        }
    }
}
