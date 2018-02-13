using DbStorage.Models;
using System.Collections.Generic;

namespace DbStorage.Repository
{
    public interface IDocumentRepository : IRepository<Document>
    {
        IEnumerable<Document> GetByName(string name);

        IEnumerable<Document> GetByAutor(string name);

        Document GetOrCreate(string name);

    }
}
