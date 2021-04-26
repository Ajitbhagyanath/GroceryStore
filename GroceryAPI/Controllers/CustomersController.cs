
using System.Threading.Tasks;
using GroceryStoreAPI.Domain.Model;
using GroceryStoreAPI.Domain.Services;
using GroceryStoreAPI.Domain.Services.Communication;
using GroceryStoreAPI.Domain.Services.Constants;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStoreAPI.Controllers
{

    [Route(AppConstants.ApiCustomersRoute)]
    [Produces(AppConstants.ProducesType)]
    [ApiController]
    public class CustomersController: Controller
    {
        private readonly ICustomerService _customerService;
        public CustomersController(ICustomerService customerService)
        {
            _customerService=customerService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CustomersResponse),200)]
        [ProducesResponseType(typeof(CustomersResponse), 400)]
        public async Task<IActionResult> ListAsync()
        {
            CustomersResponse result = await Task.Run(() => _customerService.List());
            if (!result.Success)
                return BadRequest(result);
            else
                return Ok(result);
        }

        [HttpGet(AppConstants.CustomerID)]
        [ProducesResponseType(typeof(CustomerResponse), 200)]
        [ProducesResponseType(typeof(CustomerResponse), 400)]
        public async Task<IActionResult> GetAsync(int id)
        {
            CustomerResponse result = await Task.Run(() => _customerService.FindById(id));
            if (!result.Success)
                return BadRequest(result);
            else
                return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerResponse),200)]
        [ProducesResponseType(typeof(CustomerResponse),400)]
        public async Task<IActionResult> PostAsync([FromBody] Customer resource)
        {
            CustomerResponse result = await Task.Run(() => _customerService.Save(resource));
            if(!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut(AppConstants.CustomerID)]
        [ProducesResponseType(typeof(CustomerResponse),200)]
        [ProducesResponseType(typeof(CustomerResponse),400)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Customer resource)
        {
            CustomerResponse result = await Task.Run(() => _customerService.Update(id, resource));
            if(!result.Success)
                return BadRequest(result);
            else 
                return Ok(result);
        }

        [HttpDelete(AppConstants.CustomerID)]
        [ProducesResponseType(typeof(CustomerResponse),200)]
        [ProducesResponseType(typeof(CustomerResponse),400)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            CustomerResponse result = await Task.Run(() => _customerService.Delete(id));
            if(!result.Success)
                return BadRequest(result);
            else
                return Ok(result);
        }
    }
}