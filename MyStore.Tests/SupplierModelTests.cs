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
    public class SupplierModelTests
    {
        [Fact]
        public void ShouldPass()
        {
            //arrange
            var sut = new SupplierModel()
            {
                Supplierid = 1,
                Companyname = "Scoala IT",
                Contacttitle = "Irina",
                Country = "Romania",
                Address = "Cluj",
                City = "Cluj Napoca",
                Contactname = "Alina",
                Phone = "0743"
            };

            //act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(sut, new ValidationContext(sut), validationResults, true);

            //assert
            Assert.True(actual, "Expected to succeed");
        }
    }
}
