
using System.ComponentModel.DataAnnotations;

namespace PspApi.Models
{
    public class Merchant
    {
        public Merchant(string name, string documentNumber)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.DocumentNumber = documentNumber;
            this.Active = true;
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
        }

        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string DocumentNumber { get; set; }

        public bool Active { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


    }
}
