﻿using Microsoft.AspNetCore.Mvc;
using MyStore.Domain.Entities;
using MyStore.Domain.Extensions;
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
    public class ReportsController : ControllerBase
    {
        private readonly IReportsService reportsService;

        public ReportsController(IReportsService reportsService)
        {
            this.reportsService = reportsService;
        }

        // GET: api/<ReportsController>
        [HttpGet]
        public ActionResult<List<Customer>> CustomerWithNoOrders()
        {
            return reportsService.GetCustomersWithNoOrders();
        }

        // GET: /<ReportsController>
        [HttpGet("/contacts")]
        public ActionResult<List<CustomerContact>> CustomerContacts()
        {
            var contacts = reportsService.GetContacts();
            return contacts;
        }

        // GET api/<ReportsController>/5
        [HttpGet("{id}")]
        public ActionResult<List<NrOfCustomersForProductID>> GetNrOfCustomersForProductID(int id)
        {
            return reportsService.GetNrOfCustomersForProductID(id);
        }

        // POST api/<ReportsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ReportsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ReportsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
