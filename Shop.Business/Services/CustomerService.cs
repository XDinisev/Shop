using System;
using System.Collections.Generic;
using System.Linq;
using Shop.Business.Mapper;
using Shop.Business.Models;
using Shop.Domain.Entities;
using Shop.NHibernate.Repositories;

namespace Shop.Business.Services
{
    public interface ICustomerService
    {
        void Add(CustomerViewModel customer);
        void Update(CustomerViewModel customer);
        void Delete(int id);
        bool Authorise(int id, string password);
        CustomerViewModel GetById(int id);
        IEnumerable<CustomerViewModel> GetAll();
    }
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public CustomerService(ICustomerRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void Add(CustomerViewModel customer)
        {
            _unitOfWork.BeginTransaction();

            if (_repository.GetAll().Where(x => x.Username == customer.Username || x.Email == customer.Email).Count() > 0)
            {
                System.Console.ForegroundColor = System.ConsoleColor.Green;
                Console.WriteLine("This user already exists!");
                System.Console.ForegroundColor = System.ConsoleColor.White;
            }
            else
            {
                _repository.Add(new Customer(customer.Id, customer.FirstName, customer.LastName, customer.Username, customer.Password, customer.Email, customer.UserType, new List<Order>()));
            }

            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _unitOfWork.BeginTransaction();

            try
            {
                _repository.Delete(_repository.GetById(id));
            }
            catch
            {
                System.Console.ForegroundColor = System.ConsoleColor.Green;
                System.Console.WriteLine("Ne postoo korisnik so taj ID");
                System.Console.ForegroundColor = System.ConsoleColor.White;
                _unitOfWork.Commit();
                return;
            }

            _unitOfWork.Commit();
        }
        public void Update(CustomerViewModel customer)
        {
            _unitOfWork.BeginTransaction();

            var originalCustomer = _repository.GetById(customer.Id);

            originalCustomer.FirstName=customer.FirstName;
            originalCustomer.LastName=customer.LastName;
            originalCustomer.Username = customer.Username;
            originalCustomer.Email=customer.Email;
            originalCustomer.Password = customer.Password;
            originalCustomer.UserType=customer.UserType;

            _repository.Update(originalCustomer);

            _unitOfWork.Commit();
        }

        public CustomerViewModel GetById(int id)
        {
            _unitOfWork.BeginTransaction();

            var customer = _repository.GetById(id)?.MapToViewModel();

            _unitOfWork.Commit();

            return customer;
        }

        public bool Authorise(int id, string password)
        {
            _unitOfWork.BeginTransaction();

            bool authorised = false;
            if (_repository.GetById(id).Password == password)
                authorised = true;

            _unitOfWork.Commit();

            return authorised;
        }

        public IEnumerable<CustomerViewModel> GetAll()
        {
            _unitOfWork.BeginTransaction();
            
            var allCustomers = _repository.GetAll()?.Select(x=>x.MapToViewModel());
            
            _unitOfWork.Commit();

            return allCustomers;
        }
    }
}
