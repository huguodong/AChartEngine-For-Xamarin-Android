using Android.Graphics;
using Org.Achartengine;
using Org.Achartengine.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChartTest.Chat
{
    public class BudgetDoughnutChart : AbstractDemoChart
    {

        public override string Name
        {
            get { return "Budget chart for several tears"; }
        }

        public override string Desc
        {
            get { return "The budget per project for several years (doughnut chart)"; }
        }

        public override Android.Content.Intent Execute(Android.Content.Context context)
        {
            IList<double[]> values = new List<double[]>();
            values.Add(new double[] { 12, 14, 11, 10, 19 });
            values.Add(new double[] { 10, 9, 14, 20, 11 });
            IList<string[]> titles = new List<string[]>();
            titles.Add(new String[] { "P1", "P2", "P3", "P4", "P5" });
            titles.Add(new String[] { "Project1", "Project2", "Project3", "Project4", "Project5" });
            int[] colors = new int[] { Color.Blue, Color.Green, Color.Magenta, Color.Yellow, Color.Cyan };

            DefaultRenderer renderer = BuildCategoryRenderer(colors);
            renderer.ApplyBackgroundColor = true;
            renderer.BackgroundColor = Color.Rgb(222, 222, 200);
            renderer.LabelsColor = Color.Gray;
            return ChartFactory.GetDoughnutChartIntent(context, BuildMultipleCategoryDataset("Project budget", titles, values), renderer, "Doughnut chart demo");
        }
    }
}
