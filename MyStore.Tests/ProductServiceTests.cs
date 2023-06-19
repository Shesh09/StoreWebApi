using AutoMapper;
using Moq;
using MyStore.Data;
using MyStore.Domain.Entities;
using MyStore.Infrastructure;
using MyStore.Models;
using MyStore.Services;
using MyStore.Tests.Mocks.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyStore.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> mockProductRepository;
        private readonly IMapper mapper;
        public ProductServiceTests()
        {
            mockProductRepository = new Mock<IProductRepository>();
            mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductProfile());
            }).CreateMapper();
        }

        [Fact]
        public void Should_Return_ListOfProductModel_On_GetALL()
        {
            //arrange
            mockProductRepository.Setup(x => x.GetAll())
                .Returns(new List<Product> { new Product() { } });
            var service = new ProductService(mockProductRepository.Object, mapper);
            //act
            var response = service.GetAllProducts();
            //assert
            Assert.IsType<List<ProductModel>>(response.ToList());
        }

        [Fact]
        public void Shoul_Return_OneProduct_On_GetById()
        {
            //arrange
            mockProductRepository.Setup(x => x.FindByProductId(ProductConsts.TestSupplierId))
             .Returns(ReturnOneProduct(ProductConsts.TestSupplierId));
            var service = new ProductService(mockProductRepository.Object, mapper);
            //act
            var response = service.GetById(ProductConsts.TestSupplierId);
            //assert
            Assert.IsType<Product>(response);
        }

        [Fact]
        public void Shoul_Return_ProductModel_On_Post()
        {
            //arrange
            mockProductRepository.Setup(x => x.Add(It.IsAny<Product>()))
            .Returns(ReturnOneProduct(ProductConsts.TestSupplierId));
            var service = new ProductService(mockProductRepository.Object, mapper);
            //act
            var response = service.AddProduct(ReturnOneProductModel());
            //assert
            Assert.IsType<ProductModel>(response);
            Assert.Equal(ProductConsts.ProductName, response.Productname);
        }

        [Fact]
        public void ShouldReturn_Model_On_Put()
        {
            //arrange
            mockProductRepository.Setup(x => x.Update(ReturnOneProduct(ProductConsts.TestSupplierId)))
            .Returns(ReturnOneProduct(ProductConsts.TestSupplierId));
            var service = new ProductService(mockProductRepository.Object, mapper);
            mockProductRepository.Setup(x => x.Update(It.IsAny<Product>())).Returns(ReturnOneProduct(ProductConsts.TestSupplierId));
            //act
            var response = service.UpdateProduct(ReturnOneProductModel());
            //asert
            Assert.Equal(ProductConsts.ProductName, response.Productname);
            Assert.IsType<ProductModel>(response);
        }

        [Fact]
        public void ShouldReturn_True_On_Delete()
        {
            //arrange
            mockProductRepository.Setup(x => x.Delete(ReturnOneProduct(ProductConsts.TestSupplierId)))
            .Returns(true);
            var service = new ProductService(mockProductRepository.Object, mapper);
            mockProductRepository.Setup(x => x.Delete(It.IsAny<Product>())).Returns(true);
            mockProductRepository.Setup(x => x.FindByProductId(It.IsAny<int>())).Returns(ReturnOneProduct(1));
            //act
            var response = service.Delete(ProductConsts.TestSupplierId);
            // Assert
            Assert.True(response);
        }

        private ProductModel ReturnOneProductModel()
        {
            return new ProductModel
            {
                Productname = ProductConsts.ProductName,
                Categoryid = (int)ProductConsts.Categories.Condiments,
                Supplierid = ProductConsts.TestSupplierId,
                Unitprice = ProductConsts.TestUnitPrice,
                Discontinued = true
            };
        }

        private Product ReturnOneProduct(int id)
        {
              
            return new Product
            {
                Productname = ProductConsts.ProductName,
                Categoryid = (int)ProductConsts.Categories.Condiments,
                Supplierid = ProductConsts.TestSupplierId,
                Unitprice = ProductConsts.TestUnitPrice,
                Discontinued = true
            };
        }
    }
}
