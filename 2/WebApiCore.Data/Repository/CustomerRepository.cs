using System.Collections.Generic;
using System.Linq;
using WebApiCore.Data.Context;
using WebApiCore.Data.Models;

namespace WebApiCore.Data.Repository
{
    public class CustomerRepository : IRepository<Customer>
        {
            private readonly MongoContext context;

            public IEnumerable<Customer> All => context.Customers.ToList();

            public CustomerRepository(MongoContext context)
            {
                this.context = context;
            }
            public Customer Add(Customer entity)
            {
                return context.Customers.Add(entity);
            }

            public void Delete(Customer entity)
            {
                context.Customers.Remove(entity);
            }

            public Customer FindById(string Id)
            {
                return context.Customers.FirstOrDefault(e => e.Id == Id);
            }

            public string Update(Customer entity)
            {
                return context.Customers.Update(entity);
            }
        }
}