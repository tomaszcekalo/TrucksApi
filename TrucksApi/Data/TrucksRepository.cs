using Microsoft.EntityFrameworkCore;
using TrucksApi.Models;

namespace TrucksApi.Data
{
    public interface ITrucksRepository
    {
        Task<IEnumerable<GetAllTrucksItem>> GetListAsync(
            string AlhpanumericCodeFilter,
            string NameFilter,
            TruckStatusEnum[] truckStatusFilter = null,
            OrderTrucksBy orderBy = OrderTrucksBy.AlphanumericCode,
            CancellationToken cancellationToken = default);

    }
    public class TrucksRepository : ITrucksRepository
    {
        public TrucksRepository(TrucksDb context)
        {
            Context = context;
        }

        public TrucksDb Context { get; }
        public async Task<IEnumerable<GetAllTrucksItem>> GetListAsync(
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
            switch(orderBy)
            {
                case OrderTrucksBy.AlphanumericCode:
                    query=query.OrderBy(x => x.AlphanumericCode);
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

            return await query.Select(x => new GetAllTrucksItem
            {
                Id = x.Id,
                Name = x.Name,
                AlphanumericCode = x.AlphanumericCode,
                TruckStatusFriendlyString = x.Status.Status.ToFriendlyString(),
            }).ToListAsync(cancellationToken);
        }
    }
}
