using Pozdravlyator.Domain;
using Pozdravlyator.WebApp.Repository.Interfaces;

namespace Pozdravlyator.WebApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public User? GetById(int id)
        {
            return _appDbContext.Users.FirstOrDefault(x => x.Id == id);
        }

        public List<User> GetAll()
        {
            return _appDbContext.Users.OrderBy(x=>x.Birthday.Month).ToList();
        }

        public void Create(User user)
        {
            _appDbContext.Users.Add(user);
            _appDbContext.SaveChanges();        
        }

        public void Update(User user)
        {
            _appDbContext.Users.Update(user);
            _appDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            _appDbContext.Users.Remove(_appDbContext.Users.FirstOrDefault(x => x.Id == id));
            _appDbContext.SaveChanges();
        }
    }
}
