using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrucksApi.Data;
using TrucksApi.Models;

namespace TrucksApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TruckController : ControllerBase
    {
        public TrucksDb Context { get; }
        public IDateTimeProvider DateTimeProvider { get; }
        public ITrucksRepository TrucksRepository { get; }

        public TruckController(
            TrucksDb context, 
            IDateTimeProvider dateTimeProvider,
            ITrucksRepository trucksRepository)
        {
            this.Context = context;
            DateTimeProvider = dateTimeProvider;
            TrucksRepository = trucksRepository;
        }

        [HttpGet(Name = "GetList")]
        public async Task<IEnumerable<GetAllTrucksItem>> GetListAsync(
            string AlhpanumericCodeFilter,
            string NameFilter,
            [FromQuery] TruckStatusEnum[] truckStatusFilter = null,
            OrderTrucksBy orderBy = OrderTrucksBy.AlphanumericCode,
            CancellationToken cancellationToken = default)
        {
            return await TrucksRepository.GetListAsync(
                AlhpanumericCodeFilter,
                NameFilter,
                truckStatusFilter,
                orderBy,
                cancellationToken);
        }

        [HttpPost(Name = "CreateNew")]
        public async Task CreateNewAsync(
            CreateNewTruck createNewTruck, 
            CancellationToken cancellationToken = default)
        {
            await this.Context.AddAsync(new Truck()
            {
                AlphanumericCode = createNewTruck.AlphanumericCode,
                Description = createNewTruck.Description,
                Name = createNewTruck.Name,
                Status = new TruckStatus()
                {
                    CreatedDate = DateTimeProvider.GetNow(),
                    Status = createNewTruck.TruckStatus// todo: default status not provided
                }
            }, cancellationToken);
            await this.Context.SaveChangesAsync(cancellationToken);
        }

        [HttpPut(Name = "UpdateExisting")]
        public async Task UpdateExistingAsync(UpdateTruck updateTruck, CancellationToken cancellationToken = default)
        {
            this.Context.Trucks.Update(new Truck()
            {
                AlphanumericCode = updateTruck.AlphanumericCode,
                Description = updateTruck.Description,
                Name = updateTruck.Name,
                Id = updateTruck.Id,
            });
            await this.Context.SaveChangesAsync(cancellationToken);
        }

        [HttpDelete]
        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            this.Context.Trucks.Remove(new Truck() { Id = id });
            await this.Context.SaveChangesAsync(cancellationToken);
        }
    }
}