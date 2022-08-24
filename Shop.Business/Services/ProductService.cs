using System.Collections.Generic;
using System.Linq;
using Shop.Business.Mapper;
using Shop.Business.Models;
using Shop.Domain.Entities;
using Shop.NHibernate.Repositories;

namespace Shop.Business.Services
{
    public interface IProductService
    {
        void Add(ProductViewModel product);
        void Update(ProductViewModel product);
        void Delete(int id);
        ProductViewModel GetById(int id);
        IEnumerable<ProductViewModel> GetAll();
    }
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public void Add(ProductViewModel product)
        {
            _unitOfWork.BeginTransaction();

            var category = _categoryRepository.GetById(product.CategoryId);
            _productRepository.Add(new Product(product.Id, product.Title, product.Status, product.Description, product.Orders, category));// new Category(product.Category.Title, product.Category.Status, product.Category.IsDefault)));

            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _unitOfWork.BeginTransaction();

            try
            {
                _productRepository.Delete(_productRepository.GetById(id));
            }
            catch
            {
                System.Console.ForegroundColor = System.ConsoleColor.Green;
                System.Console.WriteLine("Ne postoo produkt so taj ID");
                System.Console.ForegroundColor = System.ConsoleColor.White;
                _unitOfWork.Commit();
                return;
            }

            _unitOfWork.Commit();
        }

        public void Update(ProductViewModel product)
        {
            _unitOfWork.BeginTransaction();

            var originalProduct = _productRepository.GetById(product.Id);

            originalProduct.Description = product.Description;
            //originalProduct.Category = product.Category;
            originalProduct.Category = _categoryRepository.GetById(product.CategoryId);
            originalProduct.Status = product.Status;
            originalProduct.Title = product.Title;

            _productRepository.Update(originalProduct);

            _unitOfWork.Commit();
        }

        public ProductViewModel GetById(int id)
        {
            _unitOfWork.BeginTransaction();

            var product = _productRepository.GetById(id)?.MapToViewModel();
            product.CategoryId = product.Category.Id;

            _unitOfWork.Commit();

            return product;
        }

        public IEnumerable<ProductViewModel> GetAll()
        {
            _unitOfWork.BeginTransaction();

            var allProducts = _productRepository.GetAll()?.Select(x => x.MapToViewModel());

            _unitOfWork.Commit();

            return allProducts;
        }

    }
}
