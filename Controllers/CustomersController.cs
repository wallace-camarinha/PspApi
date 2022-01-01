using Microsoft.AspNetCore.Mvc;
using PspApi.DTO;
using PspApi.Models;
using PspApi.Repositories.CustomersRepository;

namespace PspApi.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersRepository _customersRepository;

        public CustomersController(ICustomersRepository repository)
        {
            _customersRepository = repository;
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> Create(CreateOrUpdateCustomerDTO customerData)
        {
            var validateEmail = await _customersRepository.FindByEmail(customerData.Email);

            if (validateEmail != null)
                return BadRequest("Customer already exists!");

            var customer = await _customersRepository.Create(customerData);
            return Ok(customer);
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetAll()
        {
            var result = await _customersRepository.ListAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetOne(Guid id)
        {
            var customer = await _customersRepository.FindById(id);

            if (customer == null)
                return NotFound("Customer not found!");

            if (!customer.Active)
                return NotFound("Customer not found!");

            return Ok(customer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> Update(Guid id, CreateOrUpdateCustomerDTO newData)
        {
            var customer = await _customersRepository.FindById(id);
            if (customer == null)
                return NotFound("Customer not found!");

            var validateEmail = await _customersRepository.FindByEmail(newData.Email);
            if (validateEmail != null && newData.Email != customer.Email)
                return BadRequest("Email in use by another Customer!");

            if (!customer.Active)
                return NotFound("Customer not found!");

            await _customersRepository.Update(customer, newData);

            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var customer = await _customersRepository.FindById(id);

            if (customer == null)
                return NotFound("Customer not found!");

            if (!customer.Active)
                return NotFound("Customer not found!");

            await _customersRepository.Delete(customer);

            return NoContent();
        }
    }
}
