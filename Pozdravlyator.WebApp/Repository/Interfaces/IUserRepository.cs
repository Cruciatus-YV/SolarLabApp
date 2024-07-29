using Pozdravlyator.Domain;

namespace Pozdravlyator.WebApp.Repository.Interfaces
{
    public interface IUserRepository
    {
        User? GetById(int id);
        List<User> GetAll();
        void Create(User user);
        void Update(User user);
        void Delete(int id);
        
    }
}
