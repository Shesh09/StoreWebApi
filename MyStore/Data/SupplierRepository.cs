using MyStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Data
{
    public interface ISupplierRepository
    {
        IEnumerable<Supplier> GetAll();
        Supplier FindBySupplierId(int supplierId);
        Supplier Add(Supplier supplier);
        void Update(Supplier supplier);
        bool Exists(int id);
        bool Delete(Supplier supplierToDelete);
    }

    public class SupplierRepository : ISupplierRepository
    {
        private readonly StoreContext context;

        public SupplierRepository(StoreContext context)
        {
            this.context = context;
        }

        public IEnumerable<Supplier> GetAll()
        {
            return context.Suppliers.ToList();
        }

        public Supplier FindBySupplierId(int supplierId)
        {
            return context.Suppliers.First(x => x.Supplierid == supplierId);
        }

        public Supplier Add(Supplier supplier)
        {
            var addedSupplier =  context.Suppliers.Add(supplier);
            context.SaveChanges();
            return addedSupplier.Entity;
        }

        public void Update(Supplier supplierToUpdate)
        {
            context.Suppliers.Update(supplierToUpdate);
            context.SaveChanges();
        }

        public bool Exists(int id)
        {
            var exists = context.Suppliers.Count(x => x.Supplierid == id);
            return exists == 1;
        }

        public bool Delete(Supplier supplierToDelete)
        {
            var deletedItem = context.Suppliers.Remove(supplierToDelete);
            context.SaveChanges();
            return deletedItem != null;
        }
    }
}
