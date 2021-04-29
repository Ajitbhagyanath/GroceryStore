using GroceryStoreAPI.Domain.Model;
using GroceryStoreAPI.Domain.Services.Communication;
namespace GroceryStoreAPI.Domain.Services
{
    public interface ICustomerService
    {
        CustomersResponse List();
        CustomerResponse FindById(int id);
        CustomerResponse Save(Customer product);
        CustomerResponse Update(int id, Customer product);
        CustomerResponse Delete(int id);
    }
}