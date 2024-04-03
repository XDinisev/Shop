using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Business.Models;
using Shop.Business.Services;
using Shop.Domain.Entities;

namespace Shop.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        // GET: ProductController
        public ActionResult Index(string sortOrder, string showUnavailiable, string titleSearchString, string categorySearchString, int? page)
        {
            var products = _productService.GetAll().Where(x => x.Title.Contains(string.IsNullOrEmpty(titleSearchString) ? "" : titleSearchString) &&
                                                          x.Category.Title.Contains(string.IsNullOrEmpty(categorySearchString) ? "" : categorySearchString) &&
                                                          x.Category.Status==CategoryStatus.Enabled);

            ViewBag.NameSortToggle = string.IsNullOrEmpty(sortOrder) ? "nameDesc" : "";
            ViewBag.CategorySortToggle = sortOrder == "cat" ? "catDesc" : "cat";
            ViewBag.ShowUnavailiableToggle = string.IsNullOrEmpty(showUnavailiable) ? "yes" : "";
            ViewBag.CurrentSort = sortOrder;
            ViewBag.ShowUnavailiable = showUnavailiable;
            ViewBag.TitleSearchString = titleSearchString;
            ViewBag.CategorySearchString = categorySearchString;

            switch (showUnavailiable)
            {
                case "yes":
                    break;
                default:
                    products = products.Where(x => x.Status != ProductStatus.NotAvailiable);
                    break;
            }
            switch (sortOrder)
            {
                case "cat":
                    products = products.OrderBy(x => x.Category.Title);
                    break;
                case "catDesc":
                    products = products.OrderByDescending(x => x.Category.Title);
                    break;
                case "nameDesc":
                    products = products.OrderByDescending(x => x.Title);
                    break;
                default:
                    products = products.OrderBy(x => x.Title);
                    break;
            }

            var numberOfProducts = products.Count();

            var pageSize = 100;
            var pageNumber = (page ?? 0);
            products = products.Skip(pageSize * pageNumber).Take(pageSize);

            ViewBag.CurrentPage = pageNumber;
            if (pageSize * (pageNumber + 1) < numberOfProducts)
            {
                ViewBag.NextPage = pageNumber + 1;
                ViewBag.LastPage = false;
            }
            else
            {
                ViewBag.NextPage = pageNumber;
                ViewBag.LastPage = true;
            }
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

            return View(products);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View(_productService.GetById(id));
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            ViewData["Categories"] = new SelectList(_categoryService.GetAll().Where(x => x.Status==CategoryStatus.Enabled).OrderBy(x => x.Title), "Id", "Title");
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel product)
        {
            if (ModelState.IsValid)
                _productService.Add(product);
            return RedirectToAction("Index");
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            var product = _productService.GetById(id);
            ViewData["Categories"] = new SelectList(_categoryService.GetAll().Where(x => x.Status == CategoryStatus.Enabled).OrderBy(x => x.Title), "Id", "Title", product.CategoryId);
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductViewModel product, IFormCollection collection)
        {
            if (ModelState.IsValid)
                _productService.Update(product);
            return RedirectToAction("Details", new { id = product.Id });
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_productService.GetById(id));
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            _productService.Delete(id);
            return RedirectToAction("Index");
        }

        public JsonResult GetAll(string sortOrder, string showUnavailiable, string titleSearchString, string categorySearchString, int? page)
        {
            var products = _productService.GetAll().Where(x => x.Title.Contains(string.IsNullOrEmpty(titleSearchString) ? "" : titleSearchString) &&
                                                          x.Category.Title.Contains(string.IsNullOrEmpty(categorySearchString) ? "" : categorySearchString) &&
                                                          x.Category.Status == CategoryStatus.Enabled);

            ViewBag.NameSortToggle = string.IsNullOrEmpty(sortOrder) ? "nameDesc" : "";
            ViewBag.CategorySortToggle = sortOrder == "cat" ? "catDesc" : "cat";
            ViewBag.ShowUnavailiableToggle = string.IsNullOrEmpty(showUnavailiable) ? "yes" : "";
            ViewBag.CurrentSort = sortOrder;
            ViewBag.ShowUnavailiable = showUnavailiable;
            ViewBag.TitleSearchString = titleSearchString;
            ViewBag.CategorySearchString = categorySearchString;

            switch (showUnavailiable)
            {
                case "yes":
                    break;
                default:
                    products = products.Where(x => x.Status != ProductStatus.NotAvailiable);
                    break;
            }
            switch (sortOrder)
            {
                case "cat":
                    products = products.OrderBy(x => x.Category.Title);
                    break;
                case "catDesc":
                    products = products.OrderByDescending(x => x.Category.Title);
                    break;
                case "nameDesc":
                    products = products.OrderByDescending(x => x.Title);
                    break;
                default:
                    products = products.OrderBy(x => x.Title);
                    break;
            }

            var numberOfProducts = products.Count();

            var pageSize = 10;
            var pageNumber = (page ?? 0);
            products = products.Skip(pageSize * pageNumber).Take(pageSize);

            ViewBag.CurrentPage = pageNumber;
            if (pageSize * (pageNumber + 1) < numberOfProducts)
            {
                ViewBag.NextPage = pageNumber + 1;
                ViewBag.LastPage = false;
            }
            else
            {
                ViewBag.NextPage = pageNumber;
                ViewBag.LastPage = true;
            }
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

            return Json(products);
        }
    }
}
