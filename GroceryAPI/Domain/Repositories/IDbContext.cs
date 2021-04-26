using System;
using GroceryStoreAPI.Domain.Model;

namespace GroceryStoreAPI.Domain.Repositories
{
    public interface IDbContext
    {
        public Customers List();
        public Customer AddCustomer(Customer customer);
        public bool RemoveCustomer(Customer customer);
        public bool UpdateCustomer(Customer customer);
    }
}
