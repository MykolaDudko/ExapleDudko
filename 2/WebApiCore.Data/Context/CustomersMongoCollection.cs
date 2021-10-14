using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver;
using WebApiCore.Data.Models;

namespace WebApiCore.Data.Context
{
    public class CustomersMongoCollection
    {
        private readonly IMongoCollection<Customer> _customers;

        public CustomersMongoCollection(IMongoCollection<Customer> _customers)
        {
            this._customers = _customers;
        }

        public List<Customer> ToList() =>
            _customers.Find(Customer => true).ToList();

        public Customer Get(string id) =>
            _customers.Find<Customer>(Customer => Customer.Id == id).FirstOrDefault();
        public Customer FirstOrDefault(Expression<Func<Customer, bool>> predicator) =>
            _customers.Find<Customer>(predicator).FirstOrDefault();

        public Customer Add(Customer Customer)
        {
            _customers.InsertOne(Customer);
            return Customer;
        }

        public string Update(Customer CustomerIn) =>
            _customers.ReplaceOne(Customer => Customer.Id == CustomerIn.Id, CustomerIn).UpsertedId.ToString();

        // var update = Builders<Customer>.Update
        //     .Set(s => s.Name, CustomerIn.Name)
        //     .Set(s => s.Email, CustomerIn.Email)
        //     .Set(s => s.BirthDate, CustomerIn.BirthDate);

        // _customers.UpdateOne<Customer>(x => x.Id == CustomerIn.Id, update);

        public void Remove(Customer CustomerIn) =>
            _customers.DeleteOne(Customer => Customer.Id == CustomerIn.Id);

        public void Remove(string id) =>
            _customers.DeleteOne(Customer => Customer.Id == id);
    }
}