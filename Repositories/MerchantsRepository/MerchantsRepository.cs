using Microsoft.EntityFrameworkCore;
using PspApi.Data;
using PspApi.DTO;
using PspApi.Models;

namespace PspApi.Repositories.MerchantsRepository
{
    public class MerchantsRepository : IMerchantsRepository
    {
        private readonly DatabaseContext _context;

        public MerchantsRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Merchant> Create(CreateOrUpdateMerchantDTO merchantData)
        {
            var createdMerchant = new Merchant(merchantData.Name, merchantData.DocumentNumber);

            _context.Merchants.Add(createdMerchant);
            await _context.SaveChangesAsync();

            return createdMerchant;
        }

        public async Task<Merchant?> Delete(Merchant merchant)
        {
            merchant!.Active = false;
            merchant.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return merchant;
        }

        public async Task<Merchant?> FindByDoc(string doc)
        {
            var merchant = await _context.Merchants.FirstOrDefaultAsync(x => x.DocumentNumber == doc);
            return merchant;
        }

        public async Task<Merchant?> FindById(Guid id)
        {
            var merchant = await _context.Merchants.FindAsync(id);
            return merchant;
        }

        public async Task<List<Merchant>> ListAll()
        {
            var result = await _context.Merchants.Where(x => x.Active).ToListAsync();
            return result;
        }

        public async Task<Merchant?> Update(Merchant merchant, CreateOrUpdateMerchantDTO newData)
        {
            merchant.Name = newData.Name ?? merchant.Name;
            merchant.DocumentNumber = newData.DocumentNumber ?? merchant.DocumentNumber;
            merchant.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return merchant;
        }
    }
}
