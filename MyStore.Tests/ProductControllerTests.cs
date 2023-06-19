using Microsoft.AspNetCore.Mvc;
using Moq;
using MyStore.Controllers;
using MyStore.Domain.Entities;
using MyStore.Models;
using MyStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace MyStore.Tests
{
    public class ProductControllerTests
    {
        private Mock<IProductService> mockProductService;

        public ProductControllerTests()
        {
            mockProductService = new Mock<IProductService>();
        }

        [Fact]
        public void Should_Return_OK_OnGetAll()
        {
            //arrange
            mockProductService.Setup(x => x.GetAllProducts())
                .Returns(new List<ProductModel> { new ProductModel() { } });

            var controller = new ProductsController(mockProductService.Object);

            //act
            var response = controller.Get();

            var result = response.Result as OkObjectResult;
            var actualData = result.Value as IEnumerable<ProductModel>;

            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<ProductModel>>(actualData);
        }

        [Fact]
        public void ShouldReturn_AllProducts()
        {
            //arrange
            mockProductService.Setup(x => x.GetAllProducts())
                .Returns(MultipleProducts);

            var controller = new ProductsController(mockProductService.Object);

            //act
            var response = controller.Get();

            var result = response.Result as OkObjectResult;
            var actualData = result.Value as IEnumerable<ProductModel>;

            //assert
            Assert.Equal(MultipleProducts().Count(), actualData.Count());

        }

        [Fact]
        public void Should_Return_OK_OnGetByID()
        {
            //arrange
            int testSessionId = 3;
            mockProductService.Setup(x => x.GetById(testSessionId))
                .Returns(SingleProduct());

            var controller = new ProductsController(mockProductService.Object);

            //act
            var response = controller.GetProduct(testSessionId);

            var result = response.Result as OkObjectResult;
            var actualData = result.Value as Product;

            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<Product>(actualData);
        }

        [Fact]
        public void Should_Return_NotFoundResult_OnGetByID()
        {
            //arrange
            int testSessionId = 10;
            mockProductService.Setup(x => x.GetById(testSessionId)).Returns(NullProduct());
            
            var controller = new ProductsController(mockProductService.Object);

            //act
            var response = controller.GetProduct(testSessionId);

            //assert
            Assert.IsType<NotFoundResult> (response.Result);
            Assert.Null(response.Value);
        }

        [Fact]
        public void Test_POST_ValidObjectPassed_ReturnsCreatedResponse()
        {
            //arrange
            mockProductService.Setup(x => x.AddProduct(It.IsAny<ProductModel>())).Returns(MultipleProducts()[0]);
            var controller = new ProductsController(mockProductService.Object);
            
            //act
            var response = controller.Post(MultipleProducts()[0]);
            
            //assert
            var addedProd = Assert.IsType<CreatedAtActionResult>(response);
        }

        [Fact]
        public void Test_POST_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            //arrange
            mockProductService.Setup(x => x.AddProduct(It.IsAny<ProductModel>())).Returns(MultipleProducts()[0]);
            var controller = new ProductsController(mockProductService.Object);

            //act
            var createdResponse = controller.Post(MultipleProducts()[0]) as CreatedAtActionResult;
            var item = createdResponse.Value as ProductModel;

            //assert
            Assert.IsType<ProductModel>(item);
            Assert.Equal("Test Product Name 1", item.Productname);
        }

        [Fact]
        public void Test_POST_InvalidObjectPassed_ReturnsBadRequest()
        {
            //arrange
            mockProductService.Setup(x => x.AddProduct(It.IsAny<ProductModel>())).Returns(MultipleProducts()[1]);
            var controller = new ProductsController(mockProductService.Object);

            controller.ModelState.AddModelError("Productname", "Required");

            //act
            var badResponse = controller.Post(MultipleProducts()[1]);

            //assert
            Assert.IsType<BadRequestResult>(badResponse);
        }

        [Fact]
        public void ShouldReturn_Ok_On_Put()
        {
            ProductModel model = new ProductModel
            {
                Productid = ProductConsts.TestProduct,
                Productname = ProductConsts.ProductName,
                Supplierid = ProductConsts.TestSupplierId,
                Categoryid = (int)ProductConsts.Categories.Condiments,
                Unitprice = ProductConsts.TestUnitPrice,
                Discontinued = true
            };

            //arrange

            mockProductService.Setup(x => x.UpdateProduct(It.IsAny<ProductModel>())).Returns(model);
            mockProductService.Setup(x => x.Exists(ProductConsts.TestProduct)).Returns(true);
            var controller = new ProductsController(mockProductService.Object);

            //act
            var response = controller.Put(ProductConsts.TestProduct, model);
            var result = response.Result as OkObjectResult;
            var actualData = result.Value as ProductModel;

            //assert
            Assert.IsType<OkObjectResult>(result);
            
        }

        [Fact]
        public void ShouldReturn_NoContent_On_Delete()
        {
            ////arrange
            mockProductService.Setup(x => x.Exists(MultipleProducts()[1].Productid)).Returns(true);
            mockProductService.Setup(x => x.Delete(MultipleProducts()[1].Productid)).Returns(true);
            var controller = new ProductsController(mockProductService.Object);
            // Act
            var result = controller.Delete(MultipleProducts()[1].Productid);
            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        private ProductModel ProductToInsert()
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

        private List<ProductModel> MultipleProducts()
        {
            return new List<ProductModel>()
            {
                new ProductModel
                {
                    Categoryid = (int)ProductConsts.Categories.Condiments,
                    Productid = ProductConsts.TestProduct,
                    Productname = ProductConsts.ProductName, // ce e in ghilimele e hardcodat
                    Discontinued = false,
                    Supplierid = ProductConsts.TestSupplierId,
                    Unitprice = ProductConsts.TestUnitPrice
                },

                new ProductModel
                {
                    Categoryid = (int)ProductConsts.Categories.Condiments,
                    Productid = ProductConsts.TestSupplierId,
                    Productname = null, 
                    Discontinued = true,
                    Supplierid = ProductConsts.TestSupplierId,
                    Unitprice = ProductConsts.TestUnitPrice
                }
            };
        }

        private Product SingleProduct()
        {
            return new Product
            {
                Categoryid = (int)ProductConsts.Categories.Condiments,
                Productid = ProductConsts.TestProduct,
                Productname = ProductConsts.ProductName, // ce e in ghilimele e hardcodat
                Discontinued = false,
                Supplierid = ProductConsts.TestSupplierId,
                Unitprice = ProductConsts.TestUnitPrice
            };
        }

        private Product? NullProduct()
        {
            return null;
        }

        

    }
}
