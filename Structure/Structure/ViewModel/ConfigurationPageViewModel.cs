using Structure.Model;
using System.Collections.ObjectModel;


namespace Structure.ViewModel
{
    public class ConfigurationPageViewModel :BaseViewModel
    {
        private ObservableCollection<LanguageModel> _languages;
        public ObservableCollection<LanguageModel> Languages
        {
            get { return _languages; }
            set { SetValue(ref _languages, value); }
        }

        public ConfigurationPageViewModel()
        {
            ShowLanguages();
        }
      
        public void ShowLanguages()
        {
            Languages = new ObservableCollection<LanguageModel>();

            var l1 = new LanguageModel()
            {
                Id = 1,
                Name = "English",
                Culture = "en-US",
            };
            var l2 = new LanguageModel()
            {
                Id = 2,
                Name = "French",
                Culture = "fr-FR"
            };
            var l3 = new LanguageModel()
            {
                Id = 3,
                Name = "Spanish",
                Culture = "es-ES"
            };
            var l4 = new LanguageModel()
            {
                Id = 4,
                Name = "German",
                Culture = "de-DE"
            };
            Languages.Add(l1);
            Languages.Add(l2);
            Languages.Add(l3);
            Languages.Add(l4);

        }

    }
}