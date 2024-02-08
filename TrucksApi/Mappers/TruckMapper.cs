using TrucksApi.Data;
using TrucksApi.Requests;

namespace TrucksApi.Mappers
{
    public interface ITruckMapper
    {
        Truck MapFromCreateNewTruck(CreateNewTruckRequest createNewTruck);

        Truck MapFromId(int id);

        Truck MapFromUpdateTruck(UpdateTruckRequest updateTruck);
    }

    public class TruckMapper : ITruckMapper
    {
        public TruckMapper(IDateTimeProvider dateTimeProvider)
        {
            DateTimeProvider = dateTimeProvider;
        }

        public IDateTimeProvider DateTimeProvider { get; }

        public Truck MapFromCreateNewTruck(CreateNewTruckRequest createNewTruck)
        {
            return new Truck()
            {
                AlphanumericCode = createNewTruck.AlphanumericCode,
                Description = createNewTruck.Description,
                Name = createNewTruck.Name,
                Status = new TruckStatus()
                {
                    CreatedDate = DateTimeProvider.GetNow(),
                    Status = createNewTruck.TruckStatus// todo: default status not provided
                }
            };
        }

        public Truck MapFromId(int id)
        {
            return new Truck() { Id = id };
        }

        public Truck MapFromUpdateTruck(UpdateTruckRequest updateTruck)
        {
            return new Truck()
            {
                AlphanumericCode = updateTruck.AlphanumericCode,
                Description = updateTruck.Description,
                Name = updateTruck.Name,
                Id = updateTruck.Id,
            };
        }
    }
}