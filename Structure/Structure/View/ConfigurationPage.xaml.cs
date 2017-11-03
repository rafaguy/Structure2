using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Structure.Data;
using Structure.Interface;
using Structure.Model;
using Structure.Service;
using Structure.Utils;
using Structure.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Forms.Button;
using static Xamarin.Forms.Button.ButtonContentLayout;

namespace Structure.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfigurationPage : ContentPage
    {
        #region Variables

        private string _netInfo;
        private string _netNotAvailable;
        private string _ok;

        private string _loginError;
        private string _loginMsg;

        #endregion

        public ConfigurationPageViewModel ConfigurationViewModel { get; set; } = new ConfigurationPageViewModel { NewComCount=GlobalCommunicationDataSource.CurrentNewComNumber};
        public DatabaseAccess DatabaseAccess { get; set; }

        public LoginService LoginService { get; set; }
        public string CultureInfo { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationPage" /> class.
        /// </summary>
        public ConfigurationPage()
        {
            DatabaseAccess = new DatabaseAccess();
            LoginService = new LoginService();
            this.BindingContext = ConfigurationViewModel;
            InitializeComponent();
            ResetLayout(true);
            PopulateConfiguration();
            SetLanguage();
         

        }
      

        public void PopulateConfiguration()
        {
           var configExist = DatabaseAccess.ReadOneConfiguration();

            if (configExist == null)
            {
                FooterDisable(true);
              

            }
            else
            {
                
                TxtUsername.Text = configExist.Login;
                TxtPassword.Text = configExist.Pwd;
                LanguagePicker.SelectedIndex = configExist.LanguageId;
                FooterDisable(false);
            }

        }

        private void FooterDisable(bool isDisabled)
        {
         
            BtnComm.IsEnabled = !isDisabled;
            BtnConfig.IsEnabled = !isDisabled;
            BtnDoc.IsEnabled = !isDisabled;
            BtnHelp.IsEnabled = !isDisabled;
            BtnHome.IsEnabled = !isDisabled;
        
        }

        /// <summary>
        /// When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override void OnAppearing()
        {
            if (!CheckNetwork())
            {
                DisplayAlert(_netInfo, _netNotAvailable, _ok);
            }
        }

        /// <summary>
        /// Checks the network.
        /// </summary>
        /// <returns></returns>
        private bool CheckNetwork()
        {
            CrossConnectivity.Current.ConnectivityChanged += UpdateNetworkInfo;
            return Boolean.Parse(CrossConnectivity.Current.IsConnected.ToString());
        }

        /// <summary>
        /// When overridden, allows the application developer to customize behavior as the <see cref="T:Xamarin.Forms.Page" /> disappears.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override void OnDisappearing()
        {
            CrossConnectivity.Current.ConnectivityChanged -= UpdateNetworkInfo;
        }

        /// <summary>
        /// Updates the network information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ConnectivityChangedEventArgs"/> instance containing the event data.</param>
        private void UpdateNetworkInfo(object sender, ConnectivityChangedEventArgs e)
        {
            var connectionType = CrossConnectivity.Current.ConnectionTypes.FirstOrDefault();
            var conNet =  connectionType.ToString();
        }

        /// <summary>
        /// Trigger the login and save of config resources
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void LoginBtn_Clicked(object sender, EventArgs e)
        {

            var language = LanguagePicker.SelectedItem as LanguageModel;
            if (CheckNetwork())
            {
                if (language != null)
                {
                    ResetLayout(false);

                    string login = TxtUsername.Text;
                    string pwrd = TxtPassword.Text;
                    string lang = CultureInfo;
                    int langIndex = LanguagePicker.SelectedIndex;
                    DatabaseAccess.SaveDefaultLanguage(language);
                    SetLanguage();

                    var clientKey = await AuthentifyLogin(login, pwrd, lang);
                    if (clientKey != "KO")
                    {
                        ResetLayout(true);
                        //save config to db
                        DatabaseAccess.CreateConfiguration(new ConfigurationModel { ClientKey = clientKey, Language = lang, Login = login, Pwd = pwrd, LanguageId = langIndex });

                        await Navigation.PushAsync(new HomePage());
                    }
                    else
                    {
                        ResetLayout(true);
                        await DisplayAlert(_loginError, _loginMsg, _ok);
                    }
                }
                else
                {
                    await DisplayAlert(_loginError, _loginMsg, _ok);
                }
               
            }
            else
            {
                await DisplayAlert(_netInfo, _netNotAvailable, _ok);
            }
        }

        /// <summary>
        /// Authentifies the login user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public async Task<string> AuthentifyLogin(string userName, string password,string language)
        {

            var clientKey = await LoginService.PostConfig(userName, password,language);

            return clientKey;

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


        /// <summary>
        /// Sets the language.
        /// </summary>
        public void SetLanguage()
        {
            CultureInfo = DatabaseAccess.GetCultureInfo();
            HandleTranslation(CultureInfo);
        }

        /// <summary>
        /// Handles the translation.
        /// </summary>
        /// <param name="cultureInfo">The culture information.</param>
        public void HandleTranslation(string cultureInfo)
        {
            //LblConfig = Localize.GetString("LblConfig", cultureInfo);
            LblLogin.Text = Localize.GetString(Constants.login, cultureInfo);
            TxtUsername.Placeholder = Localize.GetString(Constants.login, cultureInfo);

            LblPassword.Text = Localize.GetString(Constants.password, cultureInfo);
            TxtPassword.Placeholder = Localize.GetString(Constants.password, cultureInfo);

            LblLanguage.Text = Localize.GetString(Constants.language, cultureInfo);
            LanguagePicker.Title = Localize.GetString(Constants.language, cultureInfo);

           BtnPasswordLost.Text = Localize.GetString(Constants.passwordLost, cultureInfo);

            LoginBtn.Text = Localize.GetString(Constants.save, cultureInfo);
            LblConfig.Text = Localize.GetString(Constants.configuration, cultureInfo);

            #region DisplayAlert

            _netInfo = Localize.GetString(Constants.NetInfo,cultureInfo);
            _netNotAvailable = Localize.GetString(Constants.NetNotAvailable, cultureInfo);
            _ok = Localize.GetString(Constants.ok, cultureInfo);
            _loginMsg = Localize.GetString(Constants.LoginMsg, cultureInfo);
            _loginError = Localize.GetString(Constants.LoginError, cultureInfo);
            
            #endregion

        }

        #region Footer Navigations

        /// <summary>
        /// Handles the Clicked event of the BtnDoc control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnDoc_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new DocumentsPage());
         
        }

        /// <summary>
        /// Handles the Clicked event of the BtnComm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnComm_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new NotificationCommunicationPage());

        }

        /// <summary>
        /// Handles the Clicked event of the BtnHelp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnHelp_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new WebPage());
        }

        /// <summary>
        /// Handles the Clicked event of the BtnConfig control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnConfig_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new ConfigurationPage());

        }

        /// <summary>
        /// Handles the Clicked event of the BtnHome control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnHome_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }

        #endregion

        private async void BtnPasswordLost_Clicked(object sender, EventArgs e)
        {
            var clientKey = DatabaseAccess.GetClientKey();
            if (clientKey != null)
            {
                await DisplayAlert("Sending..", "Sending request", "ok");
              await LoginService.PostPasswordLost(clientKey);

            }
            else
            {
                await DisplayAlert("Error","No user Found","ok");
            }
        }
    }
}