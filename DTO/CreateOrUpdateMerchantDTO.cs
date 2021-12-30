using System.ComponentModel.DataAnnotations;

namespace PspApi.DTO
{
    public class CreateOrUpdateMerchantDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string DocumentNumber { get; set; }
    }
}
