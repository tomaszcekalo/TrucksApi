using Microsoft.AspNetCore.Mvc;
using TrucksApi.Data;

namespace TrucksApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController
    {
        public StatusController(
            TrucksDb context,
            ITruckStatusValidator truckStatusValidator)
        {
            Context = context;
            TruckStatusValidator = truckStatusValidator;
        }

        public TrucksDb Context { get; }
        public ITruckStatusValidator TruckStatusValidator { get; }

        [HttpPut(Name = "SetStatusForTruck")]
        public async Task SetStatus(
            int truckId,
            TruckStatusEnum truckStatus,
            CancellationToken cancellationToken = default)
        {
            var truck = Context.Trucks.Find(truckId);
            if (!TruckStatusValidator.IsStatusChangeAllowed(truck.Status.Status, truckStatus))
            {
                throw new Exception("This status change is not allowed");
            }
            truck.Status.Status = truckStatus;
            Context.Trucks.Update(truck);
            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}