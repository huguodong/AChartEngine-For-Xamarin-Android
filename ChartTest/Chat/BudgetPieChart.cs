using Android.Content;
using Android.Graphics;
using Org.Achartengine;
using Org.Achartengine.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChartTest.Chat
{
    public class BudgetPieChart : AbstractDemoChart
    {

        public override string Name
        {
            get { return "Budget chart"; }
        }

        public override string Desc
        {
            get { return "The budget per project for this year (pie chart)"; }
        }

        public override Android.Content.Intent Execute(Android.Content.Context context)
        {
            double[] values = new double[] { 12, 14, 11, 10, 19 };
            int[] colors = new int[] { Color.Blue, Color.Green, Color.Magenta, Color.Yellow, Color.Cyan };
            DefaultRenderer renderer = BuildCategoryRenderer(colors);
            renderer.ZoomButtonsVisible = true;
            renderer.ZoomEnabled = true;
            renderer.ChartTitleTextSize = 20;
            renderer.DisplayValues = true;
            renderer.ShowLabels = false;
            SimpleSeriesRenderer r = renderer.GetSeriesRendererAt(0);
            r.GradientEnabled = true;
            r.SetGradientStart(0, Color.Blue);
            r.SetGradientStop(0, Color.Green);
            r.Highlighted = true;
            Intent intent = ChartFactory.GetPieChartIntent(context, BuildCategoryDataset("Project budget", values), renderer, "Budget");
            return intent;
        }
    }
}
