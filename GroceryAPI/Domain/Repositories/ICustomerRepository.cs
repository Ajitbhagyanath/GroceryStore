using GroceryStoreAPI.Domain.Model;
using System.Collections.Generic;
namespace GroceryStoreAPI.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Customers List();
        Customer Add(Customer customer);
        Customer FindById(int id);
        bool Update(Customer customer);
        bool Remove(Customer customer);
    }
}