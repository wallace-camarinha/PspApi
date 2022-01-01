using System.ComponentModel.DataAnnotations;

namespace PspApi.DTO
{
    public class CreateOrUpdateCustomerDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public string? DocumentType { get; set; }

        public string? DocumentNumber { get; set; }
    }
}
