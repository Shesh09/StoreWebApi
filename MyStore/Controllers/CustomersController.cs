using Microsoft.AspNetCore.Mvc;
using MyStore.Domain.Entities;
using MyStore.Models;
using MyStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService customerService;
        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        // GET: api/<CustomersController>
        [HttpGet]
        public IEnumerable<CustomerModel> Get()
        {
            var customerList = customerService.GetAllCustomers();
            return customerList;
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            var customer = customerService.GetById(id);
            return customer;
        }

        // POST api/<CustomersController>
        [HttpPost]
        public IActionResult Post([FromBody] CustomerModel newCutomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var addedCustomer = customerService.AddCustomer(newCutomer);
            return CreatedAtAction("Get", new { id = addedCustomer.Custid }, addedCustomer);
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CustomerModel customerToUpdate)
        {
            if (id != customerToUpdate.Custid)
            {
                return BadRequest();
            }
            if (!customerService.Exists(id))
            {
                return NotFound();
            }

            customerService.UpdateCustomer(customerToUpdate);
            return NoContent();
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!customerService.Exists(id))
            {
                return NotFound();
            }
            //search the object with the id
            //delete the object
            //return no content
            var isDeleted = customerService.Delete(id);
            //daca nu s-a sters, return 422 status code
            return NoContent();
        }
    }
}
