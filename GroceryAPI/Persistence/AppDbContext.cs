using System;
using System.Collections.Generic;
using System.IO;
using GroceryStoreAPI.Domain.Model;
using GroceryStoreAPI.Domain.Repositories;
using GroceryStoreAPI.Domain.Services.Constants;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GroceryStoreAPI.Persistence
{
    public class AppDbContext : IDbContext
    {
        private readonly ILogger<AppDbContext> _logger;

        private List<Customer> customerList = new List<Customer>();

        public AppDbContext(ILogger<AppDbContext> logger)
        {
            _logger = logger;
            try
            {
                ReadJsonFile();
            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException.ToString());
                throw;
            }
        }
        public Customers List()
        {
            try
            {
                return new Customers() { customers = customerList };
            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException.ToString());
                throw;
            }
            
        }
        
        public Customer AddCustomer(Customer customer)
        {
            try
            {
                customer.id = customerList.Count + 1;
                customerList.Add(customer);
                Commit();
                return customer;
            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException.ToString());
                throw;
            }
        }
        public bool RemoveCustomer(Customer customer)
        {
            
            try
            {
                customerList.Remove(customer);
                return Commit();
            }
            catch(Exception e)
            {
                _logger.LogError(e.InnerException.ToString());
                throw ;
            }
        }
        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                customerList.Find(e => e.id==customer.id).name=customer.name;
                return Commit();
            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException.ToString());
                throw;
            }
            
        }
        private bool Commit()
        {
            try
            {
                string json = JsonConvert.SerializeObject(new Customers { customers = customerList }, Formatting.Indented);
                return WriteJsonFile(json);
            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException.ToString());
                throw;
            }
        }

        private static bool WriteJsonFile(string json)
        {
            using (StreamWriter r = new StreamWriter(AppConstants.JsonPath))
            {
                r.Write(json);
                return true;
            }
        }

        private void ReadJsonFile()
        {
            using (StreamReader r = new StreamReader(AppConstants.JsonPath))
            {
                string json = r.ReadToEnd();
                Customers root = JsonConvert.DeserializeObject<Customers>(json);
                customerList = root.customers;
            }
        }
    }
}
