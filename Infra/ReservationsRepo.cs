using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ReservationProject.Core;
using ReservationProject.Data;
using ReservationProject.Domain;
using ReservationProject.Domain.Repos;
using ReservationProject.Infra.Common;

namespace ReservationProject.Infra {

    public sealed class ReservationsRepo : PagedRepo<ReservationEntity,ReservationData>, IReservationsRepo
    {
        public ReservationsRepo(ApplicationDbContext c) : base(c, c?.Reservations) { }
        protected override ReservationEntity ToEntity(ReservationData d) => new(d);
        protected override ReservationData ToData(ReservationEntity e) => e?.Data ?? new ReservationData();
        protected internal override IQueryable<ReservationData> ApplyFilters(IQueryable<ReservationData> query)
        {
            if (SearchString is null) return query;
            return query.Where(
                x => x.ReservationDate.ToString(CultureInfo.InvariantCulture).Contains(SearchString) ||
                     x.RoomId.Contains(SearchString) ||
                     x.WorkerId.Contains(SearchString));
        }

        public override async Task<bool> Add(ReservationEntity e)
        {
            if (IsRoomAvailable(e)) return await base.Add(e);
            ErrorMessage = ErrorMessages.RoomNotFree;
            return false;

        }

        public override async Task<bool> Update(ReservationEntity e)
        {
            if (IsRoomAvailable(e)) return await base.Update(e);
            ErrorMessage = ErrorMessages.RoomNotFree;
            return false;
        }

        internal  bool IsRoomAvailable(ReservationEntity e)
        {
            var reservationInDataBase = Set.SingleOrDefault(
                r => r.RoomId == e.RoomId && r.ReservationDate == e.ReservationDate && e.Id != r.Id);
            if (reservationInDataBase == null) return true;
            return false;
        }
    }
}











