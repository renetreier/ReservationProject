using System.Linq;
using ReservationProject.Data;
using ReservationProject.Domain;
using ReservationProject.Domain.Repos;
using ReservationProject.Facade;
using ReservationProject.Infra.Common;

namespace ReservationProject.Infra {

    public sealed class ReservationsRepo : PagedRepo<ReservationEntity,ReservationData>, IReservationsRepo
    {
        public ReservationsRepo(ApplicationDbContext c) : base(c, c?.Reservations) { }
        protected override ReservationEntity ToEntity(ReservationData d) => new(d);
        protected override ReservationData ToData(ReservationEntity e) => e?.Data ?? new ReservationData();
        //TODO siin ka veel kala, peaks saama x.roomname.contains ja sama workeriga
        protected internal override IQueryable<ReservationData> ApplyFilters(IQueryable<ReservationData> query)
        {
            if (SearchString is null) return query;
            return query.Where(
                x => x.ReservationDate.ToString().Contains(SearchString) ||
                     x.RoomId.Contains(SearchString) ||
                     x.WorkerId.Contains(SearchString));
        }
        //TODO Renksu kala
        //public override async Task<List<ReservationData>> Get()
        //{
        //    return await Set.AsNoTracking().OrderBy(r=>r.ReservationDate).Include(c => c.ReservedRoom)
        //        .Include(c => c.ReservedWorker).ToListAsync();
        //}
    }
}











