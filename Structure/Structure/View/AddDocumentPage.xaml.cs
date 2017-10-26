using System;
using Plugin.Media;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media.Abstractions;
using System.Collections.ObjectModel;
using Structure.Model;
using Structure.ViewModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using Structure.Data;
using Structure.Service;
using Structure.Interface;
using System.Linq;

namespace Structure.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddDocumentPage : ContentPage ,INotifyPropertyChanged
    {

        #region Properties
        public int _position;
        public int Position
        {
            get { return _position; }
            set { SetValue(ref _position, value); }
        }
        public ObservableCollection<Document> _carouselList;
        public ObservableCollection<Document> CarouselList
        {
            get { return _carouselList; }
            set { SetValue(ref _carouselList, value); }
        }

        public DatabaseAccess DatabaseAccess { get; set; }

        public DocumentService DocumentService { get; set; }

        #endregion

        public AddDocumentPage()
        {
            BindingContext = this;
            DatabaseAccess = new DatabaseAccess();
            DocumentService = new DocumentService();
            CarouselList = new ObservableCollection<Document>();
            InitializeComponent();
           
            ResetLayout(true);
            HandleView();

        }

        #region Clicked Events
        private async void BtnTakePhoto_Clicked(object sender, EventArgs e)
        {
            await TakePhoto();
            DiableCarousel();
        }

       
        private void ArrowBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async Task BtnSave_Clicked(object sender, EventArgs e)
        {

            string clientKey = "6FDFDD074B4BD30209085207575E5D0D"; //DatabaseAccess.GetClientKey();
            ResetLayout(false);

            var carouselItem = CarouselList[Position];

            carouselItem.Comments = MyEntry._editor.Text;

            carouselItem.Sent = await DocumentService.PostDocument(carouselItem, clientKey);
                 
            var docID = DatabaseAccess.CreateDocument(carouselItem);
            
            ResetLayout(true);

            DiableCarousel();
        }
        #endregion

        public void HandleView()
        {
            var items = DatabaseAccess.ReadAllDocuments().Where(x => x.Type == "Temporary").ToList();
            
            if (items.Count == 0 && CarouselList.Count == 0)
            {
                ShowTakePhoto(true);

            }
            else
            {
                ShowTakePhoto(false);
                CarouselList =new ObservableCollection<Document>(items);
            }
          
            
        }
        private void ShowTakePhoto(bool value)
        {
           
            BtnAddPhoto.IsVisible = value;
            MainContent.IsEnabled = !value;
            BtnSave.IsVisible = !value;
            BtnSave.IsEnabled = !value;
            if (value)
            {
                MainContent.Opacity = 0;
            }
            else
            {
                MainContent.Opacity = 1;

            }

        }
        public async Task TakePhoto()
        {

            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            {
                // Supply media options for saving our photo after it's taken.
                var mediaOptions = new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "DocumentPhoto",
                    Name = $"{DateTime.UtcNow}.jpg",
                    PhotoSize = PhotoSize.Small,
                    
                };

                // Take a photo 
                var file =  await CrossMedia.Current.TakePhotoAsync(mediaOptions);
                if (file != null)
                {
                    System.Diagnostics.Debug.WriteLine(file.Path + ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>+++++++++++========+++++++++++<<<<<<<<<<<<< ");
                    //DocImage.Source = file.Path;
                    await DisplayAlert("Success", "Saved to: " + file.Path, "OK");
                    var base64 = DependencyService.Get<IFileMgr>().GetBase64ImageString(file.Path);
                    var newDoc = new Document
                    {
                        Name = "Document" + DateTime.UtcNow.ToString(),
                        FilePath = file.Path.ToString(),
                        Data = base64,
                        Type =  "Temporary",
                    };

                    CarouselList.Add(newDoc);
                    Position = CarouselList.Count - 1;
                    
                }
                else
                {
                    await DisplayAlert("Failed", "No Photo Taken", "OK");
                }
            }
            DiableCarousel();


        }
        public void DiableCarousel()
        {
            ShowTakePhoto(false);
            var currentItem = CarouselList[Position];

            if (currentItem.Sent)
            {
              
                BtnSave.IsEnabled = false;
                BtnSave.IsVisible = false;

                MyCarousel.IsEnabled = true;

                BtnTakePhoto.IsEnabled = true;
                BtnTakePhoto.IsVisible = true;
            }
            else
            {
                MyCarousel.IsEnabled = false;

                BtnTakePhoto.IsEnabled = false;
                BtnTakePhoto.IsVisible = false;

                BtnSave.IsEnabled = true;
                BtnSave.IsVisible = true;
            }
        }
        public void SaveManyDocuments(ObservableCollection<Document> documents)
        {
            DatabaseAccess.CreateManyDocuments(documents);
        }

        /// <summary>
        /// Resets the layout.
        /// </summary>
        public void ResetLayout(bool status)
        {
            ActivityLayout.IsVisible = !status;
            ActivityLayout.IsRunning = !status;
            MainContent.IsEnabled = status;
            if (status)
            {
                MainContent.Opacity = 1;
            }
            else
            {
                MainContent.Opacity = 0.4;

            }
        }

        #region PropertyCHanged Event
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetValue<T>(ref T backingField, T value, [CallerMemberName]string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
                return;
            backingField = value;
            OnPropertyChanged(propertyName);
        }
        #endregion
       
        #region Footer Navigations
        private void BtnDoc_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new DocumentsPage());

        }

        private void BtnComm_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new NotificationCommunicationPage());

        }

        private void BtnHelp_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new WebPage());
        }

        private void BtnConfig_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new ConfigurationPage());

        }

        private void BtnHome_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }

        #endregion

        
    }
}