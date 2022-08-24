using System;
using System.Collections.Generic;
using System.Linq;
using Shop.Business.Mapper;
using Shop.Business.Models;
using Shop.Domain.Entities;
using Shop.NHibernate.Repositories;

namespace Shop.Business.Services
{
    public interface IOrderService
    {
        void Add(OrderViewModel order);
        void Update(OrderViewModel order);
        void Delete(int id);
        OrderViewModel GetById(int id);
        IEnumerable<OrderViewModel> GetAll();
    }
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IOrderRepository repository, IProductRepository productRepository, ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = repository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public void Add(OrderViewModel order)
        {
            _unitOfWork.BeginTransaction();

            _orderRepository.Add(new Order(order.Id, order.Quantity, DateTime.Now, OrderStatus.NotComplete, _customerRepository.GetById(order.CustomerId), _productRepository.GetById(order.ProductId)));

            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                _orderRepository.Delete(_orderRepository.GetById(id));
            }
            catch
            {
                System.Console.ForegroundColor = System.ConsoleColor.Green;
                System.Console.WriteLine("Ne postoo poracka so taj ID");
                System.Console.ForegroundColor = System.ConsoleColor.White;
                _unitOfWork.Commit();
                return;
            }

            _unitOfWork.Commit();
        }

        public void Update(OrderViewModel order)
        {
            _unitOfWork.BeginTransaction();

            var originalOrder = _orderRepository.GetById(order.Id);

            originalOrder.Quantity = order.Quantity;
            originalOrder.Status = order.Status;
            originalOrder.Customer = _customerRepository.GetById(order.CustomerId);
            originalOrder.Product = _productRepository.GetById(order.ProductId);

            _orderRepository.Update(originalOrder);

            _unitOfWork.Commit();
        }


        public OrderViewModel GetById(int id)
        {
            _unitOfWork.BeginTransaction();

            var order = _orderRepository.GetById(id)?.MapToViewModel();

            _unitOfWork.Commit();

            return order;
        }
        public IEnumerable<OrderViewModel> GetAll()
        {
            _unitOfWork.BeginTransaction();

            var allOrders = _orderRepository.GetAll()?.Select(x => x.MapToViewModel());

            _unitOfWork.Commit();
            
            return allOrders;
        }
    }
}
