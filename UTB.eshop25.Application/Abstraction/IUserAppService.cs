using UTB.eshop25.Domain.Entities;

namespace UTB.eshop25.Application.Abstraction
{
    public interface IUserAppService
    {
        IList<User> SelectAll();
        void Create(User user);
        bool Delete(int id);
    }
}
