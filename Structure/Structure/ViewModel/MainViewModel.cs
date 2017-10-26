using Prism.Commands;
using Structure.Data;
using Structure.Interface;
using Structure.Model;
using Structure.Service;
using Structure.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Structure.ViewModel
{
    public class MainViewModel :BaseViewModel
    {
        ObservableCollection<Grouping<SelectCategoryViewModel, Document>> _Categories;
      
        public ObservableCollection<Document> DataDocuments{get; set;}
        public ObservableCollection<Grouping<SelectCategoryViewModel, Document>> Categories
        {
            get { return _Categories; }
            set { SetValue(ref _Categories, value); }
        }


        public DelegateCommand<Grouping<SelectCategoryViewModel, Document>> HeaderSelectedCommand
        {
            get
            {
                return new DelegateCommand<Grouping<SelectCategoryViewModel, Document>>(g =>
                {
                    if (g == null) return;
                    g.Key.Selected = !g.Key.Selected;
                    if (g.Key.Selected)
                    {

                       DataDocuments.Where(i => (i.Type == g.Key.Category))
                                      .ForEach(g.Add);

                    }
                    else
                    {
                        g.Clear();
                    }
                });
            }
        }
        public DatabaseAccess DatabaseAccess { get; set; }
        public DocumentService DocumentService { get; set; }

        public MainViewModel()
        {
          
            DatabaseAccess = new DatabaseAccess();
            DocumentService = new DocumentService();

           
        }
        public void ConstructList()
        {
            DataDocuments = new ObservableCollection<Document>(DatabaseAccess.ReadAllDocuments());
            Categories = new ObservableCollection<Grouping<SelectCategoryViewModel, Document>>();
            var selectCategories =
             DataDocuments.Select(x => new SelectCategoryViewModel { Category = x.Type, Selected = false })
                    .GroupBy(sc => new { sc.Category })
                    .Select(g => g.First())
                    .ToList();
            selectCategories.ForEach(sc => Categories.Add(new Grouping<SelectCategoryViewModel, Document>(sc, new List<Document>())));
        }
        public async Task<bool> DownloadAndSaveDocumentToDB()
        {
            List<Document> docsToAdd = new List<Document>();

            var clientKey = DatabaseAccess.GetClientKey();
            var documentList = await DocumentService.GetDocuments(clientKey);
            foreach (var item in documentList)
            {
                var filePath = await DependencyService.Get<IFileMgr>().Base64ToFile(item.Data, item.Name);
                var position = DependencyService.Get<ILocationService>().GetLocation();
                item.FilePath = filePath;
                item.Longitude = position.Longitude;
                item.Latitude = position.Latitude;

                docsToAdd.Add(item);
            }

            DatabaseAccess.CreateManyDocuments(docsToAdd);
            ConstructList();
            return true;
        }

    }
    public class SelectCategoryViewModel
    {
        public string Category { get; set; }
        public bool Selected { get; set; }
    }
}

