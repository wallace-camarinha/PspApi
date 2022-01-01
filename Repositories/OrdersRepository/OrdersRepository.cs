using Microsoft.EntityFrameworkCore;
using PspApi.Data;
using PspApi.DTO;
using PspApi.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PspApi.Repositories.OrdersRepository
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly DatabaseContext _context;
        public OrdersRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Order> Create(Order order)
        {
            var createdOrder = _context.Add(order);
            await _context.SaveChangesAsync();

            var returnOrder = await _context.Orders
                .Include(x => x.Merchant)
                .Include(x => x.Customer)
                .FirstOrDefaultAsync(x => x.Id == createdOrder.Entity.Id);

            return returnOrder!;
        }

        public async Task<List<Order>> ListByMerchantId(Guid? merchantId)
        {
            var returnOrders = await _context.Orders
                .Include(x => x.Merchant)
                .Include(x => x.Customer)
                .Where(x => x.MerchantId == merchantId)
                .ToListAsync();

            return returnOrders;
        }

        public async Task<List<Order>> ListByCustomerId(Guid? customerId)
        {
            var returnOrders = await _context.Orders
                .Include(x => x.Merchant)
                .Include(x => x.Customer)
                .Where(x => x.CustomerId == customerId)
                .ToListAsync();

            return returnOrders;
        }

        public async Task<Order?> FindById(Guid id)
        {
            var returnOrder = await _context.Orders
                .Include(x => x.Merchant)
                .Include(x => x.Customer)
                .FirstOrDefaultAsync(x => x.Id == id);

            return returnOrder!;
        }

        public async Task<List<Order>> ListAll()
        {
            var result = await _context.Orders
                .Include(x => x.Merchant)
                .Include(x => x.Customer)
                .ToListAsync();

            return result;
        }

    }
}
