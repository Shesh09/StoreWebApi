using AutoMapper;
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
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            this.supplierService = supplierService;
        }


        // GET: api/<SuppliersController>
        [HttpGet]
        public IEnumerable<SupplierModel> Get()
        {
            var suppliersList = supplierService.GetAllSuppliers();
            return suppliersList;
        }

        // GET api/<SuppliersController>/5
        [HttpGet("{id}")]
        public Supplier Get(int id)
        {
            return supplierService.GetById(id);
        }

        // POST api/<SuppliersController>
        [HttpPost]
        public IActionResult Post([FromBody] SupplierModel newSupplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var addedSupplier = supplierService.AddSupplier(newSupplier);
            return CreatedAtAction("Get", new { id = addedSupplier.Supplierid}, addedSupplier);
        }

        // PUT api/<SuppliersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] SupplierModel supplierTuUpdate)
        {
            if (id != supplierTuUpdate.Supplierid)
            {
                return BadRequest();
            }
            if (!supplierService.Exists(id))
            {
                return NotFound();
            }

            supplierService.UpdateSupplier(supplierTuUpdate);
            return NoContent();
        }

        // DELETE api/<SuppliersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!supplierService.Exists(id))
            {
                return NotFound();
            }

            var isDeleted = supplierService.DeleteSupplier(id);
            return NoContent();
        }
    }
}
