using Moq;
using MyStore.Domain.Entities;
using MyStore.Models;
using MyStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Tests.Mocks.Services
{
    public class MockProductService : Mock<IProductService>
    {
        public MockProductService MockGetAllProducts(List<ProductModel> results)
        {
            Setup(x => x.GetAllProducts()).Returns(results);

            return this;
            //this.Method1().Method(2) - fiecare metoda a clasei returneaza clasa in sine = fluent
        }

        public MockProductService MockGetById(Product product)
        {
            Setup(x => x.GetById(It.IsAny<int>())).Returns(product);
                //.Throws(new Exception("Product with ID not found"));
            
            return this;
        }
        //fluent validations - cand returnez aceeasi clasa
    }
}
