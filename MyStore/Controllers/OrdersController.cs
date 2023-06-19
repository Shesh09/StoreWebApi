using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyStore.Domain.Entities;
using MyStore.Infrastructure;
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
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }


        // GET: api/<OrdersController>
        [HttpGet]
        public ActionResult<IEnumerable<OrderModel>> Get(string? city, [FromQuery] List<string>? country, Shippers shippers)
        {
            //1. shipcity -> free value from a parameter
            //2. a predefined value for : Customer
            var allOrders = orderService.GetAll(city, country, shippers);
            var mappedOrders = mapper.Map<List<OrderModel>>(allOrders);
           
            return Ok(mappedOrders);
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public ActionResult<OrderModel> Get(int id)
        {
            var result = orderService.GetById(id);
            var finalResult = mapper.Map<OrderModel>(result);
            if (finalResult == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(finalResult);
            }
        }

        // POST api/<OrdersController>
        [HttpPost]
        public IActionResult Post([FromBody] OrderModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            //model -> domain object
            var order = mapper.Map<Order>(model);

            var addedItem = orderService.Add(order);
            //do a reverse mapping Order -> OrderModel
            return CreatedAtAction("Get", new { id = addedItem.Orderid }, mapper.Map<OrderModel>(addedItem));
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public ActionResult<OrderModel> Put(int id, [FromBody] OrderModel orderToUpdate)
        {
            if (id != orderToUpdate.Orderid)
            {
                return BadRequest();
            }
            if (!orderService.Exists(id))
            {
                return NotFound();
            }
            var updatedOrder = orderService.UpdateOrder(orderToUpdate);
            return Ok(updatedOrder);
        }
        

       // DELETE api/<OrdersController>/5
       [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!orderService.Exists(id))
            {
                return NotFound();
            }
            var isDeleted = orderService.Delete(id);

            return NoContent();
        }
    }
}
