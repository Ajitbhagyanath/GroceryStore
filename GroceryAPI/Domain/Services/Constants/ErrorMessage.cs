
namespace GroceryStoreAPI.Domain.Services.Constants
{
    public struct ErrorMessage
    {
        public const string CustomerListEmpty = "Customer List is empty";
        public const string CustomerIdNotFound = "Customer with id :{0} not found";
        public const string CustomerIdAlreadyExists = "Customer with id :{0} already exists";
        public const string ErrorListingCustomers = "An error occurred when Listing the Customers : {0}";
        public const string ErrorFindingCustomerId = "An error occurred when finding the Customer id = {0}: {1}";
        public const string ErrorSavingCustomerName = "An error occurred when Saving the Customer Name = {0}: {1}";
        public const string ErrorUpdatingCustomerId = "An error occurred when Updating the Customer id = {0}: {1}";
        public const string ErrorDeletingCustomerId = "An error occurred when Deleting the Customer id = {0}: {1}";

    }
}
