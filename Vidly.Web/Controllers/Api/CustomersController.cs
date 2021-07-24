using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Vidly.Web.Dtos;
using Vidly.Web.Models;

namespace Vidly.Web.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/customers
        [HttpGet]
        public IEnumerable<CustomerDto> GetCustomers()
        {
            // map with out calling it, without '()'
            return _context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
        }

        // GET /api/customers/1
        [HttpGet]
        public CustomerDto GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            // if customer doesn't exist, return not found
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            // else return customer
            return Mapper.Map<Customer, CustomerDto>(customer);
        }

        // creating new customer in Db
        // POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            // check if sent request is valid
            if (!ModelState.IsValid)
                return BadRequest();

            // add customer to the context and save changes
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            // add Id from new customer to Dto
            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        // edit (update) existing customer in Db
        // PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            // check if request is valid
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            // if 'id' is not valid from request
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            // all good, update (replacing old data with the new from request)
            // map new customer from request to old customer
            // first argument is source, second argument is target (it will output changes to it)
            Mapper.Map(customerDto, customerInDb);

            _context.SaveChanges();
        }

        // delete existing customer from Db
        // DELETE /api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            // try to get customer with requested id
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            // check customer
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }
    }
}
