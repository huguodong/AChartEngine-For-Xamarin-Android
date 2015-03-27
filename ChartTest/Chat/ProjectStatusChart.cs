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
    public class ProjectStatusChart : AbstractDemoChart
    {

        public override string Name
        {
            get { return "Project tickets status"; }
        }

        public override string Desc
        {
            get { return "The opened tickets and the fixed tickets (time chart)"; }
        }

        public override Android.Content.Intent Execute(Android.Content.Context context)
        {
            String[] titles = new String[] { "New tickets", "Fixed tickets" };
            IList<Date[]> dates = new List<Date[]>();
            IList<double[]> values = new List<double[]>();
            int length = titles.Length;
            for (int i = 0; i < length; i++)
            {
                dates.Add(new Date[12]);
                dates[i][0] = new Date(108, 9, 1);
                dates[i][1] = new Date(108, 9, 8);
                dates[i][2] = new Date(108, 9, 15);
                dates[i][3] = new Date(108, 9, 22);
                dates[i][4] = new Date(108, 9, 29);
                dates[i][5] = new Date(108, 10, 5);
                dates[i][6] = new Date(108, 10, 12);
                dates[i][7] = new Date(108, 10, 19);
                dates[i][8] = new Date(108, 10, 26);
                dates[i][9] = new Date(108, 11, 3);
                dates[i][10] = new Date(108, 11, 10);
                dates[i][11] = new Date(108, 11, 17);
            }
            values.Add(new double[] { 142, 123, 1422, 152, 149, 122, 110, 120, 125, 155, 146, 150 });
            values.Add(new double[] { 102, 90, 112, 105, 125, 112, 125, 112, 105, 115, 116, 135 });
            length = values[0].Length;
            int[] colors = new int[] { Color.Blue, Color.Green };
            PointStyle[] styles = new PointStyle[] { PointStyle.Point, PointStyle.Point };
            XYMultipleSeriesRenderer renderer = BuildRenderer(colors, styles);
            SetChartSettings(renderer, "Project work status", "Date", "Tickets", dates[0][0].Time, dates[0][11].Time, 50, 190, Color.Gray, Color.LightGray);
            renderer.XLabels = 0;
            renderer.YLabels = 10;
            renderer.AddYTextLabel(100, "test");
            length = renderer.SeriesRendererCount;
            for (int i = 0; i < length; i++)
            {
                SimpleSeriesRenderer seriesRenderer = renderer.GetSeriesRendererAt(i);
                seriesRenderer.DisplayChartValues = true;
            }
            renderer.XRoundedLabels = false;
            return ChartFactory.GetTimeChartIntent(context, BuildDateDataset(titles, dates, values), renderer, "MM/dd/yyyy");
        }
    }
}
