using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Vidly.Web.Mappers;
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
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        // GET /api/customers/1
        [HttpGet]
        public Customer GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            // if customer doesn't exist, return not found
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            // else return customer
            return customer;
        }

        // creating new customer in Db
        // POST /api/customers
        [HttpPost]
        public Customer CreateCustomer(Customer customer)
        {
            // check if sent request is valid
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            // add customer to the context and save changes
            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer;
        }

        // edit (update) existing customer in Db
        // PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, Customer customer)
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
            MappersHelper.MapNewCustomer(customerInDb, customer);

            _context.SaveChanges();
        }
    }
}
