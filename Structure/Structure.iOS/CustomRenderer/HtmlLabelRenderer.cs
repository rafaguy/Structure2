using Foundation;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Structure.Extensions.HtmlLabel),typeof(Structure.iOS.CustomRenderer.HtmlLabelRenderer))]
namespace Structure.iOS.CustomRenderer
{
  public  class HtmlLabelRenderer: LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
                setText();
        }

        private void setText()
        {
            var attr = new NSAttributedStringDocumentAttributes();
            var nsError = new NSError();
            attr.DocumentType = NSDocumentType.HTML;
            var HtmlData = NSData.FromString(Element.Text, NSStringEncoding.Unicode);
            Control.AttributedText = new NSAttributedString(HtmlData,attr, ref nsError);
        }
    }
}
