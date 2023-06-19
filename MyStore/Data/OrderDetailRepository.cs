using MyStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Data
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetail> GetAllOrders();
        IEnumerable<OrderDetail> GetById(int id);
        OrderDetail Add(OrderDetail newOrderDetail);
        bool Delete(OrderDetail orderDetailToDelete);
        void Update(OrderDetail orderDetailToUpdate);
        bool Exists(int id);
    }
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly StoreContext context;

        public OrderDetailRepository(StoreContext context)
        {
            this.context = context;
        }

        public IEnumerable<OrderDetail> GetAllOrders()
        {
            return context.OrderDetails.ToList();
        }

        public IEnumerable<OrderDetail> GetById(int orderId)
        {
            return context.OrderDetails.Where(x => x.Orderid == orderId).ToList();
        }

        public OrderDetail Add(OrderDetail newOrderDetail)
        {
            var savedEntity = context.OrderDetails.Add(newOrderDetail).Entity;
            context.SaveChanges();
            return savedEntity;
        }

        public void Update(OrderDetail orderDetailToUpdate)
        {
            context.OrderDetails.Update(orderDetailToUpdate);
            context.SaveChanges();
        }
        public bool Exists(int id)
        {
            var exists = context.OrderDetails.Count(x => x.Orderid == id);
            return exists == 1;
        }
        public bool Delete(OrderDetail orderDetailToDelete)
        {
            var deletedItem = context.OrderDetails.Remove(orderDetailToDelete);
            context.SaveChanges();
            return deletedItem != null;
        }
    }
}
