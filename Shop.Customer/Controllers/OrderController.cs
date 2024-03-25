using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Business.Models;
using Shop.Business.Services;
using Shop.Domain.Entities;
using System.Linq;

namespace Shop.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        public OrderController(IOrderService orderService, IProductService productService, ICustomerService customerService)
        {
            _orderService = orderService;
            _productService = productService;
            _customerService = customerService;
        }
        // GET: OrderController
        public ActionResult Index(string sortOrder, string showComplete, string productSearchString, string customerSearchString, int? page)
        {
            var orders = _orderService.GetAll().Where(x => x.Product.Title.Contains(string.IsNullOrEmpty(productSearchString) ? "" : productSearchString) && x.Customer.Username.Contains(string.IsNullOrEmpty(customerSearchString) ? "" : customerSearchString));

            ViewBag.CustomerSortToggle = string.IsNullOrEmpty(sortOrder) ? "custDesc" : "";
            ViewBag.ProductSortToggle = sortOrder == "prod" ? "prodDesc" : "prod";
            ViewBag.QuantitySortToggle = sortOrder == "qty" ? "qtyDesc" : "qty";
            ViewBag.ShowCompleteToggle = string.IsNullOrEmpty(showComplete) ? "yes" : "";
            ViewBag.CurrentSort = sortOrder;
            ViewBag.ShowComplete = showComplete;
            ViewBag.ProductSearchString = productSearchString;
            ViewBag.CustomerSearchString = customerSearchString;

            switch (showComplete)
            {
                case "yes":
                    break;
                default:
                    orders = orders.Where(x => x.Status != OrderStatus.Complete);
                    break;
            }
            switch (sortOrder)
            {
                case "prod":
                    orders = orders.OrderBy(x => x.Product.Title);
                    break;
                case "prodDesc":
                    orders = orders.OrderByDescending(x => x.Product.Title);
                    break;
                case "qty":
                    orders = orders.OrderBy(x => x.Quantity);
                    break;
                case "qtyDesc":
                    orders = orders.OrderByDescending(x => x.Quantity);
                    break;
                case "cust":
                    orders = orders.OrderBy(x => x.Customer.Username);
                    break;
                default:
                    orders = orders.OrderByDescending(x => x.Customer.Username);
                    break;
            }

            var numberOfOrders = orders.Count();

            var pageSize = 10;
            var pageNumber = (page ?? 0);
            orders = orders.Skip(pageSize * pageNumber).Take(pageSize);

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

            if (pageSize * (pageNumber + 1) < numberOfOrders)
            {
                ViewBag.NextPage = pageNumber + 1;
                ViewBag.LastPage = false;
            }
            else
            {
                ViewBag.NextPage = pageNumber;
                ViewBag.LastPage = true;
            }


            return View(orders);
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            ViewData["Products"] =  new SelectList(_productService.GetAll().Where(x => x.Status==ProductStatus.Availiable).OrderBy(x => x.Title), "Id", "Title");
            ViewData["Customers"] = new SelectList(_customerService.GetAll().OrderBy(x => x.FullName), "Id", "FullName");
            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderViewModel order, IFormCollection collection)
        {
            if (ModelState.IsValid)
                _orderService.Add(order);
            return RedirectToAction("Index");
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            return View(_orderService.GetById(id));
        }

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            var order = _orderService.GetById(id);
            ViewData["Products"] = new SelectList(_productService.GetAll().Where(x => x.Status == ProductStatus.Availiable).OrderBy(x => x.Title), "Id", "Title", order.ProductId);
            ViewData["Customers"] = new SelectList(_customerService.GetAll().OrderBy(x => x.FullName), "Id", "FullName", order.CustomerId);
            return View(order);
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderViewModel order, IFormCollection collection)
        {
            if (ModelState.IsValid)
                _orderService.Update(order);
            return RedirectToAction("Details", new { id = order.Id });
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_orderService.GetById(id));
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            _orderService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
