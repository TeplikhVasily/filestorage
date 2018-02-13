using DbStorage.Models;
using DbStorage.Repository;
using NHibernate;
using System.Collections.Generic;

namespace DbStorage.NHibernate.Repositiry
{
    public class NHBaseRepository<T> : IRepository<T>
        where T : class, IEntity
    {
        public void Delete(long Id)
        {
            using (var session = Helper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var obj = session.Get<T>(Id);
                    if (obj != null)
                    {
                        session.Delete(obj);
                        transaction.Commit();
                    }
                }
            }
        }

        public T Get(long Id)
        {
            using (var session = Helper.OpenSession())
            {
                return session.Get<T>(Id);
            }
        }

        public IList<T> GetAll()
        {
            using (var session = Helper.OpenSession())
            {
                return session.QueryOver<T>().List();

            }
        }

        public void Save(T result)
        {
            using (var session = Helper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(result);
                    transaction.Commit();
                }
            }
        }
    }
}
