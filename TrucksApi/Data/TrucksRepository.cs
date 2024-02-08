using Microsoft.EntityFrameworkCore;
using TrucksApi.Responses;

namespace TrucksApi.Data
{
    public interface ITrucksRepository
    {
        Task AddAsync(Truck truck, CancellationToken cancellationToken);

        Task<IEnumerable<GetAllTrucksResponseItem>> GetListAsync(
            string AlhpanumericCodeFilter,
            string NameFilter,
            TruckStatusEnum[] truckStatusFilter = null,
            OrderTrucksBy orderBy = OrderTrucksBy.AlphanumericCode,
            CancellationToken cancellationToken = default);

        void Remove(Truck truck);

        Task SaveChangesAsync(CancellationToken cancellationToken);

        void Update(Truck truck);
    }

    public class TrucksRepository : ITrucksRepository
    {
        public TrucksRepository(TrucksDb context)
        {
            Context = context;
        }

        public TrucksDb Context { get; }

        public async Task AddAsync(Truck truck, CancellationToken cancellationToken)
        {
            await Context.Trucks.AddAsync(truck, cancellationToken);
        }

        public async Task<IEnumerable<GetAllTrucksResponseItem>> GetListAsync(
            string AlhpanumericCodeFilter,
            string NameFilter,
            TruckStatusEnum[] truckStatusFilter = null,
            OrderTrucksBy orderBy = OrderTrucksBy.AlphanumericCode,
            CancellationToken cancellationToken = default)
        {
            var query = this.Context.Trucks.Where(x => true);
            if (!string.IsNullOrWhiteSpace(AlhpanumericCodeFilter))
            {
                query = query.Where(x => x.AlphanumericCode.Contains(AlhpanumericCodeFilter));
            }
            if (!string.IsNullOrWhiteSpace(NameFilter))
            {
                query = query.Where(x => x.Name.Contains(NameFilter));
            }
            if (truckStatusFilter is not null && truckStatusFilter.Count() > 0)
            {
                query = query.Where(x => truckStatusFilter.Contains(x.Status.Status));
            }
            switch (orderBy)
            {
                case OrderTrucksBy.AlphanumericCode:
                    query = query.OrderBy(x => x.AlphanumericCode);
                    break;

                case OrderTrucksBy.AlphanumericCodeDescending:
                    query = query.OrderByDescending(x => x.AlphanumericCode);
                    break;

                case OrderTrucksBy.Name:
                    query = query.OrderBy(x => x.AlphanumericCode);
                    break;

                case OrderTrucksBy.NameDescending:
                    query = query.OrderByDescending(x => x.AlphanumericCode);
                    break;

                case OrderTrucksBy.Status:
                    query = query.OrderBy(x => x.Status);
                    break;

                case OrderTrucksBy.StatusDescending:
                    query = query.OrderByDescending(x => x.Status);
                    break;
            }

            return await query.Select(x => new GetAllTrucksResponseItem
            {
                Id = x.Id,
                Name = x.Name,
                AlphanumericCode = x.AlphanumericCode,
                TruckStatusFriendlyString = x.Status.Status.ToFriendlyString(),
            }).ToListAsync(cancellationToken);
        }

        public void Remove(Truck truck)
        {
            Context.Trucks.Remove(truck);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await Context.SaveChangesAsync(cancellationToken);
        }

        public void Update(Truck truck)
        {
            this.Context.Trucks.Update(truck);
        }
    }
}