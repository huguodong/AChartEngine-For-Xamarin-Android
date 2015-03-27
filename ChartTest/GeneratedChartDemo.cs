using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Org.Achartengine.Model;
using Org.Achartengine.Chart;
using Org.Achartengine.Renderer;
using Android.Graphics;
using Org.Achartengine;

namespace ChartTest
{
    [Activity(Label = "GeneratedChartDemo")]
    public class GeneratedChartDemo : ListActivity
    {
        private static readonly int SERIES_NR = 2;
        private String[] mMenuText;
        private String[] mMenuSummary;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            mMenuText = new String[] { "Line chart", "Scatter chart", "Time chart", "Bar chart" };
            mMenuSummary = new String[] { "Line chart with randomly generated values",
            "Scatter chart with randomly generated values","Time chart with randomly generated values",
            "Bar chart with randomly generated values"};

            ListAdapter = new SimpleAdapter(this, GetListValues(), Android.Resource.Layout.SimpleListItem2,
                new String[] { "name", "desc" }, new int[] { Android.Resource.Id.Text1, Android.Resource.Id.Text2 });
        }

        private IList<IDictionary<String, Object>> GetListValues()
        {
            IList<IDictionary<String, Object>> values = new List<IDictionary<String, Object>>();
            int length = mMenuText.Length;
            for (int i = 0; i < length; i++)
            {
                IDictionary<String, Object> v = new JavaDictionary<String, Object>();
                v.Add("name", mMenuText[i]);
                v.Add("desc", mMenuSummary[i]);
                values.Add(v);
            }
            return values;
        }

        private XYMultipleSeriesDataset GetDemoDataset()
        {
            XYMultipleSeriesDataset dataset = new XYMultipleSeriesDataset();
            const int nr = 10;
            Random r = new Random();
            for (int i = 0; i < SERIES_NR; i++)
            {
                XYSeries series = new XYSeries("Demo series " + (i + 1));
                for (int k = 0; k < nr; k++)
                {
                    series.Add(k, 20 + r.Next() % 100);
                }
                dataset.AddSeries(series);
            }
            return dataset;
        }

        private XYMultipleSeriesDataset GetDateDemoDataset()
        {
            XYMultipleSeriesDataset dataset = new XYMultipleSeriesDataset();
            const int nr = 10;
            long value = new Java.Util.Date().Time - 3 * TimeChart.Day;
            Random r = new Random();
            for (int i = 0; i < SERIES_NR; i++)
            {
                TimeSeries series = new TimeSeries("Demo series " + (i + 1));
                for (int k = 0; k < nr; k++)
                {
                    series.Add(new Java.Util.Date(value + k * TimeChart.Day / 4), 20 + r.Next() % 100);
                }
                dataset.AddSeries(series);
            }
            return dataset;
        }

        private XYMultipleSeriesDataset GetBarDemoDataset()
        {
            XYMultipleSeriesDataset dataset = new XYMultipleSeriesDataset();
            const int nr = 10;
            Random r = new Random();
            for (int i = 0; i < SERIES_NR; i++)
            {
                CategorySeries series = new CategorySeries("Demo series " + (i + 1));
                for (int k = 0; k < nr; k++)
                {
                    series.Add(100 + r.Next() % 100);
                }
                dataset.AddSeries(series.ToXYSeries());
            }
            return dataset;
        }

        private XYMultipleSeriesRenderer GetDemoRenderer()
        {
            XYMultipleSeriesRenderer renderer = new XYMultipleSeriesRenderer();
            renderer.AxisTitleTextSize = 16;
            renderer.ChartTitleTextSize = 20;
            renderer.LabelsTextSize = 15;
            renderer.LegendTextSize = 15;
            renderer.PointSize = 5f;
            renderer.SetMargins(new int[] { 20, 30, 15, 0 });
            XYSeriesRenderer r = new XYSeriesRenderer();
            r.Color = Color.Blue;
            r.PointStyle = PointStyle.Square;
            r.FillBelowLine = true;
            r.SetFillBelowLineColor(Color.White);
            r.FillPoints = true;
            renderer.AddSeriesRenderer(r);
            r = new XYSeriesRenderer();
            r.PointStyle = PointStyle.Circle;
            r.Color = Color.Green;
            r.FillPoints = true;
            renderer.AddSeriesRenderer(r);
            renderer.AxesColor = Color.DarkGray;
            renderer.LabelsColor = Color.LightGray;
            return renderer;
        }

        public XYMultipleSeriesRenderer GetBarDemoRenderer()
        {
            XYMultipleSeriesRenderer renderer = new XYMultipleSeriesRenderer();
            renderer.AxisTitleTextSize = 16;
            renderer.ChartTitleTextSize = 20;
            renderer.LabelsTextSize = 15;
            renderer.LegendTextSize = 15;
            renderer.SetMargins(new int[] { 20, 30, 15, 0 });
            SimpleSeriesRenderer r = new SimpleSeriesRenderer();
            r.Color = Color.Blue;
            renderer.AddSeriesRenderer(r);
            r = new SimpleSeriesRenderer();
            r.Color = Color.Green;
            renderer.AddSeriesRenderer(r);
            return renderer;
        }

        private void SetChartSettings(XYMultipleSeriesRenderer renderer)
        {
            renderer.ChartTitle = "Chart demo";
            renderer.XTitle = "x values";
            renderer.YTitle = "y values";
            renderer.XAxisMin = 0.5;
            renderer.XAxisMax = 10.5;
            renderer.YAxisMin = 0;
            renderer.YAxisMax = 210;
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            base.OnListItemClick(l, v, position, id);
            switch (position)
            {
                case 0:
                    Intent intent = ChartFactory.GetLineChartIntent(this, GetDemoDataset(), GetDemoRenderer());
                    StartActivity(intent);
                    break;
                case 1:
                    intent = ChartFactory.GetScatterChartIntent(this, GetDemoDataset(), GetDemoRenderer());
                    StartActivity(intent);
                    break;
                case 2:
                    intent = ChartFactory.GetTimeChartIntent(this, GetDateDemoDataset(), GetDemoRenderer(), null);
                    StartActivity(intent);
                    break;
                case 3:
                    XYMultipleSeriesRenderer renderer = GetBarDemoRenderer();
                    SetChartSettings(renderer);
                    intent = ChartFactory.GetBarChartIntent(this, GetBarDemoDataset(), renderer, BarChart.Type.Default);
                    StartActivity(intent);
                    break;
            }
        }
    }
}