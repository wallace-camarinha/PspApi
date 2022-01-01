using PspApi.DTO;
using PspApi.Models;

namespace PspApi.Repositories.OrdersRepository
{
    public interface IOrdersRepository
    {
        public Task<Order?> FindById(Guid id);
        public Task<List<Order>> ListByMerchantId(Guid? merchantId);
        public Task<List<Order>> ListByCustomerId(Guid? merchantId);
        public Task<List<Order>> ListAll();
        public Task<Order> Create(Order order);
    }
}
