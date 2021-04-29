using GroceryStoreAPI.Domain.Model;

namespace GroceryStoreAPI.Domain.Services.Communication
{
    public class CustomerResponse : BaseResponse<Customer>
    {
        /// Good Response
        public CustomerResponse(Customer customer) : base(customer)
        { }

        /// Bad Response
        public CustomerResponse(string message) : base(message)
        { }
    }
}