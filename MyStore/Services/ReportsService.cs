using MyStore.Data;
using MyStore.Domain.Entities;
using MyStore.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Services
{
    public interface IReportsService
    {
        List<Customer> GetCustomersWithNoOrders();
        List<CustomerContact> GetContacts();
        List<NrOfCustomersForProductID> GetNrOfCustomersForProductID(int id);
    }
    public class ReportsService : IReportsService
    {
        private readonly IReportsRepository reportsRepository;

        public ReportsService(IReportsRepository reportsRepository)
        {
            this.reportsRepository = reportsRepository;
        }

        public List<Customer> GetCustomersWithNoOrders()
        {
            return this.reportsRepository.GetCustomersWithNoOrders();
        }

        public List<CustomerContact> GetContacts()
        {
            var result = this.reportsRepository.GetContacts();
            return result;
        }

        public List<NrOfCustomersForProductID> GetNrOfCustomersForProductID(int id)
        {
            return reportsRepository.GetNrOfCustomersForProductID(id);
        }
    }
}
