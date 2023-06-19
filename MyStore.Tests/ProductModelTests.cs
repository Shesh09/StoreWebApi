using MyStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyStore.Tests
{
    public class ProductModelTests
    {
        public const string ProductNameRequiredMessage = "The Productname field is required.";
        public const int ValidCategoryId = 2;

        [Fact]
        public void FailingTest()
        {
            Assert.Equal(2, 2);
        }

        [Fact]
        public void ShouldPass()
        {
            //arrange
            var sut = new ProductModel() 
            {
                Categoryid = ValidCategoryId,
                Productid = 2,
                Supplierid = 2,
                Unitprice = 10,
                Discontinued = true,
                Productname = "Test productname"
            };

            //act
            var validationResults = new List<ValidationResult>(); //vreau sa obtin lista de erori || face trigari la validarea facuta in controller (clasa care se ocupa de validare)
            var actual = Validator.TryValidateObject(sut, new ValidationContext(sut), validationResults, true);

            //assert
            Assert.True(actual, "Expected to succeed");
        }

        [Fact]
        public void Should_Fail_When_ProductName_IsEmpty()
        {
            //arrange
            var sut = new ProductModel()
            {
                Categoryid = ValidCategoryId,
                Productid = 2,
                Supplierid = 2,
                //Unitprice = 10,
                Discontinued = true,
                Productname = ""
            };

            //act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(sut, new ValidationContext(sut), validationResults, true);

            var message = validationResults[0];
            //var message1 = validationResults[1];


            //assert
            Assert.Equal(ProductNameRequiredMessage, message.ErrorMessage);
           // Assert.Equal("The Unitprice field is required.", message1.ErrorMessage);
        }
    }
}
