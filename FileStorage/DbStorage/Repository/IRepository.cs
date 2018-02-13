using DbStorage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbStorage.Repository
{
    public interface IRepository<T>
        where T : IEntity 
    {
        //CRUD

        T Get(long Id);

        void Save(T result);

        void Delete(long Id);

        IList<T> GetAll();

    }
}
