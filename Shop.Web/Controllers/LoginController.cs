using Microsoft.AspNetCore.Mvc;
using Shop.Business.Models;
using Shop.Business.Services;

namespace Shop.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ICustomerService _customerService;
        public LoginController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login(CustomerViewModel user)
        {
            var customer = _customerService.Authorise(user.Username, user.Password);

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Logout()
        {
            _customerService.Logout();

            return RedirectToAction("Index", "Home");
        }
    }
}