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
using Android.Text;
using Xamarin.Forms.Platform.Android;
using Structure.Extensions;
using Android.Text.Method;
using Android.Text.Style;
using Structure.Droid.Implementation;
using System.Text.RegularExpressions;
using Android.Graphics;

[assembly:ExportRenderer(typeof(Structure.Extensions.ExtendedLabel),typeof(Structure.Droid.CustomRenderer.ExtendedLabelRenderer))]
namespace Structure.Droid.CustomRenderer
{
  public  class ExtendedLabelRenderer:LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                SetText();
            }
        }
        

        private void SetText()
        {
            //SpannableString spanString =new SpannableString(@"<p>Go to <a href='http://www.google.ge'>Google </a></p>");
            var spanString = new SpannableString(Html.FromHtml(Element.Text));
            string pattern = @"\bhttps?://[\w./#?&-_=()]+\b";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            var matches = regex.Matches(Element.Text);
            foreach(Match matche in matches)
            {
                spanString.SetSpan(new StyleSpan(TypefaceStyle.Italic), matche.Index, matche.Length + matche.Index, SpanTypes.ExclusiveExclusive); ;
                spanString.SetSpan(new URLSpan(matche.Value),matche.Index, matche.Length+matche.Index, SpanTypes.ExclusiveExclusive);
            } 
            Control.TextFormatted =spanString;
            Control.MovementMethod = LinkMovementMethod.Instance;
        }
    }
}