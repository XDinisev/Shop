using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Business.Models;
using Shop.Business.Services;
using System.Linq;

namespace Shop.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        // GET: CustomerController
        public ActionResult Index(string sortOrder, string firstNameSearchString, string lastNameSearchString, string usernameSearchString, int? page)
        {
            var customers = _customerService.GetAll().Where(x => x.FirstName.Contains(string.IsNullOrEmpty(firstNameSearchString) ? "" : firstNameSearchString) && x.LastName.Contains(string.IsNullOrEmpty(lastNameSearchString) ? "" : lastNameSearchString) && x.Username.Contains(string.IsNullOrEmpty(usernameSearchString) ? "" : usernameSearchString));

            ViewBag.FirstNameSortToggle = string.IsNullOrEmpty(sortOrder) ? "firstName" : "";
            ViewBag.LastNameSortToggle = sortOrder == "lastName" ? "lastNameDesc" : "lastName";
            ViewBag.UsernameSortToggle = sortOrder == "username" ? "usernameDesc" : "username";
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FirstNameSearchString = firstNameSearchString;
            ViewBag.LastNameSearchString = lastNameSearchString;
            ViewBag.UsernameSearchString = usernameSearchString;

            switch (sortOrder)
            {
                case "lastName":
                    customers = customers.OrderBy(x => x.LastName);
                    break;
                case "lastNameDesc":
                    customers = customers.OrderByDescending(x => x.LastName);
                    break;
                case "username":
                    customers = customers.OrderBy(x => x.Username);
                    break;
                case "usernameDesc":
                    customers = customers.OrderByDescending(x => x.Username);
                    break;
                case "firstName":
                    customers = customers.OrderBy(x => x.FirstName);
                    break;
                default:
                    customers = customers.OrderByDescending(x => x.FirstName);
                    break;
            }

            var numberOfCustomers = customers.Count();

            var pageSize = 10;
            var pageNumber = (page ?? 0);
            customers = customers.Skip(pageSize * pageNumber).Take(pageSize);

            ViewBag.CurrentPage = pageNumber;
            if (pageNumber > 0)
            {
                ViewBag.PreviousPage = pageNumber - 1;
                ViewBag.FirstPage = false;
            }
            else
            {
                ViewBag.PreviousPage = 0;
                ViewBag.FirstPage = true;
            }

            if (pageSize * (pageNumber + 1) < numberOfCustomers)
            {
                ViewBag.NextPage = pageNumber + 1;
                ViewBag.LastPage = false;
            }
            else
            {
                ViewBag.NextPage = pageNumber;
                ViewBag.LastPage = true;
            }

            return View(customers);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            return View(_customerService.GetById(id));
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerViewModel customer, IFormCollection collection)
        {
            if (ModelState.IsValid)
                _customerService.Add(customer);
            return RedirectToAction("Index");
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_customerService.GetById(id));
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerViewModel customer, IFormCollection collection)
        {
            if (ModelState.IsValid)
                _customerService.Update(customer);
            return RedirectToAction("Details", new { id = customer.Id });
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_customerService.GetById(id));
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            _customerService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
