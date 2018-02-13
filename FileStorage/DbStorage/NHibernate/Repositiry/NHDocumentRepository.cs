using DbStorage.Models;
using DbStorage.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbStorage.NHibernate.Repositiry
{
    public class NHDocumentRepository : NHBaseRepository<Document>, IDocumentRepository
    {
        public IEnumerable<Document> GetByAutor(string name)
        {
            using (var session = Helper.OpenSession())
            {
                return session.QueryOver<Document>()
                    .Where(it => it.Author.LastName == name)
                    .List();
            };
        }

        public IEnumerable<Document> GetByName(string name)
        {
            using (var session = Helper.OpenSession())
            {
                return session.QueryOver<Document>()
                    .Where(it => it.Name == name)   
                    .List();
            };
        }

        public Document GetOrCreate(string documentName)
        {
            using (var session = Helper.OpenSession())
            {
                var document = session.QueryOver<Document>()
                    .And(it => it.Name == documentName)
                    .SingleOrDefault();

                if (document != null)
                    return document;

                document = new Document()
                {
                    Name = documentName,                   
                };
                Save(document);

                return document;
            }
        }
    }
}
