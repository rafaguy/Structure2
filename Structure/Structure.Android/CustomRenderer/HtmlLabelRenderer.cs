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
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Text;

[assembly: ExportRenderer(typeof(Structure.Extensions.HtmlLabel),typeof(Structure.Droid.CustomRenderer.HtmlLabelRenderer))]
namespace Structure.Droid.CustomRenderer
{
   public class HtmlLabelRenderer: LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if(e.NewElement !=null)
              setText();
        }

        private void setText()
        {
            Control?.SetText(Html.FromHtml(Element.Text), TextView.BufferType.Spannable);
        }
    }
}