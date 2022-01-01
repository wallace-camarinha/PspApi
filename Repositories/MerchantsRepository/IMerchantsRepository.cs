using PspApi.DTO;
using PspApi.Models;

namespace PspApi.Repositories.MerchantsRepository
{
    public interface IMerchantsRepository
    {
        public Task<Merchant?> FindById(Guid id);
        public Task<Merchant?> FindByDoc(string doc);
        public Task<List<Merchant>> ListAll();
        public Task<Merchant> Create(CreateOrUpdateMerchantDTO merchantData);
        public Task<Merchant?> Update(Merchant merchant, CreateOrUpdateMerchantDTO newData);
        public Task<Merchant?> Delete(Merchant merchant);
    }
}
