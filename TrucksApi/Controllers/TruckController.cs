using Microsoft.AspNetCore.Mvc;
using TrucksApi.Data;
using TrucksApi.Mappers;
using TrucksApi.Requests;
using TrucksApi.Responses;

namespace TrucksApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TruckController : ControllerBase
    {
        public IDateTimeProvider DateTimeProvider { get; }
        public ITrucksRepository TrucksRepository { get; }
        public ITruckMapper TruckMapper { get; }

        public TruckController(
            IDateTimeProvider dateTimeProvider,
            ITrucksRepository trucksRepository,
            ITruckMapper truckMapper)
        {
            DateTimeProvider = dateTimeProvider;
            TrucksRepository = trucksRepository;
            TruckMapper = truckMapper;
        }

        [HttpGet(Name = "GetList")]
        public async Task<IEnumerable<GetAllTrucksResponseItem>> GetListAsync(
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
            CreateNewTruckRequest createNewTruck,
            CancellationToken cancellationToken = default)
        {
            Truck truck = TruckMapper.MapFromCreateNewTruck(createNewTruck);
            await this.TrucksRepository.AddAsync(truck, cancellationToken);
            await this.TrucksRepository.SaveChangesAsync(cancellationToken);
        }

        [HttpPut(Name = "UpdateExisting")]
        public async Task UpdateExistingAsync(UpdateTruckRequest updateTruck, CancellationToken cancellationToken = default)
        {
            Truck truck = TruckMapper.MapFromUpdateTruck(updateTruck);
            this.TrucksRepository.Update(new Truck()
            {
                AlphanumericCode = updateTruck.AlphanumericCode,
                Description = updateTruck.Description,
                Name = updateTruck.Name,
                Id = updateTruck.Id,
            });
            await this.TrucksRepository.SaveChangesAsync(cancellationToken);
        }

        [HttpDelete]
        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            Truck truck = TruckMapper.MapFromId(id);
            this.TrucksRepository.Remove(truck);
            await this.TrucksRepository.SaveChangesAsync(cancellationToken);
        }
    }
}