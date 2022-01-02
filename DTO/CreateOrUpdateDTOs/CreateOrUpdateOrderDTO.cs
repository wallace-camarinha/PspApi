using System.ComponentModel.DataAnnotations;

namespace PspApi.DTO
{
    public class CreateOrUpdateOrderDTO
    {
        [Required]
        public int Amount { get; set; }

        public string Description { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        [Required]
        public string CardNumber { get; set; }

        [Required]
        public string CardholderName { get; set; }

        [Required]
        public int ExpMonth { get; set; }

        [Required]
        public int ExpYear { get; set; }

        [Required]
        public string Cvv { get; set; }

        public string? Status { get; set; }

        [Required]
        public Guid MerchantId { get; set; }

        [Required]
        public Guid CustomerId { get; set; }
    }
}
