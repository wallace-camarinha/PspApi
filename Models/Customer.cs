using PspApi.DTO;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PspApi.Models
{
    public class Customer
    {
        public Customer
            (
                string firstName,
                string lastName,
                string email,
                string documentType,
                string documentNumber
            )
        {
            this.Id = Guid.NewGuid();
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.DocumentType = documentType ?? string.Empty;
            this.DocumentNumber = documentNumber ?? string.Empty;
            this.Active = true;
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;

        }

        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }

        public bool Active { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
