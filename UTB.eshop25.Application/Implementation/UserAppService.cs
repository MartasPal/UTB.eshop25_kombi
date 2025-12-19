using UTB.eshop25.Application.Abstraction;
using UTB.eshop25.Domain.Entities;
using UTB.eshop25.Infrastructure.Database;

namespace UTB.eshop25.Application.Implementation
{
    public class UserAppService : IUserAppService
    {
        private readonly EshopDbContext _db;

        public UserAppService(EshopDbContext db)
        {
            _db = db;
        }

        public IList<User> SelectAll()
            => _db.Users.OrderByDescending(u => u.Id).ToList();

        public void Create(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }

        public bool Delete(int id)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return false;

            _db.Users.Remove(user);
            _db.SaveChanges();
            return true;
        }
    }
}
