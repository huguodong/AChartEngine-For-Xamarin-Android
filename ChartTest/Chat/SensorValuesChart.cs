using Android.Graphics;
using Java.Util;
using Org.Achartengine;
using Org.Achartengine.Chart;
using Org.Achartengine.Renderer;
using Org.Achartengine.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChartTest.Chat
{
    public class SensorValuesChart : AbstractDemoChart
    {
        private static readonly long HOUR = 3600 * 1000;
        private static readonly long DAY = HOUR * 24;
        private static readonly int HOURS = 24;

        public override string Name
        {
            get { return "Sensor data"; }
        }

        public override string Desc
        {
            get { return "The temperaturs, as read from an outside and an inside sensors"; }
        }

        public override Android.Content.Intent Execute(Android.Content.Context context)
        {
            String[] titles = new String[] { "Inside", "Outside" };
            long now = new Date().Time / DAY * DAY;
            IList<Date[]> x = new List<Date[]>();
            for (int i = 0; i < titles.Length; i++)
            {
                Date[] dates = new Date[HOURS];
                for (int j = 0; j < HOURS; j++)
                {
                    dates[j] = new Date(now - (HOURS - j) * HOUR);
                }
                x.Add(dates);
            }
            IList<double[]> values = new List<double[]>();

            values.Add(new double[] {21.2, 21.5, 21.7, 21.5, 21.4, 21.4, 21.3, 21.1, 20.6, 20.3, 20.2,
                19.9, 19.7, 19.6, 19.9, 20.3, 20.6, 20.9, 21.2, 21.6, 21.9, 22.1, 21.7, 21.5 });
            values.Add(new double[] {1.9, 1.2, 0.9, 0.5, 0.1, -0.5, -0.6, MathHelper.NullValue,
                MathHelper.NullValue, -1.8, -0.3, 1.4, 3.4, 4.9, 7.0, 6.4, 3.4, 2.0, 1.5, 0.9, -0.5,
                MathHelper.NullValue, -1.9, -2.5, -4.3 });

            int[] colors = new int[] { Color.Green, Color.Blue };
            PointStyle[] styles = new PointStyle[] { PointStyle.Circle, PointStyle.Diamond };
            XYMultipleSeriesRenderer renderer = BuildRenderer(colors, styles);
            int length = renderer.SeriesRendererCount;
            for (int i = 0; i < length; i++)
            {
                ((XYSeriesRenderer)renderer.GetSeriesRendererAt(i)).FillPoints = true;
            }
            SetChartSettings(renderer, "Sensor temperature", "Hour", "Celsius degrees", x[0][0].Time, x[0][HOURS - 1].Time, -5, 30, Color.LightGray, Color.LightGray);
            renderer.XLabels = 10;
            renderer.YLabels = 10;
            renderer.SetShowGrid(true);
            renderer.XLabelsAlign = Android.Graphics.Paint.Align.Center;
            renderer.SetYLabelsAlign(Android.Graphics.Paint.Align.Right);
            return ChartFactory.GetTimeChartIntent(context, BuildDateDataset(titles, x, values), renderer, "h:mm a");
        }
    }
}
