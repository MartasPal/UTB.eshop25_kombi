using UTB.eshop25.Domain.Entities;

namespace UTB.eshop25.Application.Abstraction
{
    public interface IOrderAppService
    {
        int Create(string customerName, string address, IEnumerable<(int productId, int amount)> items);
        int Create(int userId, IEnumerable<(int productId, int amount)> items);

        Order? Get(int id);

        IList<Order> GetAll();

    }
}
