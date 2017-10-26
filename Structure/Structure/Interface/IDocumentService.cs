using Structure.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structure.Interface
{
    public interface IDocumentService
    {
        Task<bool> PostDocument(Document document, string clientKey);

        Task<ObservableCollection<Document>> GetDocuments(string ClientKey);
    }
}
