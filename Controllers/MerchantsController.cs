using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PspApi.DTO;
using PspApi.DTO.ResponseDTOs;
using PspApi.Models;
using PspApi.Repositories.MerchantsRepository;

namespace PspApi.Controllers
{
    [Route("api/merchants")]
    [ApiController]
    public class MerchantsController : ControllerBase
    {
        private readonly IMerchantsRepository _merchantsRepository;
        private readonly IMapper _mapper;

        public MerchantsController(IMerchantsRepository repository, IMapper mapper)
        {
            _merchantsRepository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<MerchantDTO>> Create(CreateOrUpdateMerchantDTO merchantData)
        {
            var validadeDoc = await _merchantsRepository.FindByDoc(merchantData.DocumentNumber);

            if (validadeDoc != null)
                return BadRequest("Merchant already exists!");

            var createdMerchant = await _merchantsRepository.Create(merchantData);

            var responseMerchant = _mapper.Map<MerchantDTO>(createdMerchant);

            return Ok(responseMerchant);
        }

        [HttpGet]
        public async Task<ActionResult<List<MerchantDTO>>> GetAll()
        {
            var merchant = await _merchantsRepository.ListAll();
            var responseMerchant = _mapper.Map<MerchantDTO>(merchant);

            return Ok(responseMerchant);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MerchantDTO>> GetOne(Guid id)
        {
            var merchant = await _merchantsRepository.FindById(id);

            if (merchant == null)
                return NotFound("Merchant not found!");

            if (!merchant.Active)
                return NotFound("Merchant not found!");

            var responseMerchant = _mapper.Map<MerchantDTO>(merchant);

            return Ok(responseMerchant);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MerchantDTO>> Update(Guid id, CreateOrUpdateMerchantDTO newData)
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

            var responseMerchant = _mapper.Map<MerchantDTO>(merchant);

            return Ok(responseMerchant);
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
