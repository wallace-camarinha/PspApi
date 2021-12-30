using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PspApi.Data;
using PspApi.DTO;
using PspApi.Models;

namespace PspApi.Controllers
{
    [Route("api/merchants")]
    [ApiController]
    public class MerchantsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public MerchantsController(DatabaseContext context)
        {
            _context = context;
        }

        private static List<Merchant> merchants = new List<Merchant>
        {
            new Merchant("Test Merchant", "1234")
        };

        [HttpPost]
        public async Task<ActionResult<Merchant>> Create(CreateOrUpdateMerchantDTO merchantData)
        {
            var validadeMerchantDoc = await _context.Merchants.FirstOrDefaultAsync(x => x.DocumentNumber == merchantData.DocumentNumber);

            if (validadeMerchantDoc != null)
                return BadRequest("Merchant already exists!");

            var createdMerchant = new Merchant(merchantData.Name, merchantData.DocumentNumber);

            _context.Merchants.Add(createdMerchant);
            await _context.SaveChangesAsync();


            return Ok(createdMerchant);
        }

        [HttpGet]
        public async Task<ActionResult<List<Merchant>>> GetAll()
        {
            var result = await _context.Merchants.Where(x => x.Active).ToListAsync();


            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Merchant>> GetOne(Guid id)
        {
            var merchant = await _context.Merchants.FindAsync(id);

            if (merchant == null)
                return NotFound("Merchant not found!");

            if (!merchant.Active)
                return NotFound("Merchant not found!");

            return Ok(merchant);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Merchant>> Update(Guid id, CreateOrUpdateMerchantDTO merchantData)
        {
            var merchant = await _context.Merchants.FindAsync(id);


            if (merchant == null)
                return NotFound("Merchant not found!");

            if (!merchant.Active)
                return NotFound("Merchant not found!");


            merchant.Name = merchantData.Name ?? merchant.Name;
            merchant.DocumentNumber = merchantData.DocumentNumber ?? merchant.DocumentNumber;
            merchant.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(merchant);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Merchant>> Delete(Guid id)
        {
            var merchant = await _context.Merchants.FindAsync(id);

            if (merchant == null)
                return NotFound("Merchant not found!");

            if (!merchant.Active)
                return NotFound("Merchant not found!");

            merchant.Active = false;
            await _context.SaveChangesAsync();

            return Ok("Merchant Deleted");
        }


    }
}
