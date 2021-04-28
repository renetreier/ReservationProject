using System.Linq;
using ReservationProject.Data;
using ReservationProject.Domain;
using ReservationProject.Domain.Repos;
using ReservationProject.Infra.Common;

namespace ReservationProject.Infra {

    public sealed class RoomsRepo : PagedRepo<RoomEntity, RoomData>, IRoomsRepo
    {
        public RoomsRepo(ApplicationDbContext c) : base(c, c?.Rooms) { }
        protected override RoomEntity ToEntity(RoomData d) => new(d);
        protected override RoomData ToData(RoomEntity e) =>  e?.Data ?? new RoomData() ;

        protected internal override IQueryable<RoomData> ApplyFilters(IQueryable<RoomData> query)
        {
            if (SearchString is null) return query;
            return query.Where(
                x => x.RoomName.Contains(SearchString) ||
                     x.BuildingAddress.Contains(SearchString));
        }

    }
}










