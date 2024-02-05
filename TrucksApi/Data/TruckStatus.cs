namespace TrucksApi.Data
{
    public class TruckStatus
    {
        public int Id { get; set; }
        public TruckStatusEnum Status { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
