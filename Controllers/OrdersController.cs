using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PspApi.DTO;
using PspApi.DTO.ResponseDTOs;
using PspApi.Models;
using PspApi.Repositories.OrdersRepository;

namespace PspApi.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IMapper _mapper;

        public OrdersController(IOrdersRepository repository, IMapper mapper)
        {
            _ordersRepository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> Create(CreateOrUpdateOrderDTO orderData)
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
            var responseOrder = _mapper.Map<OrderDTO>(createdOrder);

            return Ok(responseOrder);
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderDTO>>> GetAll()
        {
            var order = await _ordersRepository.ListAll();
            var responseOrder = _mapper.Map<List<OrderDTO>>(order);

            return Ok(responseOrder);
        }

        [HttpGet]
        [Route("/api/merchants/{merchantId}/orders")]
        public async Task<ActionResult<List<OrderDTO>>> ListByMerchantId(Guid merchantId)
        {
            var orders = await _ordersRepository.ListByMerchantId(merchantId);
            var responseOrder = _mapper.Map<List<OrderDTO>>(orders);

            return Ok(responseOrder);
        }

        [HttpGet]
        [Route("/api/customers/{customerId}/orders")]
        public async Task<ActionResult<List<OrderDTO>>> ListByCustomerId(Guid customerId)
        {
            var orders = await _ordersRepository.ListByCustomerId(customerId);
            var responseOrder = _mapper.Map<List<OrderDTO>>(orders);

            return Ok(responseOrder);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOne(Guid id)
        {
            var order = await _ordersRepository.FindById(id);
            if (order == null)
                return NotFound("Order not found!");


            var responseOrder = _mapper.Map<OrderDTO>(order);

            return Ok(responseOrder);
        }
    }
}
