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
namespace GroceryStoreAPITest
{
    public class ControllerTest
    {
        
        [Fact]
        public void ListAsync()
        {
            //arrange
            List<Customer> customerList= new() { new Customer(){ id = 1, name = "Ajit" }, new Customer() { id = 2, name = "Testing" } };
            var customers = new Customers() { customers = customerList };
            var AppDBContext = new Mock<IDbContext>();
            AppDBContext
                .Setup(m => m.List())
                .Returns(customers);
            ICustomerRepository customerRepository = new CustomerRepository(AppDBContext.Object);
            ILogger<CustomerService> _logger = Mock.Of<ILogger<CustomerService>>();
            ICustomerService customerService = new CustomerService(customerRepository, _logger);
            
            CustomersController customersctrl = new CustomersController(customerService);
            //act
            var lst = customersctrl.ListAsync();
            //assert
            Console.WriteLine(lst.Result.ToString());
            Assert.Equal("Microsoft.AspNetCore.Mvc.OkObjectResult", lst.Result.ToString());
        }
        [Fact]
        public void GetAsync()
        {
            //arrange

            //act

            //assert
            Assert.NotEmpty(null);
        }
        [Fact]
        public void PostAsync()
        {
            //arrange

            //act

            //assert
            Assert.NotEmpty(null);
        }
        [Fact]
        public void PutAsync()
        {
            //arrange

            //act

            //assert
            Assert.NotEmpty(null);
        }
        [Fact]
        public void DeleteAsync()
        {
            //arrange

            //act

            //assert
            Assert.NotEmpty(null);
        }
    }
}
