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
using Structure.Utils;

namespace Structure.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddDocumentPage : ContentPage ,INotifyPropertyChanged,IPageNotification
    {

        #region Properties
        private int _position;
        public int Position
        {
            get { return _position; }
            set { SetValue(ref _position, value); }
        }
        private ObservableCollection<Document> _carouselList;
        public ObservableCollection<Document> CarouselList
        {
            get { return _carouselList; }
            set { SetValue(ref _carouselList, value); }
        }

        public string CultureInfo { get; set; }

        public DatabaseAccess DatabaseAccess { get; set; }

        public DocumentService DocumentService { get; set; }
        public int NewComCount
        {
            get
            {
                return _count;
            }
            set
            {
                if(_count!=value)
                {
                    _count = value;
                    OnPropertyChanged();
                }
            }
        }


        #endregion

        #region Private Variables

        private string _temporary;
        private int _count;
        private string _takePhoto;
        private string _noPhoto;
        private string _ok;
        #endregion
        public AddDocumentPage()
        {
            BindingContext = this;
            NewComCount = GlobalCommunicationDataSource.CurrentNewComNumber;
            DatabaseAccess = new DatabaseAccess();
            DocumentService = new DocumentService();
            CarouselList = new ObservableCollection<Document>();
            InitializeComponent();
            SetLanguage();
            ResetLayout(true);
            HandleView();

        }

        #region Clicked Events
        private void BtnDeletePhoto_Clicked(object sender, EventArgs e)
        {
            var displayItem = CarouselList[Position];

            var dbItem = DatabaseAccess.ReadDocument(displayItem.Id);
            if (dbItem != null)
            {
                DatabaseAccess.DeleteDocument(dbItem);
            }
            CarouselList.Remove(displayItem);
            if (CarouselList.Count == 0)
            {
                ShowTakePhoto(true);
            }
        }
        private async void BtnTakePhoto_Clicked(object sender, EventArgs e)
        {
            await TakePhoto();
            DiableCarousel();
        }


       
        private void ArrowBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        //BtnSave_Clicked
        private async void BtnSave_Clicked(object sender, EventArgs e)
        {

            string clientKey = DatabaseAccess.GetClientKey();
            ResetLayout(false);

            var carouselItem = CarouselList[Position];

            carouselItem.Comments = MyEntry._editor.Text;

            var position = DependencyService.Get<ILocationService>().GetLocation();
            position.ClientKey = clientKey;

            carouselItem.Sent = await DocumentService.PostDocument(carouselItem, clientKey , position);
                 
            DatabaseAccess.CreateDocument(carouselItem);
            
            ResetLayout(true);

            DiableCarousel();
        }
        #endregion

        public void  HandleTranslation(string cultureInfo)
        {
            _temporary =  Localize.GetString(Constants.Temporary, cultureInfo);
            _noPhoto = Localize.GetString(Constants.NoPhoto,cultureInfo);
            _takePhoto = Localize.GetString(Constants.TakePhoto, cultureInfo);
            _ok = Localize.GetString(Constants.ok, cultureInfo);
        }
        public void SetLanguage()
        {
            CultureInfo = DatabaseAccess.GetCultureInfo();
            HandleTranslation(CultureInfo);
        }
        public void HandleView()
        {
            var items = DatabaseAccess.ReadAllDocuments().Where(x => x.Type == _temporary).ToList();
            
            if (items.Count == 0 && CarouselList.Count == 0)
            {
                ShowTakePhoto(true);

            }
            else
            {
                ShowTakePhoto(false);
                BtnSave.IsVisible = false;
                BtnSave.IsEnabled = false;
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
                    var base64 = DependencyService.Get<IFileMgr>().GetBase64ImageString(file.Path);
                    var newDoc = new Document
                    {
                        Name = "Document " + DateTime.UtcNow.ToString(),
                        FileName = "Document " + DateTime.UtcNow.ToString(),
                        FilePath = file.Path.ToString(),
                        Data = base64,
                        Type =  _temporary,
                    };

                    CarouselList.Add(newDoc);
                    Position = CarouselList.Count - 1;
                    DiableCarousel();
                }
                else
                {
                    await DisplayAlert(_takePhoto, _noPhoto, _ok);
                }
            }
            


        }

        public void DiableCarousel()
        {
            var items = DatabaseAccess.ReadAllDocuments().Where(x => x.Type == _temporary).ToList();

            if (items.Count != 0 || CarouselList.Count != 0)
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
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
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