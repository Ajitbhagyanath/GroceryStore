using GroceryStoreAPI.Domain.Model;

namespace GroceryStoreAPI.Domain.Services.Communication
{
    public class CustomersResponse : BaseResponse<Customers>
    {
        /// Good Response
        public CustomersResponse(Customers customers) : base(customers)
        { }
        /// Bad Response
        public CustomersResponse(string message) : base(message)
        { }
    }
}

