using Xamarin.Forms;

namespace Structure.Utils
{
    public class CustomProgressBar : ProgressBar
    {
        public static BindableProperty BarColorProperty
        = BindableProperty.Create<CustomProgressBar, Color>(p => p.BarColor, default(Color));

        public Color BarColor
        {
            get { return (Color)GetValue(BarColorProperty); }
            set { SetValue(BarColorProperty, value); }
        }
    }
}
