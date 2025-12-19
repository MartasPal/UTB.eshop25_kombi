using Microsoft.AspNetCore.Mvc;
using UTB.eshop25.Application.Abstraction;

namespace UTB.eshop25.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class OrderController : Controller
    {
        private readonly IOrderAppService _orders;

        public OrderController(IOrderAppService orders)
        {
            _orders = orders;
        }

        public IActionResult Select()
        {
            var orders = _orders.GetAll();
            return View(orders);
        }

        public IActionResult Detail(int id)
        {
            var order = _orders.Get(id);
            if (order == null) return NotFound();

            return View(order);
        }
    }
}
