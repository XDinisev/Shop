using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Business.Models;
using Shop.Business.Services;
using Shop.Domain.Entities;
using System.Collections.Generic;
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

        public IActionResult Index(string sortOrder, string showDisabled, string titleSearchString, int? page)
        {
            var categories = _categoryService.GetAll().Where(x => x.Title.Contains(string.IsNullOrEmpty(titleSearchString) ? "" : titleSearchString));

            ViewBag.NameSortToggle = string.IsNullOrEmpty(sortOrder) ? "nameDesc" : "";
            ViewBag.ShowDisabledToggle = string.IsNullOrEmpty(showDisabled) ? "yes" : "";
            ViewBag.CurrentSort = sortOrder;
            ViewBag.ShowDisabled = showDisabled;
            ViewBag.TitleSearchString = titleSearchString;
            page = page ?? 0;
            switch (showDisabled)
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

            var numberOfCategories = categories.Count();

            var pageSize = 10;
            var pageNumber = (page ?? 0);
            categories = categories.Skip(pageSize * pageNumber).Take(pageSize);

            ViewBag.CurrentPage = pageNumber;
            if(pageNumber>0)
            {
                ViewBag.PreviousPage = pageNumber-1;
                ViewBag.FirstPage = false;
            }
            else
            {
                ViewBag.PreviousPage = 0;
                ViewBag.FirstPage = true;
            }

            if(pageSize * (pageNumber + 1) < numberOfCategories)
            {
                ViewBag.NextPage = pageNumber + 1;
                ViewBag.LastPage = false;
            }
            else
            {
                ViewBag.NextPage = pageNumber;
                ViewBag.LastPage = true;
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