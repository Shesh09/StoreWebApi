using Moq;
using MyStore.Data;
using MyStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyStore.Tests
{
    public class SupplierRepositoryTests
    {
        [Fact]
        public void Should_GetAllProducts()
        {
            //arrange
            var mockRopo = new Mock<ISupplierRepository>();
            mockRopo.Setup(repo => repo.GetAll())
                .Returns(new List<Supplier>());

            //act
            var result = mockRopo.Object.GetAll();

            //assert
            Assert.Equal(0, result.Count());
            Assert.IsType<List<Supplier>>(result);
        }
    }
}
