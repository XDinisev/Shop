using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Business.Models;
using Shop.Business.Services;
using Shop.Domain.Entities;
using System.Linq;

namespace Shop.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index(string sortOrder, string showDisabled)
        {
            var categories = _categoryService.GetAll();
            ViewBag.NameSortToggle = string.IsNullOrEmpty(sortOrder) ? "nameDesc" : "";
            ViewBag.ShowDisabledToggle = string.IsNullOrEmpty(showDisabled) ? "yes" : "";
            ViewBag.CurrentSort = sortOrder;
            ViewBag.ShowDisabled = showDisabled;
            switch(showDisabled)
            {
                case "yes":
                    break;
                default:
                    categories = categories.Where(x => x.Status != CategoryStatus.Disabled);
                    break;
            }
            switch (sortOrder)
            {
                case "nameDesc":
                    categories = categories.OrderByDescending(x => x.Title);
                    break;
                default:
                    categories = categories.OrderBy(x => x.Title);
                    break;
            }
            return View(categories);
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