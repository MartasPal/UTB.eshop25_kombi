using Microsoft.AspNetCore.Mvc;
using UTB.eshop25.Application.Abstraction;
using UTB.eshop25.Domain.Entities;

namespace UTB.eshop25.Web.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class UserController : Controller
    {
        private readonly IUserAppService _users;

        public UserController(IUserAppService users)
        {
            _users = users;
        }

        public IActionResult Select()
        {
            var users = _users.SelectAll();
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _users.Create(user);
                return RedirectToAction(nameof(Select));
            }

            return View(user);
        }

        public IActionResult Delete(int id)
        {
            bool deleted = _users.Delete(id);
            if (deleted)
                return RedirectToAction(nameof(Select));

            return NotFound();
        }
    }
}
