using Microsoft.EntityFrameworkCore;
using UTB.eshop25.Application.Abstraction;
using UTB.eshop25.Domain.Entities;
using UTB.eshop25.Infrastructure.Database;

namespace UTB.eshop25.Application.Implementation
{
    public class OrderAppService : IOrderAppService
    {
        private readonly EshopDbContext _db;

        public OrderAppService(EshopDbContext db)
        {
            _db = db;
        }

        public int Create(string customerName, string address, IEnumerable<(int productId, int amount)> items)
        {

            var user = new User
            {
                Name = customerName,
                Address = address
            };

            _db.Users.Add(user);
            _db.SaveChanges();


            return Create(user.Id, items);
        }

        public int Create(int userId, IEnumerable<(int productId, int amount)> items)
        {
            var itemList = items.ToList();
            if (itemList.Count == 0)
                throw new ArgumentException("Order must contain at least one item.");


            var user = _db.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                throw new InvalidOperationException("User was not found.");

            var productIds = itemList.Select(i => i.productId).Distinct().ToList();

            var products = _db.Products
                .Where(p => productIds.Contains(p.Id))
                .ToList();

            if (products.Count != productIds.Count)
                throw new InvalidOperationException("Some products were not found.");

            var order = new Order
            {
                UserId = userId,
                CustomerName = user.Name,    
                Address = user.Address,      
                OrderNumber = Guid.NewGuid().ToString("N").ToUpper(),
                TotalPrice = 0
            };

            foreach (var (productId, amount) in itemList)
            {
                if (amount <= 0) continue;

                var product = products.Single(p => p.Id == productId);

                order.OrderItems.Add(new OrderItem
                {
                    ProductID = product.Id,
                    Amount = amount,
                    Price = product.Price
                });
            }

            order.TotalPrice = order.OrderItems.Sum(i => i.Price * i.Amount);

            _db.Orders.Add(order);
            _db.SaveChanges();

            return order.Id;
        }

        public Order? Get(int id)
        {
            return _db.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.Id == id);
        }

        public IList<Order> GetAll()
        {
            return _db.Orders
                .OrderByDescending(o => o.Id)
                .ToList();
        }
    }
}
