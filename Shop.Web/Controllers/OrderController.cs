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
        public ActionResult Index(string sortOrder, string showComplete)
        {
            var orders = _orderService.GetAll();

            ViewBag.CustomerSortToggle = string.IsNullOrEmpty(sortOrder) ? "custDesc" : "";
            ViewBag.ProductSortToggle = sortOrder == "prod" ? "prodDesc" : "prod";
            ViewBag.QuantitySortToggle = sortOrder == "qty" ? "qtyDesc" : "qty";
            ViewBag.ShowCompleteToggle = string.IsNullOrEmpty(showComplete) ? "yes" : "";
            ViewBag.CurrentSort = sortOrder;
            ViewBag.ShowComplete = showComplete;


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

            return View(orders);
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            ViewData["Products"] = new SelectList(_productService.GetAll(), "Id", "Title");
            ViewData["Customers"] = new SelectList(_customerService.GetAll(), "Id", "Username");
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
            ViewData["Products"] = new SelectList(_productService.GetAll(), "Id", "Title", order.ProductId);
            ViewData["Customers"] = new SelectList(_customerService.GetAll(), "Id", "Username", order.CustomerId);
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
