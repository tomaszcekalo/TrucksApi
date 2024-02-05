using System.ComponentModel.DataAnnotations.Schema;

namespace TrucksApi.Data
{
    public class Truck
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        // must have a unique alphanumeric code given by the user
        public string AlphanumericCode { get; set; }
        //must have a name
        public string Name { get; set; }
        //may have a description
        public string Description { get; set; }
        public TruckStatus Status { get; set; }
    }
}