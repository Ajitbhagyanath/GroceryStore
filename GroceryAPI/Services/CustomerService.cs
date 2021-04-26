using System;
using GroceryStoreAPI.Domain.Model;
using GroceryStoreAPI.Domain.Repositories;
using GroceryStoreAPI.Domain.Services;
using GroceryStoreAPI.Domain.Services.Communication;
using GroceryStoreAPI.Domain.Services.Constants;
using Microsoft.Extensions.Logging;

namespace GroceryStoreAPI.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger _logger;
        public CustomerService(ICustomerRepository customerRepository,ILogger<CustomerService> logger)
        {
            _logger = logger;
            _customerRepository=customerRepository;
        }
        public CustomersResponse List()
        {
            try
            {
                var existingCustomer = _customerRepository.List();
                if (existingCustomer == null || existingCustomer.customers==null || existingCustomer.customers.Count==0)
                    return new CustomersResponse(ErrorMessage.CustomerListEmpty);
                else
                    return new CustomersResponse(existingCustomer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                return new CustomersResponse(string.Format(ErrorMessage.ErrorListingCustomers,ex.Message));
            }
        }
        public CustomerResponse FindById(int id)
        {
            
            try
            {
                var existingCustomer = _customerRepository.FindById(id);
                if (existingCustomer == null)
                    return new CustomerResponse(string.Format(ErrorMessage.CustomerIdNotFound, id));
                else
                    return new CustomerResponse(_customerRepository.FindById(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                return new CustomerResponse(string.Format(ErrorMessage.ErrorFindingCustomerId,id, ex.Message));
            }
        }
        public CustomerResponse Save(Customer customer)
        {
            
            try
            {
                var existingCustomer = _customerRepository.FindById(customer.id);
                if (existingCustomer != null)
                    return new CustomerResponse(string.Format(ErrorMessage.CustomerIdAlreadyExists, customer.id));
                customer = _customerRepository.Add(customer);
                return new CustomerResponse(customer);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                return new CustomerResponse(string.Format(ErrorMessage.ErrorSavingCustomerName, customer.name, ex.Message));
            }
        }
        public CustomerResponse Update(int id, Customer customer)
        {
            try
            {
                var existingCustomer = _customerRepository.FindById(id);
                if (existingCustomer == null)
                    return new CustomerResponse(string.Format(ErrorMessage.CustomerIdNotFound, id));
                existingCustomer.name = customer.name;
                _customerRepository.Update(existingCustomer);
                return new CustomerResponse(existingCustomer);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                return new CustomerResponse(string.Format(ErrorMessage.ErrorUpdatingCustomerId, id, ex.Message));
            }
        }
        public CustomerResponse Delete(int id)
        {
            
            try
            {
                var existingCustomer = _customerRepository.FindById(id);
                if (existingCustomer == null)
                    return new CustomerResponse(string.Format(ErrorMessage.CustomerIdNotFound, id));
                _customerRepository.Remove(existingCustomer);
                return new CustomerResponse(existingCustomer);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                return new CustomerResponse(string.Format(ErrorMessage.ErrorDeletingCustomerId, id, ex.Message));
            }
        }

    }
}