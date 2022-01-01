using Microsoft.AspNetCore.Mvc;
using PspApi.DTO;
using PspApi.Models;
using PspApi.Repositories.OrdersRepository;

namespace PspApi.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersRepository _ordersRepository;

        public OrdersController(IOrdersRepository repository)
        {
            _ordersRepository = repository;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Create(CreateOrUpdateOrderDTO orderData)
        {
            var order = new Order
                (
                    amount: orderData.Amount,
                    description: orderData.Description,
                    paymentMethod: orderData.PaymentMethod,
                    cardNumber: orderData.CardNumber,
                    cardholderName: orderData.CardholderName,
                    expMonth: orderData.ExpMonth,
                    expYear: orderData.ExpYear,
                    cvv: orderData.Cvv,
                    status: "processing",
                    merchantId: orderData.MerchantId,
                    customerId: orderData.CustomerId
                );

            var createdOrder = await _ordersRepository.Create(order);
            return Ok(createdOrder);
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAll()
        {
            var order = await _ordersRepository.ListAll();
            return Ok(order);
        }

        [HttpGet]
        [Route("/api/merchants/{merchantId}/orders")]
        public async Task<ActionResult<List<Order>>> ListByMerchantId(Guid merchantId)
        {
            var orders = await _ordersRepository.ListByMerchantId(merchantId);
            return orders;
        }

        [HttpGet]
        [Route("/api/customers/{customerId}/orders")]
        public async Task<ActionResult<List<Order>>> ListByCustomerId(Guid customerId)
        {
            var orders = await _ordersRepository.ListByCustomerId(customerId);
            return orders;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOne(Guid id)
        {
            var order = await _ordersRepository.FindById(id);
            if (order == null)
                return NotFound("Order not found!");

            return order;
        }
    }
}
