using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Org.Achartengine;
using Org.Achartengine.Chart;
using Org.Achartengine.Model;
using Org.Achartengine.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChartTest.Chat
{
    [Activity]
    public class XYChartBuilder : Activity
    {
        private XYMultipleSeriesDataset mDataset = new XYMultipleSeriesDataset();
        private XYMultipleSeriesRenderer mRenderer = new XYMultipleSeriesRenderer();
        private XYSeries mCurrentSeries;
        private XYSeriesRenderer mCurrentRenderer;
        private Button mNewSeries;
        private Button mAdd;
        private EditText mX;
        private EditText mY;
        private GraphicalView mChartView;

        protected override void OnSaveInstanceState(Android.OS.Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutSerializable("dataset", mDataset);
            outState.PutSerializable("renderer", mRenderer);
            outState.PutSerializable("current_series", mCurrentSeries);
            outState.PutSerializable("current_renderer", mCurrentRenderer);
        }

        protected override void OnRestoreInstanceState(Android.OS.Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);
            mDataset = savedInstanceState.GetSerializable("dataset") as XYMultipleSeriesDataset;
            mRenderer = savedInstanceState.GetSerializable("renderer") as XYMultipleSeriesRenderer;
            mCurrentSeries = savedInstanceState.GetSerializable("current_series") as XYSeries;
            mCurrentRenderer = savedInstanceState.GetSerializable("current_renderer") as XYSeriesRenderer;
        }

        protected override void OnCreate(Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.xy_chart);

            mX = FindViewById<EditText>(Resource.Id.xValue);
            mY = FindViewById<EditText>(Resource.Id.yValue);
            mAdd = FindViewById<Button>(Resource.Id.add);

            mRenderer.ApplyBackgroundColor = true;
            mRenderer.BackgroundColor = Color.Argb(100, 50, 50, 50);
            mRenderer.AxisTitleTextSize = 16;
            mRenderer.ChartTitleTextSize = 20;
            mRenderer.LabelsTextSize = 15;
            mRenderer.LegendTextSize = 15;
            mRenderer.SetMargins(new int[] { 20, 30, 15, 0 });
            mRenderer.ZoomButtonsVisible = true;
            mRenderer.PointSize = 5;

            mNewSeries = FindViewById<Button>(Resource.Id.new_series);
            mNewSeries.Click += (e, s) =>
            {
                String seriesTitle = "Series " + mDataset.SeriesCount + 1;
                XYSeries series = new XYSeries(seriesTitle);
                mDataset.AddSeries(series);
                mCurrentSeries = series;
                XYSeriesRenderer renderer = new XYSeriesRenderer();
                mRenderer.AddSeriesRenderer(renderer);
                renderer.PointStyle = PointStyle.Circle;
                renderer.FillPoints = true;
                renderer.DisplayChartValues = true;
                renderer.DisplayChartValuesDistance = 10;
                mCurrentRenderer = renderer;
                SetSeriesWidgetsEnabled(true);
                mChartView.Repaint();
            };

            mAdd.Click += (e, s) =>
            {
                double x = 0;
                double y = 0;
                try
                {
                    x = Double.Parse(mX.Text);
                }
                catch (Exception)
                {
                    mX.RequestFocus();
                    return;
                }
                try
                {
                    y = double.Parse(mY.Text);
                }
                catch (Exception)
                {
                    mY.RequestFocus();
                    return;
                }
                mCurrentSeries.Add(x, y);
                mX.Text = "";
                mY.Text = "";
                mX.RequestFocus();
                mChartView.Repaint();
            };
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (mChartView == null)
            {
                LinearLayout layout = FindViewById<LinearLayout>(Resource.Id.chart);
                mChartView = ChartFactory.GetLineChartView(this, mDataset, mRenderer);
                mRenderer.ClickEnabled = true;
                mRenderer.SelectableBuffer = 10;
                mChartView.Click += (e, s) =>
                    {
                        SeriesSelection seriesSelection = mChartView.CurrentSeriesAndPoint;
                        if (seriesSelection == null)
                        {
                            Toast.MakeText(this, "No chart element", ToastLength.Short).Show();
                        }
                        else
                        {
                            Toast.MakeText(this, "Chart element in series index " + seriesSelection.SeriesIndex
                                + " data point index " + seriesSelection.PointIndex + " was clicked"
                                + " closest point value X=" + seriesSelection.XValue + ",Y="
                                + seriesSelection.Value, ToastLength.Short).Show();
                        }
                    };
                layout.AddView(mChartView, new Android.Views.ViewGroup.LayoutParams(ViewGroup.LayoutParams.FillParent,
                    ViewGroup.LayoutParams.FillParent));
                bool enabled = mDataset.SeriesCount > 0;
                SetSeriesWidgetsEnabled(enabled);
            }
            else
            {
                mChartView.Repaint();
            }
        }

        private void SetSeriesWidgetsEnabled(bool enabled)
        {
            mX.Enabled = enabled;
            mY.Enabled = enabled;
            mAdd.Enabled = enabled;
        }
    }
}
