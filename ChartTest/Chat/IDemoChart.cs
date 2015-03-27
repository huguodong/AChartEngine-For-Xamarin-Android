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

namespace ChartTest.Chat
{
    public interface IDemoChart
    {
        String Name { get; }
        String Desc { get; }
        Intent Execute(Context context);
    }
}