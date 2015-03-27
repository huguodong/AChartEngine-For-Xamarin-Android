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
using ChartTest.Chat;

namespace ChartTest
{
    [Activity(Label = "ChartDemo", MainLauncher = true)]
    public class ChartDemo : ListActivity
    {
        private IDemoChart[] mCharts = new IDemoChart[] { new AverageTemperatureChart(),
            new AverageCubicTemperatureChart(),new SalesStackedBarChart(),new SalesBarChart(),
            new TrigonometricFunctionsChart(),new ScatterChart(),new SalesComparisonChart(),
            new ProjectStatusChart(), new SalesGrowthChart(),new BudgetPieChart(),
            new BudgetDoughnutChart(),new ProjectStatusBubbleChart(),new TemperatureChart(),
            new WeightDialChart(),new SensorValuesChart(),new CombinedTemperatureChart(),
            new MultipleTemperatureChart()};

        private String[] mMenuText;
        private String[] mMenuSummary;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            int length = mCharts.Length;
            mMenuText = new String[length + 3];
            mMenuSummary = new String[length + 3];
            mMenuText[0] = "Embedded line chart demo";
            mMenuSummary[0] = "A demo on how to include a clickable line chart into a graphical activity";
            mMenuText[1] = "Embedded pie chart demo";
            mMenuSummary[1] = "A demo on how to include a clickable pie chart into a graphical activity";
            for (int i = 0; i < length; i++)
            {
                mMenuText[i + 2] = mCharts[i].Name;
                mMenuSummary[i + 2] = mCharts[i].Desc;
            }
            mMenuText[length + 2] = "Random values charts";
            mMenuSummary[length + 2] = "Chart demos using randomly generated values";
            ListAdapter = new SimpleAdapter(this, GetListValues(), Android.Resource.Layout.SimpleListItem1,
            new String[] { "name", "desc" }, new int[] { Android.Resource.Id.Text1, Android.Resource.Id.Text2 });
        }

        private IList<IDictionary<string, object>> GetListValues()
        {
            IList<IDictionary<string, object>> values = new List<IDictionary<string, object>>();
            int length = mMenuText.Length;
            for (int i = 0; i < length; i++)
            {
                IDictionary<string, object> v = new JavaDictionary<string, object>();
                v.Add("name", mMenuText[i]);
                v.Add("desc", mMenuSummary[i]);
                values.Add(v);
            }
            return values;
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            base.OnListItemClick(l, v, position, id);
            Intent intent = null;
            if (position == 0)
            {
                intent = new Intent(this, typeof(XYChartBuilder));
            }
            else if (position == 1)
            {
                intent = new Intent(this, typeof(PieChartBuilder));
            }
            else if (position <= mCharts.Length + 1)
            {
                intent = mCharts[position - 2].Execute(this);
            }
            else
            {
                intent = new Intent(this, typeof(GeneratedChartDemo));
            }
            StartActivity(intent);
        }
    }
}