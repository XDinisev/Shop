using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Business.Models;
using Shop.Business.Services;

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
        public ActionResult Index()
        {
            return View(_productService.GetAll());
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View(_productService.GetById(id));
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            ViewData["Categories"] = new SelectList(_categoryService.GetAll(), "Id", "Title");
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
            ViewData["Categories"] = new SelectList(_categoryService.GetAll(), "Id", "Title", product.CategoryId);
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
    }
}
