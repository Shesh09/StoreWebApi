using AutoMapper;
using MyStore.Data;
using MyStore.Domain.Entities;
using MyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Services
{
    public interface ICustomerService
    {
        CustomerModel AddCustomer(CustomerModel newCustomer);
        bool Delete(int id);
        bool Exists(int id);
        IEnumerable<CustomerModel> GetAllCustomers();
        Customer GetById(int id);
        void UpdateCustomer(CustomerModel model);
    }

    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            this.customerRepository = customerRepository;
            this.mapper = mapper;
        }

        public IEnumerable<CustomerModel> GetAllCustomers()
        {
            var allCustomers = customerRepository.GetAll().ToList();
            var customerModel = mapper.Map<IEnumerable<CustomerModel>>(allCustomers);
            
            return customerModel;
        }

        public Customer GetById(int id)
        {
            return (Customer)customerRepository.FindByCustomerId(id);
        }

        public CustomerModel AddCustomer(CustomerModel newCustomer)
        {
            Customer customerToAdd = mapper.Map<Customer>(newCustomer);
            var addedCustomer = customerRepository.Add(customerToAdd);
            newCustomer = mapper.Map<CustomerModel>(addedCustomer);

            return newCustomer;
        }

        public void UpdateCustomer(CustomerModel model)
        {
            Customer customerToUpdate = mapper.Map<Customer>(model);
            customerRepository.Update(customerToUpdate);
        }

        public bool Exists(int id)
        {
            return customerRepository.Exists(id);
        }

        public bool Delete(int id)
        {
            //get item by id
            var itemToDelete = customerRepository.FindByCustomerId(id);
            //delete item
            return customerRepository.Delete(itemToDelete);
        }
    }
}
