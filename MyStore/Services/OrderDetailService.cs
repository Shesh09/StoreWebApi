using MyStore.Data;
using MyStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Services
{
    public interface IOrderDetailService
    {
        IEnumerable<OrderDetail> GetAllOrders();
        IEnumerable<OrderDetail> GetById(int id);
        OrderDetail AddOrderDetail(OrderDetail newOrderDetail);
        bool Exists(int id);
        void UpdateOrderDetail(OrderDetail model);
        bool Delete(int id);
    }

    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository repository;
        public OrderDetailService(IOrderDetailRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<OrderDetail> GetAllOrders()
        {
            var allOrderDetail = repository.GetAllOrders().ToList();

            return allOrderDetail;
        }

        public IEnumerable<OrderDetail> GetById(int id)
        {
            return (IEnumerable<OrderDetail>)repository.GetById(id);
        }       

        public bool Exists(int id)
        {
            return repository.Exists(id);
        }

        public OrderDetail AddOrderDetail(OrderDetail newOrderDetail)
        {                        
            return repository.Add(newOrderDetail);
        }
        public void UpdateOrderDetail(OrderDetail model)
        {
            repository.Update(model);
        }
        public bool Delete(int id)
        {
            OrderDetail itemToDelete = (OrderDetail)repository.GetById(id);
            return repository.Delete(itemToDelete);
        }
    }
}
