using Structure.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Structure.Utils
{
    public class CarouselEntry : StackLayout
    {
        public Editor _editor { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarouselEntry"/> class.
        /// </summary>
        public CarouselEntry()
        {
            _editor = new Editor() { HeightRequest = 100 , WidthRequest =1000 ,BackgroundColor = Color.Gray};
            this.HorizontalOptions = LayoutOptions.CenterAndExpand;
                this.Children.Add(_editor);
        }

        public static readonly BindableProperty PositionProperty = BindableProperty.Create(nameof(Position), typeof(int), typeof(CarouselEntry), 0, BindingMode.TwoWay, propertyChanging: PositionChanging);
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(CarouselEntry), Enumerable.Empty<object>(), BindingMode.OneWay, propertyChanged: ItemsChanged);
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(TextSource), typeof(IEnumerable), typeof(CarouselEntry), Enumerable.Empty<object>(), BindingMode.OneWay, propertyChanged: ItemsChanged);
      
        public string TextSource
        {
            get { return (string)this.GetValue(PositionProperty); }
            set { this.SetValue(PositionProperty, value); }
        }

        public int Position
        {
            get { return (int)this.GetValue(PositionProperty); }
            set { this.SetValue(PositionProperty, value); }
        }

        public ICollection ItemsSource
        {
            get { return (ICollection)this.GetValue(ItemsSourceProperty); }
            set { this.SetValue(ItemsSourceProperty, (object)value); }
        }

        private void Init(int position)
        {
            var list = new List<Document>();
            if (ItemsSource !=null)
            {
                foreach (var item in ItemsSource)
                {
                    list.Add(item as Document);
                }
                if (list.Count > 0)
                {
                    var x = list[position];
                    _editor.Text = x.Comments;
                }

            }

        }
        public static void PositionChanging(object bindable, object oldValue, object newValue)
        {
            var carouselEnrty = bindable as CarouselEntry;

            carouselEnrty.Init(Convert.ToInt32(newValue));
        }
        private void Clear()
        {
            _editor.Text = "";
        }
        private static void ItemsChanged(object bindable, object oldValue, object newValue)
        {
            var carouselEntry = bindable as CarouselEntry;

            carouselEntry.Clear();
            carouselEntry.Init(0);
        }

    }
}
