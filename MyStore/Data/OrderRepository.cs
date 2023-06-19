using Microsoft.EntityFrameworkCore;
using MyStore.Domain.Entities;
using MyStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Data
{
    public interface IOrderRepository
    {
        Order Add(Order newOrder);
        IQueryable<Order> GetAll(string? city, List<string>? country, Shippers shippers);
        Order GetById(int orderId);
        Order Update(Order orderToUpdate);
        bool Exists(int id);
        bool Delete(Order orderToDelete);
    }
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreContext context;

        public OrderRepository(StoreContext context)
        {
            this.context = context;
        }

        //add filters
        public IQueryable<Order> GetAll(string? city, List<string>? country, Shippers shippers)
        {
            var query = this.context.Orders
                .Include(x => x.OrderDetails)
                .Select(x => x);

            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(x => x.Shipcity == city);
            }

            query = query.Where(x => x.Shipperid == (int)shippers);

            if (country.Any())
            {
                query = query.Where(x => country.Contains(x.Shipcountry));
            }

            //var pageNumber = 3;
            //var itemsPerPage = 20;
            //query.Skip(pageNumber-1 * itemsPerPage).Take(itemsPerPage);

            return query;
        }

        public Order Add(Order newOrder)
        {
            var savedEntity = context.Orders.Add(newOrder).Entity;
            context.SaveChanges();
            return savedEntity; 
        }

        public Order GetById(int orderId)
        {
            return context.Orders.FirstOrDefault(x => x.Orderid == orderId);
        }

        public Order Update(Order orderToUpdate)
        {
            var updatedOrder = context.Orders.Update(orderToUpdate);
            context.SaveChanges();
            return updatedOrder.Entity;
        }

        public bool Exists(int id)
        {
            var exists = context.Orders.Count(x => x.Orderid == id);
            return exists == 1;
        }

        public bool Delete(Order orderToDelete)
        {
            var deletedItem = context.Orders.Remove(orderToDelete);
            context.SaveChanges();
            return deletedItem != null;
        }
    }
}
