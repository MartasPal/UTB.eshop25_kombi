using Microsoft.AspNetCore.Mvc;
using UTB.eshop25.Application.Abstraction;
using UTB.eshop25.Web.Helpers;
using UTB.eshop25.Web.Models.Cart;

namespace UTB.eshop25.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductAppService _products;
        private readonly IOrderAppService _orders;

        public CartController(IProductAppService products, IOrderAppService orders)
        {
            _products = products;
            _orders = orders;
        }

        [HttpPost]
        public IActionResult AddToCartAjax(int id)
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("cart")
           ?? new List<CartItem>();

            var item = cart.FirstOrDefault(i => i.ProductId == id);

            if (item != null)
            {
                item.Amount++;
            }
            else
            {
                var product = _products.Select(id);
                if (product == null)
                    return NotFound();

                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Amount = 1
                });
            }

            HttpContext.Session.Set("cart", cart);

            int count = cart.Sum(i => i.Amount);
            return Json(count);
        }

        public IActionResult Index()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("cart")
                       ?? new List<CartItem>();

            return View(cart);
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Remove("cart");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("cart") ?? new();
            if (!cart.Any())
                return RedirectToAction(nameof(Index));

            var vm = new UTB.eshop25.Web.Models.OrderCheckoutVM
            {
                Items = cart
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(UTB.eshop25.Web.Models.OrderCheckoutVM vm)
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("cart") ?? new();
            if (!cart.Any())
                return RedirectToAction(nameof(Index));

            if (!ModelState.IsValid)
            {
                vm.Items = cart;
                return View(vm);
            }

     
            int userId = 0; 
            var items = cart.Select(c => (c.ProductId, c.Amount));

            int orderId = _orders.Create(vm.CustomerName, vm.Address, items);

            HttpContext.Session.Remove("cart");

            return RedirectToAction(nameof(CheckoutDone), new { id = orderId });
        }

        public IActionResult CheckoutDone(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }

    }
}
