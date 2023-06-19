using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using MyStore.Data;
using MyStore.Domain.Entities;
using Xunit;

namespace MyStore.Tests
{
    public class ProductRepositoryTests
    {
        public ProductRepositoryTests()
        {

        }

        [Fact]
        public void Should_GetAllProducts()
        {
            //arrange
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.GetAll())    //setup => ii zic ce sa returneze si din ce
                    .Returns(ReturnMultiple());

            //act
            var result = mockRepo.Object.GetAll();   //instanta dummy pe care si-o face el si de pe care pot apela metode care sunt in repository-ul meu

            //assert
            Assert.Equal(3, result.Count());

            Assert.IsType<List<Product>>(result);
        }

        [Fact]
        public void Should_GetOneProduct()
        {
            //arrange
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.FindByProductId(ProductConsts.TestProduct))
                .Returns(ReturnOneProduct(ProductConsts.TestProduct));

            //act
            var result = mockRepo.Object.FindByProductId(ProductConsts.TestProduct);

            //asert
            Assert.Equal(ProductConsts.TestProduct, result.Productid);
            Assert.IsType<Product>(result);
        }

        [Fact]
        public void Shoul_Return_Product_On_Post()
        {
            //arrange
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.Add(It.IsAny<Product>())).Returns(ReturnOneProduct(ProductConsts.TestProduct));

            //act
            var result = mockRepo.Object.Add(ReturnOneProduct(ProductConsts.TestProduct));

            //asert
            Assert.Equal(ProductConsts.ProductName, result.Productname);
            Assert.IsType<Product>(result);
        }

        [Fact]
        public void ShouldReturn_Product_On_Put()
        {
            //arrange
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.Update(It.IsAny<Product>())).Returns(ReturnOneProduct(ProductConsts.TestProduct));

            //act
            var result = mockRepo.Object.Update(ReturnOneProduct(ProductConsts.TestProduct));

            //asert
            Assert.Equal(ProductConsts.ProductName, result.Productname);
            Assert.IsType<Product>(result);
        }

        [Fact]
        public void Shoul_Return_True_On_Delete()
        {
            //arrange
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.Delete(It.IsAny<Product>())).Returns(true);

            //act
            var result = mockRepo.Object.Delete(ReturnOneProduct(ProductConsts.TestProduct));

            //asert
            Assert.True(result);
        }

        private Product ReturnOneProduct(int i)
        {
            IEnumerable<Product> products = ReturnMultiple();
            return products.Where(x => x.Productid == i).FirstOrDefault();
        }

        private List<Product> ReturnMultiple()
        {
            return new List<Product>()
            {
                new Product
                {
                    Productid = ProductConsts.TestProduct,
                    Productname = ProductConsts.ProductName,
                    Categoryid = (int)ProductConsts.Categories.Condiments,
                    Supplierid = ProductConsts.TestSupplierId,
                    Unitprice = ProductConsts.TestUnitPrice,
                    Discontinued = true
                },
                new Product
                {
                    Productid = ProductConsts.TestProduct2,
                    Productname = ProductConsts.ProductName2,
                    Categoryid = (int)ProductConsts.Categories.Confections,
                    Supplierid = ProductConsts.TestSupplierId2,
                    Unitprice = ProductConsts.TestUnitPrice,
                    Discontinued = true
                },
                new Product
                {
                    Productid = ProductConsts.TestProduct3,
                    Productname = ProductConsts.ProductName3,
                    Categoryid = (int)ProductConsts.Categories.Dairy,
                    Supplierid = ProductConsts.TestSupplierId3,
                    Unitprice = ProductConsts.TestUnitPrice,
                    Discontinued = true
                }
            };
        }
    }
}
