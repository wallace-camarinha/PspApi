using Microsoft.AspNetCore.Mvc;
using PspApi.DTO;
using PspApi.Models;
using PspApi.Repositories.MerchantsRepository;

namespace PspApi.Controllers
{
    [Route("api/merchants")]
    [ApiController]
    public class MerchantsController : ControllerBase
    {
        private readonly IMerchantsRepository _merchantsRepository;

        public MerchantsController(IMerchantsRepository repository)
        {
            _merchantsRepository = repository;
        }

        [HttpPost]
        public async Task<ActionResult<Merchant>> Create(CreateOrUpdateMerchantDTO merchantData)
        {
            var validadeDoc = await _merchantsRepository.FindByDoc(merchantData.DocumentNumber);

            if (validadeDoc != null)
                return BadRequest("Merchant already exists!");

            var createdMerchant = await _merchantsRepository.Create(merchantData);

            return Ok(createdMerchant);
        }

        [HttpGet]
        public async Task<ActionResult<List<Merchant>>> GetAll()
        {
            var result = await _merchantsRepository.ListAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Merchant>> GetOne(Guid id)
        {
            var merchant = await _merchantsRepository.FindById(id);

            if (merchant == null)
                return NotFound("Merchant not found!");

            if (!merchant.Active)
                return NotFound("Merchant not found!");

            return Ok(merchant);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Merchant>> Update(Guid id, CreateOrUpdateMerchantDTO newData)
        {
            var merchant = await _merchantsRepository.FindById(id);

            if (merchant == null)
                return NotFound("Merchant not found!");

            var validadeDoc = await _merchantsRepository.FindByDoc(newData.DocumentNumber);
            if (validadeDoc != null && newData.DocumentNumber != merchant.DocumentNumber)
                return BadRequest("Document number in use by another Merchant");

            if (!merchant.Active)
                return NotFound("Merchant not found!");

            await _merchantsRepository.Update(merchant, newData);

            return Ok(merchant);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var merchant = await _merchantsRepository.FindById(id);

            if (merchant == null)
                return NotFound("Merchant not found!");

            if (!merchant.Active)
                return NotFound("Merchant not found!");

            await _merchantsRepository.Delete(merchant);

            return NoContent();
        }
    }
}
