using Android.Graphics;
using Org.Achartengine;
using Org.Achartengine.Model;
using Org.Achartengine.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChartTest.Chat
{
    public class WeightDialChart : AbstractDemoChart
    {

        public override string Name
        {
            get { return "Weight chart"; }
        }

        public override string Desc
        {
            get { return "The weight indicator (dial chart)"; }
        }

        public override Android.Content.Intent Execute(Android.Content.Context context)
        {
            CategorySeries category = new CategorySeries("Weight indic");
            category.Add("Current", 75);
            category.Add("Minimum", 65);
            category.Add("Maximum", 90);
            DialRenderer renderer = new DialRenderer();
            renderer.ChartTitleTextSize = 20;
            renderer.LabelsTextSize = 15;
            renderer.LegendTextSize = 15;
            renderer.SetMargins(new int[] { 20, 30, 15, 0 });
            SimpleSeriesRenderer r = new SimpleSeriesRenderer();
            r.Color = Color.Blue;
            renderer.AddSeriesRenderer(r);
            r = new SimpleSeriesRenderer();
            r.Color = Color.Rgb(0, 150, 0);
            renderer.AddSeriesRenderer(r);
            r = new SimpleSeriesRenderer();
            r.Color = Color.Green;
            renderer.AddSeriesRenderer(r);
            renderer.LabelsTextSize = 10;
            renderer.LabelsColor = Color.White;
            renderer.ShowLabels = true;
            renderer.SetVisualTypes(new DialRenderer.Type[] { DialRenderer.Type.Arrow, DialRenderer.Type.Needle, DialRenderer.Type.Needle });
            renderer.MinValue = 0;
            renderer.MaxValue = 150;
            return ChartFactory.GetDialChartIntent(context, category, renderer, "Weight indicator");
        }
    }
}
