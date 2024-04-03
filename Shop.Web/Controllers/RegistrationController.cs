using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Business.Models;
using Shop.Business.Services;

namespace Shop.Web.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ICustomerService _customerService;
        public RegistrationController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(CustomerViewModel customer, IFormCollection collection)
        {
            customer.UserType = Domain.Entities.UserType.Internal;
            if (ModelState.IsValid)
                _customerService.Add(customer);
            return RedirectToAction("Index");
        }
    }
}