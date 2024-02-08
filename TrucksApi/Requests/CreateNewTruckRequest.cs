using TrucksApi.Data;
using TrucksApi.Models;

namespace TrucksApi.Requests
{
    public class CreateNewTruckRequest : TruckBase
    {
        public TruckStatusEnum TruckStatus { get; set; }
    }
}