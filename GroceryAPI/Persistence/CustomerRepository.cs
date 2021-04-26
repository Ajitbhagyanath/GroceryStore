using GroceryStoreAPI.Domain.Model;
using GroceryStoreAPI.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace GroceryStoreAPI.Persistence
{
public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbContext _context;
        public CustomerRepository(IDbContext dbcontext)
        {
            _context = dbcontext;
        }
        
        public Customers List()
        {
            return  _context.List();
        }
        public Customer Add(Customer customer)
        {
            return _context.AddCustomer(customer);
        }

        public Customer FindById(int id)
        {
            return  _context.List().customers.Find(e=> e.id==id);
        }

        public bool Update(Customer customer)
        {
             return _context.UpdateCustomer(customer);
        }

        public bool Remove(Customer customer)
        {
            return _context.RemoveCustomer(customer);
        }
    }
}