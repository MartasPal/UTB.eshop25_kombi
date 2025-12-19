using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTB.eshop25.Domain.Entities;

namespace UTB.eshop25.Application.Abstraction
{
    public interface IProductAppService
    {
        IList<Product> SelectAll();
        void Create(Product product);
        Product? Select(int id);
        bool Delete(int id);
    }
}
