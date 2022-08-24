using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Business.Models;
using Shop.Business.Services;

namespace Shop.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View(_categoryService.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryViewModel category)
        {
            if (ModelState.IsValid)
                _categoryService.Add(category);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            return View(_categoryService.GetById(id));
        }

        [HttpGet]
        public IActionResult ListProducts(int id)
        {
            return View(_categoryService.GetById(id));
        }

        public IActionResult Edit(int id)
        {
            return View(_categoryService.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryViewModel category)
        {
            if (ModelState.IsValid)
                _categoryService.Update(category);
            return RedirectToAction("Details", new { id = category.Id });
        }

        public IActionResult Delete(int id)
        {
            return View(_categoryService.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            _categoryService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}