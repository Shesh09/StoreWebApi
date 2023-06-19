using AutoMapper;
using MyStore.Data;
using MyStore.Domain.Entities;
using MyStore.Infrastructure;
using MyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Services
{
    public interface IOrderService
    {
        Order Add(Order newOrder);
        IEnumerable<Order> GetAll(string? city, List<string>? country, Shippers shippers);

        Order GetById(int id);
        OrderModel UpdateOrder(OrderModel model);
        bool Exists(int id);
        bool Delete(int id);
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository repository;
        private readonly IMapper mapper;

        public OrderService(IOrderRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public IEnumerable<Order> GetAll(string? city, List<string>? country, Shippers shippers)
        {
            var allOrders = repository.GetAll(city, country, shippers).ToList();

            return allOrders;
        }

        public Order Add(Order newOrder)
        {
           return repository.Add(newOrder);
        }

        public Order GetById(int id)
        {
            return (Order)repository.GetById(id);
        }

        public OrderModel UpdateOrder(OrderModel model)
        {
            Order orderToUpdate = mapper.Map<Order>(model);
            var updatedOrder = repository.Update(orderToUpdate);
            return mapper.Map<OrderModel>(updatedOrder);
        }

        public bool Exists(int id)
        {
            return repository.Exists(id);
        }

        public bool Delete(int id)
        {
            //get item by id
            var itemToDelete = repository.GetById(id);
            //delete item
            return repository.Delete(itemToDelete);
        }
    }
}
