using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PspApi.DTO;
using PspApi.DTO.ResponseDTOs;
using PspApi.Models;
using PspApi.Repositories.CustomersRepository;

namespace PspApi.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly IMapper _mapper;

        public CustomersController(ICustomersRepository repository, IMapper mapper)
        {
            _customersRepository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> Create(CreateOrUpdateCustomerDTO customerData)
        {
            var validateEmail = await _customersRepository.FindByEmail(customerData.Email);

            if (validateEmail != null)
                return BadRequest("Customer already exists!");

            var customer = await _customersRepository.Create(customerData);

            var customerResult = _mapper.Map<CustomerDTO>(customer);

            return Ok(customerResult);
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerDTO>>> GetAll()
        {
            var result = await _customersRepository.ListAll();
            var customerResult = _mapper.Map<List<CustomerDTO>>(result);

            return Ok(customerResult);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetOne(Guid id)
        {
            var customer = await _customersRepository.FindById(id);

            if (customer == null)
                return NotFound("Customer not found!");

            if (!customer.Active)
                return NotFound("Customer not found!");

            var customerResult = _mapper.Map<CustomerDTO>(customer);

            return Ok(customerResult);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerDTO>> Update(Guid id, CreateOrUpdateCustomerDTO newData)
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

            var customerResult = _mapper.Map<CustomerDTO>(customer);

            return Ok(customerResult);
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
