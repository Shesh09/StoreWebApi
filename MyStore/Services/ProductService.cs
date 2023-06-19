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
    public interface IProductService
    {
        IEnumerable<ProductModel> GetAllProducts();
        Product GetById(int id);
        ProductModel AddProduct(ProductModel newProduct);
        ProductModel UpdateProduct(ProductModel model);
        bool Exists(int id);
        bool Delete(int id);
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }
        public IEnumerable<ProductModel> GetAllProducts()
        {
            //take domain objects
            var allProducts = productRepository.GetAll().ToList();//List<Product>
                                                                  //transform domain objects from List<Product> -> List<ProductModel>
            var productModels = mapper.Map<IEnumerable<ProductModel>>(allProducts);

            return productModels;
        }

        public Product GetById(int id)
        {
            return (Product)productRepository.FindByProductId(id);
        }

        public ProductModel AddProduct(ProductModel newProduct)
        {
            // -> ProductModel in Product
            //assuming is valid -> transform to Product(domain object)
            Product productToAdd = mapper.Map<Product>(newProduct);
            var addedProduct = productRepository.Add(productToAdd);
            newProduct = mapper.Map<ProductModel>(addedProduct);

            return newProduct;
        }

        public ProductModel UpdateProduct(ProductModel model)
        {
            Product productToUpdate = mapper.Map<Product>(model);
            var updatedProduct = productRepository.Update(productToUpdate);
            return mapper.Map<ProductModel>(updatedProduct);
        }

        public bool Exists(int id)
        {
            return productRepository.Exists(id);
        }

        public bool Delete(int id)
        {
            //get item by id
            var itemToDelete = productRepository.FindByProductId(id);
            //delete item
            return productRepository.Delete(itemToDelete);
        }
    }
}
