using System.ComponentModel.DataAnnotations;

namespace TrucksApi.Models
{
    public class TruckBase
    {
        [Required]
        public string AlphanumericCode { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}