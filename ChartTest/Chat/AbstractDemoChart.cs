using Java.Util;
using Org.Achartengine.Chart;
using Org.Achartengine.Model;
using Org.Achartengine.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChartTest.Chat
{
    public abstract class AbstractDemoChart : IDemoChart
    {
        protected XYMultipleSeriesDataset BuildDataset(String[] titles, IList<double[]> xValues, IList<double[]> yValues)
        {
            XYMultipleSeriesDataset dataset = new XYMultipleSeriesDataset();
            AddXYSeries(dataset, titles, xValues, yValues, 0);
            return dataset;
        }

        public void AddXYSeries(XYMultipleSeriesDataset dataset, String[] titles, IList<double[]> xValues, IList<double[]> yValues, int scale)
        {
            int length = titles.Length;
            for (int i = 0; i < length; i++)
            {
                XYSeries series = new XYSeries(titles[i], scale);
                double[] xV = xValues[i];
                double[] yV = yValues[i];
                int seriesLength = xV.Length;
                for (int j = 0; j < seriesLength; j++)
                {
                    series.Add(xV[j], yV[j]);
                }
                dataset.AddSeries(series);
            }
        }

        protected XYMultipleSeriesRenderer BuildRenderer(int[] colors, PointStyle[] styles)
        {
            XYMultipleSeriesRenderer renderer = new XYMultipleSeriesRenderer();
            SetRenderer(renderer, colors, styles);
            return renderer;
        }

        protected void SetRenderer(XYMultipleSeriesRenderer renderer, int[] colors, PointStyle[] styles)
        {
            renderer.AxisTitleTextSize = 16;
            renderer.ChartTitleTextSize = 20;
            renderer.LabelsTextSize = 15;
            renderer.LegendTextSize = 15;
            renderer.PointSize = 5f;
            renderer.SetMargins(new[] { 20, 30, 15, 20 });
            int length = colors.Length;
            for (int i = 0; i < length; i++)
            {
                XYSeriesRenderer r = new XYSeriesRenderer();
                r.Color = colors[i];
                r.PointStyle = styles[i];
                renderer.AddSeriesRenderer(r);
            }
        }

        protected void SetChartSettings(XYMultipleSeriesRenderer renderer, String title, String xTitle,
            String yTitle, double xMin, double xMax, double yMin, double yMax, int axesColor, int labelsColor)
        {
            renderer.ChartTitle = title;
            renderer.XTitle = xTitle;
            renderer.YTitle = yTitle;
            renderer.XAxisMin = xMin;
            renderer.XAxisMax = xMax;
            renderer.YAxisMin = yMin;
            renderer.YAxisMax = yMax;
            renderer.AxesColor = axesColor;
            renderer.LabelsColor = labelsColor;
        }

        protected XYMultipleSeriesDataset BuildDateDataset(String[] titles, IList<Date[]> xValues, IList<double[]> yValues)
        {
            XYMultipleSeriesDataset dataset = new XYMultipleSeriesDataset();
            int length = titles.Length;
            for (int i = 0; i < length; i++)
            {
                TimeSeries series = new TimeSeries(titles[i]);
                Date[] xV = xValues[i];
                double[] yV = yValues[i];
                int seriesLength = xV.Length;
                for (int j = 0; j < seriesLength; j++)
                {
                    series.Add(xV[j], yV[j]);
                }
                dataset.AddSeries(series);
            }
            return dataset;
        }

        protected CategorySeries BuildCategoryDataset(String title, double[] values)
        {
            CategorySeries series = new CategorySeries(title);
            int k = 0;
            foreach (double item in values)
            {
                series.Add("Project " + ++k, item);
            }
            return series;
        }

        protected MultipleCategorySeries BuildMultipleCategoryDataset(String title, IList<String[]> titles, IList<double[]> values)
        {
            MultipleCategorySeries series = new MultipleCategorySeries(title);
            for(int i = 0;i < values.Count;i++)
            {
                series.Add(2007 + i++ + "", titles[i], values[i]);
            }
            return series;
        }

        protected DefaultRenderer BuildCategoryRenderer(int[] colors)
        {
            DefaultRenderer renderer = new DefaultRenderer();
            renderer.LabelsTextSize = 15;
            renderer.LegendTextSize = 15;
            renderer.SetMargins(new[] { 20, 30, 15, 0 });
            foreach (int color in colors)
            {
                SimpleSeriesRenderer r = new SimpleSeriesRenderer();
                r.Color = color;
                renderer.AddSeriesRenderer(r);
            }
            return renderer;
        }

        protected XYMultipleSeriesDataset BuildBarDataset(String[] title, IList<double[]> values)
        {
            XYMultipleSeriesDataset dataset = new XYMultipleSeriesDataset();
            int length = title.Length;
            for (int i = 0; i < length; i++)
            {
                CategorySeries series = new CategorySeries(title[i]);
                double[] v = values[i];
                int seriesLength = v.Length;
                for (int k = 0; k < seriesLength; k++)
                {
                    series.Add(v[k]);
                }
                dataset.AddSeries(series.ToXYSeries());
            }
            return dataset;
        }

        protected XYMultipleSeriesRenderer BuildBarRenderer(int[] colors)
        {
            XYMultipleSeriesRenderer renderer = new XYMultipleSeriesRenderer();
            renderer.AxisTitleTextSize = 16;
            renderer.ChartTitleTextSize = 20;
            renderer.LabelsTextSize = 15;
            renderer.LegendTextSize = 15;
            int length = colors.Length;
            for (int i = 0; i < length; i++)
            {
                SimpleSeriesRenderer r = new SimpleSeriesRenderer();
                r.Color = colors[i];
                renderer.AddSeriesRenderer(r);
            }
            return renderer;
        }

        public abstract string Name { get; }

        public abstract string Desc { get; }

        public abstract Android.Content.Intent Execute(Android.Content.Context context);
    }
}
