using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApiCore.Api.Services.BackgroundService;
using WebApiCore.Data.DTO;
using WebApiCore.Data.Models;
using WebApiCore.Data.Repository;

namespace WebApiCore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IRepository<Customer> contextCustomers;
        private readonly ILogger<CustomersController> logger;
        private readonly IMapper mapper;

        public CustomersController(
            IRepository<Customer> contextCustomers,
            ILogger<CustomersController> logger,
            IMapper mapper
        )
        {
            this.contextCustomers = contextCustomers;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CustomerDTO>> Get([FromServices] TimedHostedService timedService)
        {
            timedService.StartAsync();
            logger.LogInformation("getting all customers");
            return mapper.Map<IEnumerable<Customer>, CustomerDTO[]>(contextCustomers.All);
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> Get(string id)
        {
            var res = contextCustomers.FindById(id);
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        [HttpPost]
        public void Post([FromBody] Customer value)
        {
            contextCustomers.Update(value);
        }

        /// <summary>
        /// Creates a Customer.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/customers
        ///     {
        ///        "name": "Name",
        ///        "email": "email"
        ///        "birthDate": "2019-12-22"
        ///     }
        ///
        /// </remarks>
        /// <param name="item"></param>
        /// <returns>A newly created Customer</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>  
        [HttpPut]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult Put([FromBody] Customer value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            var res = contextCustomers.Add(value);
            return CreatedAtAction(nameof(Put), res);

        }

        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="id"></param>   
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var entity = contextCustomers.FindById(id);
            contextCustomers.Delete(entity);
            return Ok();
        }
    }
}