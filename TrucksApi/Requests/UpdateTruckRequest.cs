using TrucksApi.Models;

namespace TrucksApi.Requests
{
    public class UpdateTruckRequest : TruckBase
    {
        public int Id { get; set; }
    }
}