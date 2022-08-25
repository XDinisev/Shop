using System;
using System.Collections.Generic;
using System.Linq;
using Shop.Business.Mapper;
using Shop.Business.Models;
using Shop.Domain.Entities;
using Shop.NHibernate.Repositories;

namespace Shop.Business.Services
{
    public interface ICategoryService
    {
        void Add(CategoryViewModel category);
        void Update(CategoryViewModel category);
        void Delete(int id);
        CategoryViewModel GetById(int id);
        IEnumerable<CategoryViewModel> GetAll();
        IEnumerable<ProductViewModel> GetProducts(int id);
    }
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(ICategoryRepository repository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = repository;
            _unitOfWork = unitOfWork;
        }
        public void Add(CategoryViewModel category)
        {
            _unitOfWork.BeginTransaction();

            _categoryRepository.Add(new Category(category.Title, category.Status, category.IsDefault));

            _unitOfWork.Commit();
        }
        public void Delete(int id)
        {
            _unitOfWork.BeginTransaction();

            var category = _categoryRepository.GetById(id);
            if (category.Products.Count == 0)
                _categoryRepository.Delete(category);

            _unitOfWork.Commit();
        }
        public void Update(CategoryViewModel category)
        {
            _unitOfWork.BeginTransaction();

            var originalCategory = _categoryRepository.GetById(category.Id);

            originalCategory.Title = category.Title;
            originalCategory.Status = category.Status;
            originalCategory.IsDefault = category.IsDefault;

            _categoryRepository.Update(originalCategory);

            _unitOfWork.Commit();
        }
        public CategoryViewModel GetById(int id)
        {
            _unitOfWork.BeginTransaction();

            var category = _categoryRepository.GetById(id)?.MapToViewModel();

            _unitOfWork.Commit();

            return category;
        }
        public IEnumerable<CategoryViewModel> GetAll()
        {
            _unitOfWork.BeginTransaction();

            var allCategories = _categoryRepository.GetAll()?.Select(x => x.MapToViewModel());

            _unitOfWork.Commit();

            return allCategories;
        }

        public IEnumerable<ProductViewModel> GetProducts(int id)
        {
            throw new NotImplementedException();
        }
    }
}
