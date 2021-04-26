using Xunit;
using GroceryStoreAPI.Controllers;
using Moq;
using GroceryStoreAPI.Domain.Services;
using GroceryStoreAPI.Domain.Model;
using GroceryStoreAPI.Domain.Repositories;
using System;
using System.Collections.Generic;
using GroceryStoreAPI.Persistence;
using GroceryStoreAPI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using GroceryStoreAPI.Domain.Services.Communication;
using GroceryStoreAPI.Domain.Services.Constants;

namespace GroceryStoreAPITest
{
    public class ControllerTest
    {
        
        [Fact]
        public void List_All_Customers_Name_of_2nd_Customer()
        {
            //arrange
            ICustomerService customerService = MockDataWithTwoCustomers();
            CustomersController customersctrl = new CustomersController(customerService);
            //act
            var result = customersctrl.ListAsync();
            //assert
            var response = result.Result as OkObjectResult;
            CustomersResponse customersResponse = response.Value as CustomersResponse;
            Assert.True(customersResponse.Success);
            Assert.Equal(2,customersResponse.Resource.customers.Count);
            Assert.Equal("Ajit", customersResponse.Resource.customers[0].name);
        }
        [Fact]
        public void Get_FirstCustomer_CheckName()
        {
            //arrange
            ICustomerService customerService = MockDataWithTwoCustomers();
            CustomersController customersctrl = new CustomersController(customerService);
            //act
            var result = customersctrl.GetAsync(1);
            //assert
            var response = result.Result as OkObjectResult;
            CustomerResponse customerResponse = response.Value as CustomerResponse;
            Assert.True(customerResponse.Success);
            Assert.Equal(1, customerResponse.Resource.id);
            Assert.Equal("Ajit", customerResponse.Resource.name);
        }
        [Fact]
        public void Post_NewCustomer_CheckSuccess()
        {
            //arrange
            ICustomerService customerService = MockDataWithTwoCustomers();
            CustomersController customersctrl = new CustomersController(customerService);
            //act
            var result = customersctrl.PostAsync(new Customer() { id = 0, name = "Ajit Post testing" });
            //assert
            var response = result.Result as OkObjectResult;
            CustomerResponse customerResponse = response.Value as CustomerResponse;
            Assert.True(customerResponse.Success);
            //Mockdata won't return new ID
        }
        [Fact]
        public void Put_UpdateExisting_Customer_CheckNameAndID()
        {
            //arrange
            ICustomerService customerService = MockDataWithTwoCustomers();
            CustomersController customersctrl = new CustomersController(customerService);
            //act
            var result = customersctrl.PutAsync(2, new Customer() { id = 2, name = "Testing Put by Ajit" });
            //assert
            var response = result.Result as OkObjectResult;
            CustomerResponse customerResponse = response.Value as CustomerResponse;
            Assert.True(customerResponse.Success);
            Assert.Equal(2, customerResponse.Resource.id);
            Assert.Equal("Testing Put by Ajit", customerResponse.Resource.name);
        }
        [Fact]
        public void Put_Update_Non_Existing_Customer_CheckNameAndID()
        {
            //arrange
            ICustomerService customerService = MockDataWithTwoCustomers();
            CustomersController customersctrl = new CustomersController(customerService);
            //act
            var result = customersctrl.PutAsync(5, new Customer() { id = 5, name = "Testing Invalid Put by Ajit" });
            //assert
            var response = result.Result as BadRequestObjectResult;
            CustomerResponse customerResponse = response.Value as CustomerResponse;
            Assert.False(customerResponse.Success);
            Assert.Equal(string.Format(ErrorMessage.CustomerIdNotFound, 5), customerResponse.Message);
        }
        [Fact]
        public void Delete_ExistingCustomer_CheckNameID()
        {
            //arrange
            ICustomerService customerService = MockDataWithTwoCustomers();
            CustomersController customersctrl = new CustomersController(customerService);
            //act
            var result = customersctrl.DeleteAsync(2);
            //assert
            var response = result.Result as OkObjectResult;
            CustomerResponse customerResponse = response.Value as CustomerResponse;
            Assert.True(customerResponse.Success);
            Assert.Equal(2, customerResponse.Resource.id);
            Assert.Equal("Testing", customerResponse.Resource.name);
        }
        [Fact]
        public void Delete_Non_ExistingCustomer_CheckNameID()
        {
            //arrange
            ICustomerService customerService = MockDataWithTwoCustomers();
            CustomersController customersctrl = new CustomersController(customerService);
            //act
            var result = customersctrl.DeleteAsync(3);
            //assert
            var response = result.Result as BadRequestObjectResult;
            CustomerResponse customerResponse = response.Value as CustomerResponse;
            Assert.False(customerResponse.Success);
            Assert.Equal(string.Format(ErrorMessage.CustomerIdNotFound,3), customerResponse.Message);
            
        }
        private static ICustomerService MockDataWithTwoCustomers()
        {
            List<Customer> customerList = new() { new Customer() { id = 1, name = "Ajit" }, new Customer() { id = 2, name = "Testing" } };
            var customers = new Customers() { customers = customerList };
            var AppDBContext = new Mock<IDbContext>();
            AppDBContext
                .Setup(m => m.List())
                .Returns(customers);
            ICustomerRepository customerRepository = new CustomerRepository(AppDBContext.Object);
            ILogger<CustomerService> _logger = Mock.Of<ILogger<CustomerService>>();
            ICustomerService customerService = new CustomerService(customerRepository, _logger);
            return customerService;
        }
    }
}
