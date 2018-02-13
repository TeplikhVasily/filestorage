using DbStorage.Models;
using NHibernate;
using NHibernate.Cfg;
using System.Web;

namespace DbStorage.NHibernate
{
    public class Helper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var configuration = new Configuration();
                    configuration.Configure();

                    configuration.AddAssembly(typeof(User).Assembly);

                    _sessionFactory = configuration.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
