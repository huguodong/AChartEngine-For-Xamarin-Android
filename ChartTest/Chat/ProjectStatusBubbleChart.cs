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
    public class ProjectStatusBubbleChart : AbstractDemoChart
    {
        public override string Name
        {
            get { return "Project tickets status"; }
        }

        public override string Desc
        {
            get { return "The opened tickets and the fixed tickets (bubble chart)"; }
        }

        public override Android.Content.Intent Execute(Android.Content.Context context)
        {
            XYMultipleSeriesDataset series = new XYMultipleSeriesDataset();
            XYValueSeries newTicketSeries = new XYValueSeries("New Tickets");
            newTicketSeries.Add(1f, 2, 14);
            newTicketSeries.Add(2f, 2, 12);
            newTicketSeries.Add(3f, 2, 18);
            newTicketSeries.Add(4f, 2, 5);
            newTicketSeries.Add(5f, 2, 1);
            series.AddSeries(newTicketSeries);
            XYValueSeries fixedTicketSeries = new XYValueSeries("Fixed Tickets");
            fixedTicketSeries.Add(1f, 1, 7);
            fixedTicketSeries.Add(2f, 1, 4);
            fixedTicketSeries.Add(3f, 1, 18);
            fixedTicketSeries.Add(4f, 1, 3);
            fixedTicketSeries.Add(5f, 1, 1);
            series.AddSeries(fixedTicketSeries);

            XYMultipleSeriesRenderer renderer = new XYMultipleSeriesRenderer();
            renderer.AxisTitleTextSize = 16;
            renderer.ChartTitleTextSize = 20;
            renderer.LabelsTextSize = 15;
            renderer.LegendTextSize = 15;
            renderer.SetMargins(new int[] { 20, 30, 15, 0 });
            XYSeriesRenderer newTicketRenderer = new XYSeriesRenderer();
            newTicketRenderer.Color = Color.Blue;
            renderer.AddSeriesRenderer(newTicketRenderer);
            XYSeriesRenderer fixedTicketRenderer = new XYSeriesRenderer();
            fixedTicketRenderer.Color = Color.Green;
            renderer.AddSeriesRenderer(fixedTicketRenderer);

            SetChartSettings(renderer, "Project work status", "Priority", "", 0.5, 5.5, 0, 5, Color.Gray, Color.LightGray);
            renderer.XLabels = 7;
            renderer.YLabels = 0;
            renderer.SetShowGrid(false);
            return ChartFactory.GetBubbleChartIntent(context, series, renderer, "Project tickets");
        }
    }
}
