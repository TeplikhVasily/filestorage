using DbStorage.Models;

namespace DbStorage.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByLogin(string login);

        bool Check(string login, string password);

    }
}
