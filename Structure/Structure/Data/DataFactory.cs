using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Structure.Model;

namespace Structure.Data
{
    /// <summary>
    /// The purpose of this class to provide a list of data that you might get from e.g. a web service
    /// </summary>
    public static class DataFactory
    {
        public static IList<Document> DataDocuments { get; private set; }

        private static readonly Category Category1 = new Category {CategoryId = 1, CategoryTitle = "Category 1", };
        private static readonly Category Category2 = new Category {CategoryId = 2, CategoryTitle = "Category 2", };

        static DataFactory()
        {
            DataDocuments = new ObservableCollection<Document>()
            {
                new Document
                {
                    IdMobile = 1,
                    Name = "Document 1",
                    Type = Category1.CategoryTitle,
                    
                },
                new Document
                {
                    IdMobile = 2,
                    Name = "Document 2",
                    Type = Category1.CategoryTitle
                },
                new Document
                {
                    IdMobile = 3,
                    Name = "Document 3",
                    Type = Category2.CategoryTitle
                },
                new Document
                {
                    IdMobile = 4,
                    Name = "Document 4",
                    Type = Category2.CategoryTitle
                }
            };
        }
    }
}
