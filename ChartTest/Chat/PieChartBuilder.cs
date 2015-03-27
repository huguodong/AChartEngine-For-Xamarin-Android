using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Org.Achartengine;
using Org.Achartengine.Model;
using Org.Achartengine.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChartTest.Chat
{
    [Activity()]
    public class PieChartBuilder : Activity
    {
        private static int[] COLORS = new int[] { Color.Green, Color.Blue, Color.Magenta, Color.Cyan };
        private CategorySeries mSeries = new CategorySeries("");
        private DefaultRenderer mRenderer = new DefaultRenderer();
        private Button mAdd;
        private EditText mValue;
        private GraphicalView mChartView;

        protected override void OnRestoreInstanceState(Android.OS.Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);
            mSeries = (CategorySeries)savedInstanceState.GetSerializable("current_series");
            mRenderer = (DefaultRenderer)savedInstanceState.GetSerializable("current_renderer");
        }

        protected override void OnSaveInstanceState(Android.OS.Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutSerializable("current_series", mSeries);
            outState.PutSerializable("current_renderer", mRenderer);
        }

        protected override void OnCreate(Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.xy_chart);
            mValue = FindViewById<EditText>(Resource.Id.xValue);
            mRenderer.ZoomButtonsVisible = true;
            mRenderer.StartAngle = 180;
            mRenderer.DisplayValues = true;

            mAdd = FindViewById<Button>(Resource.Id.add);
            mAdd.Enabled = true;
            mValue.Enabled = true;

            mAdd.Click += (e, s) =>
            {
                double value = 0;
                try
                {
                    value = Double.Parse(mValue.Text);
                }
                catch (Java.Lang.NumberFormatException)
                {
                    mValue.RequestFocus();
                    return;
                }
                mValue.Text = "";
                mValue.RequestFocus();
                mSeries.Add("Series " + (mSeries.ItemCount + 1), value);
                SimpleSeriesRenderer renderer = new SimpleSeriesRenderer();
                renderer.Color = COLORS[(mSeries.ItemCount - 1) % COLORS.Length];
                mRenderer.AddSeriesRenderer(renderer);
                mChartView.Repaint();
            };
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (mChartView == null)
            {
                LinearLayout layout = FindViewById<LinearLayout>(Resource.Id.chart);
                mChartView = ChartFactory.GetPieChartView(this, mSeries, mRenderer);
                mRenderer.ClickEnabled = true;
                mChartView.Click += (e, s) =>
                {
                    SeriesSelection seriesSelection = mChartView.CurrentSeriesAndPoint;
                    if (seriesSelection == null)
                    {
                        Toast.MakeText(this, "No chart element selected", ToastLength.Short).Show();
                    }
                    else
                    {
                        for (int i = 0; i < mSeries.ItemCount; i++)
                        {
                            mRenderer.GetSeriesRendererAt(i).Highlighted = (i == seriesSelection.PointIndex);
                        }
                        mChartView.Repaint();
                        Toast.MakeText(this, "Chart data point index " + seriesSelection.PointIndex + " selected "
                            + "point value=" + seriesSelection.Value, ToastLength.Short).Show();
                    }
                };
                layout.AddView(mChartView, new Android.Views.ViewGroup.LayoutParams(ViewGroup.LayoutParams.FillParent,
                    ViewGroup.LayoutParams.FillParent));
            }
            else
            {
                mChartView.Repaint();
            }
        }
    }
}
