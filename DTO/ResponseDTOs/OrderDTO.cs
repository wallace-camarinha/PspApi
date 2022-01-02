using PspApi.Models;

namespace PspApi.DTO.ResponseDTOs
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public string PaymentMethod { get; set; }
        public string CardNumber { get; set; }
        public string CardholderName { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public string Cvv { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public MerchantDTO Merchant { get; set; }
        public CustomerDTO Customer { get; set; }
    }
}
