using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyStore.Domain.Entities;
using MyStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService orderDetailService;
        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            this.orderDetailService = orderDetailService;
        }

        // GET: api/<OrderDetailController>
        [HttpGet]
        public ActionResult<IEnumerable<OrderDetail>> Get()
        {
            var orderDetailList = orderDetailService.GetAllOrders();
            return Ok(orderDetailList);
        }

        // GET api/<OrderDetailController>/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<OrderDetail>> Get(int id)
        {
            var result = orderDetailService.GetById(id);
            
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

        // POST api/<OrderDetailController>
        [HttpPost]
        public IActionResult Post([FromBody] OrderDetail newOrderDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var addedOrderDetail = orderDetailService.AddOrderDetail(newOrderDetail);

            return CreatedAtAction("Get", new { id = addedOrderDetail.Orderid }, addedOrderDetail);
        }

        // PUT api/<OrderDetailController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)] //,Type=typeof(OrderDetail) daca vreau sa adaug obiectul la return
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(OrderDetail))]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Put(int id, [FromBody] OrderDetail orderDetailToUpdate)
        {
            //exists by 
            if (id != orderDetailToUpdate.Orderid)
            {
                return BadRequest();
            }
            if (!orderDetailService.Exists(id))
            {
                return NotFound();
            }
            orderDetailService.UpdateOrderDetail(orderDetailToUpdate);
            return NoContent();
        }

        // DELETE api/<OrderDetailController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(OrderDetail))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(OrderDetail))]
        public IActionResult Delete(int id)
        {
            if (!orderDetailService.Exists(id))
            {
                return NotFound();
            }
            
            orderDetailService.Delete(id);
            return NoContent();
        }
    }
}
