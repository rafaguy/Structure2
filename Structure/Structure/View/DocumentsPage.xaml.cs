
using Structure.Data;
using Structure.Interface;
using Structure.Model;
using Structure.Service;
using Structure.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
//using Structure.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Structure.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DocumentsPage : ContentPage
	{
        public DatabaseAccess DatabaseAccess { get; set; }
        public DocumentService DocumentService { get; set; }

        readonly MainViewModel _mainViewModel = new MainViewModel();
        public DocumentsPage ()
		{
            DatabaseAccess = new DatabaseAccess();
            DocumentService = new DocumentService();
            
            InitializeComponent ();

           
           // HandleDocumentDownload().Wait();


        }
        protected async override void OnAppearing()
        {
            ResetLayout(false);
            BindingContext = _mainViewModel;
            await HandleDocumentDownload();
            
            base.OnAppearing();
        }

        #region Clicked Events
        private void AddDocument_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AddDocumentPage());

        }
        private void ExpanClicked(object sender, System.EventArgs e)
        {
            var x = sender as TapGestureRecognizer;
             x.Command = _mainViewModel.HeaderSelectedCommand;

            /* Command="{Binding Source={x:Reference TheDocumentsPage}, Path=BindingContext.HeaderSelectedCommand}"*/
        }
        private void ArrowBack_Clicked(object sender, System.EventArgs e)
        {
            var x = Navigation.NavigationStack;

            Navigation.PopAsync();
        }


        private void LstDocs_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Document;

            if (item != null)
            {
                var openFile = DependencyService.Get<IFileOpener>();
                openFile.OpenFile(item.FilePath);
                //LstDocs.SelectedItem = null;
            }
          
        }

        #endregion

        public async Task HandleDocumentDownload()
        {
           
            var result = await _mainViewModel.DownloadAndSaveDocumentToDB();
            if (result)
            {
                ResetLayout(true);
            }
            
        }

        public void ResetLayout(bool status)
        {
            ActivityLayout.IsVisible = !status;
            ActivityLayout.IsRunning = !status;
            //MainContent.IsEnabled = status;
            if (status)
            {
              //  MainContent.Opacity = 1;
            }
            else
            {
            //    MainContent.Opacity = 0.4;

            }
        }

        #region DocumentService

       
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

        private void BtnHome_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }

        #endregion

       
    }


}