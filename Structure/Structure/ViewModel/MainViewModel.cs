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
    public class MainViewModel :BaseViewModel,IPageNotification
    {
        ObservableCollection<Grouping<SelectCategoryViewModel, Document>> _Categories;

        private string _ExtentedImageValue;
        private int _count;

        public string ExtentedImageValue
        {
            get { return _ExtentedImageValue; }
            set { SetValue(ref _ExtentedImageValue, value); }
        }

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


        public int NewComCount
        {
            get { return _count; }
            set
            {
                if(_count!=value)
                {
                    _count = value;
                    OnPropertyChanged();
                }
            }
          
           
        }

        public MainViewModel()
        {
          
            DatabaseAccess = new DatabaseAccess();
            DocumentService = new DocumentService();
            Categories = new ObservableCollection<Grouping<SelectCategoryViewModel, Document>>();
            ExtentedImageValue = "collapsed_blue.png";


        }
        public void ConstructList()
        {
            DataDocuments = new ObservableCollection<Document>(DatabaseAccess.ReadAllDocuments());
            Categories = new ObservableCollection<Grouping<SelectCategoryViewModel, Document>>();
            var selectCategories =
             DataDocuments.Select(x => new SelectCategoryViewModel {Category = x.Type, Selected = false })
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

                if (item.Type == "")
                {
                    item.Type = "No Type";
                }
                if (item.Name == "")
                {
                    item.Name = "No Name";
                }
                var filePath = await DependencyService.Get<IFileMgr>().Base64ToFile(item.Data, item.Name,item.Extension);
            
                item.FilePath = filePath;


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

