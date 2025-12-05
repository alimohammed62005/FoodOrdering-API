using FoodOrdering.Application.Interfaces;
using FoodOrdering.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerDto>>> GetAll()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> Create(CreateCustomerDto dto)
        {
            var created = await _customerService.AddCustomerAsync(dto);
            return Ok("تمت الإضافة بنجاح");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateCustomerDto dto)
        {
            var success = await _customerService.UpdateCustomerAsync(id, dto);
            if (!success)
                return NotFound();

            return Ok("تم التحديث بنجاح");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _customerService.DeleteCustomerAsync(id);
            if (!result)
                return NotFound();

            return Ok("تم الحذف بنجاح");
        }
    }
}
