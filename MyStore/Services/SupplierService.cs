﻿using AutoMapper;
using MyStore.Data;
using MyStore.Domain.Entities;
using MyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Services
{
    public interface ISupplierService
    {
        SupplierModel AddSupplier(SupplierModel supplier);
        bool DeleteSupplier(int id);
        bool Exists(int id);
        IEnumerable<SupplierModel> GetAllSuppliers();
        Supplier GetById(int id);
        void UpdateSupplier(SupplierModel model);
    }

    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository supplierRepository;
        private readonly IMapper mapper;

        public SupplierService(ISupplierRepository supplierRepository, IMapper mapper)
        {
            this.supplierRepository = supplierRepository;
            this.mapper = mapper;
        }

        public IEnumerable<SupplierModel> GetAllSuppliers()
        {
            var allSuppliers = supplierRepository.GetAll();
            var suppliersModel = mapper.Map<IEnumerable<SupplierModel>>(allSuppliers);
            return suppliersModel;
        }

        public Supplier GetById(int id)
        {
            return supplierRepository.FindBySupplierId(id);
        }

        public SupplierModel AddSupplier(SupplierModel newSupplier)
        {
            Supplier supplierToAdd = mapper.Map<Supplier>(newSupplier);
            var addedSupplier = supplierRepository.Add(supplierToAdd);
            newSupplier = mapper.Map<SupplierModel>(addedSupplier);
            
            return newSupplier;
        }

        public void UpdateSupplier(SupplierModel model)
        {
            Supplier supplierToUpdate = mapper.Map<Supplier>(model);
            supplierRepository.Update(supplierToUpdate);
        }

        public bool Exists(int id)
        {
            return supplierRepository.Exists(id);
        }

        public bool DeleteSupplier(int id)
        {
            var supplierToDelete = supplierRepository.FindBySupplierId(id);
            return supplierRepository.Delete(supplierToDelete);
        }
    }
}
