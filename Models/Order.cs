using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PspApi.Models
{
    public class Order
    {
        public Order
            (
                int amount,
                string description,
                string paymentMethod,
                string cardNumber,
                string cardholderName,
                int expMonth,
                int expYear,
                string cvv,
                string status,
                Guid merchantId,
                Guid customerId
            )
        {
            this.Id = Guid.NewGuid();
            this.Amount = amount;
            this.Description = description;
            this.PaymentMethod = paymentMethod;
            this.CardNumber = cardNumber;
            this.CardholderName = cardholderName;
            this.ExpMonth = expMonth;
            this.ExpYear = expYear;
            this.Cvv = cvv;
            this.Status = status;
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
            this.MerchantId = merchantId;
            this.CustomerId = customerId;
        }

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

        public Merchant Merchant { get; set; }

        [ForeignKey("Merchant")]
        public Guid MerchantId { get; set; }

        public Customer Customer { get; set; }

        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
    }
}
