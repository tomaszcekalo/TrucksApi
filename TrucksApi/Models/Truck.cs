using System.ComponentModel.DataAnnotations;
using TrucksApi.Data;

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
    public class CreateNewTruck : TruckBase
    {
        public TruckStatusEnum TruckStatus { get; set; }
    }

    public class UpdateTruck : TruckBase
    {
        public int Id {  get; set; }
    }
}
