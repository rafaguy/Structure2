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
using Android.Text.Style;

namespace Structure.Droid.Implementation
{
    public class ClickableSpanImpl : ClickableSpan
    {
        public Action<Android.Views.View> Click;
       

        public override void OnClick(Android.Views.View widget)
        {
            Click?.Invoke(widget);
        }
    }
}