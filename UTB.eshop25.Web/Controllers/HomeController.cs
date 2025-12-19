using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UTB.eshop25.Application.Abstraction;
using UTB.eshop25.Domain.Entities;
using UTB.eshop25.Web.Helpers;
using UTB.eshop25.Web.Models;
using UTB.eshop25.Web.Models.Cart;

namespace UTB.eshop25.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductAppService _products;

        public HomeController(
            ILogger<HomeController> logger,
            IProductAppService products)
        {
            _logger = logger;
            _products = products;
        }

        public IActionResult Index()
        {
            var products = _products.SelectAll();
            return View(products);
        }

        public IActionResult AddToCart(int id)
        {
            var product = _products.Select(id);
            if (product == null)
                return NotFound();

            var cart = HttpContext.Session.Get<List<CartItem>>("cart") ?? new();

            var item = cart.FirstOrDefault(x => x.ProductId == id);
            if (item == null)
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Amount = 1
                });
            }
            else
            {
                item.Amount++;
            }

            HttpContext.Session.Set("cart", cart);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
