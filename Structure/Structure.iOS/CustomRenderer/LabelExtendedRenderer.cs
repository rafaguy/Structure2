using Foundation;
using Structure.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Structure.Extensions.ExtendedLabel),typeof(Structure.iOS.CustomRenderer.LabelExtendedRenderer))]
namespace Structure.iOS.CustomRenderer
{
    
 public   class LabelExtendedRenderer:LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            SetText();
        }
       

         private void SetText()
         {
            var attrib = new NSAttributedStringDocumentAttributes();
            var nsError = new NSError();
            attrib.DocumentType = NSDocumentType.HTML;
            
            var HtmlData = NSData.FromString(Element.Text, NSStringEncoding.Unicode);
            var str = new NSAttributedString(HtmlData, attrib, ref nsError);

            var spannableString = new NSMutableAttributedString(str);

             string pattern = @"\bhttps?://[\w./#?&-_=()]+\b";
             Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
             var matches = regex.Matches(Element.Text);
            
             foreach(Match match in matches)
             {
                var attr = new UIStringAttributes
                {
                    ForegroundColor = UIColor.Red,
                    
                };
                
                attr.Link =new NSUrl( match.Value);
                 spannableString.SetAttributes( attr, new NSRange(match.Index,match.Length));
                
             }
             Control.AttributedText = spannableString;
             
         }


         protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
         {
             base.OnElementPropertyChanged(sender, e);
         }
    }
}
