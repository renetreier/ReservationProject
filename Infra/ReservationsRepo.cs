using System.Linq;
using System.Threading.Tasks;
using ReservationProject.Core;
using ReservationProject.Data;
using ReservationProject.Domain;
using ReservationProject.Domain.Repos;
using ReservationProject.Infra.Common;

namespace ReservationProject.Infra {

    public sealed class ReservationsRepo : PagedRepo<Reservation,ReservationData>, IReservationsRepo
    {
        public ReservationsRepo(){}
        public ReservationsRepo(ApplicationDbContext c) : base(c, c?.Reservations) { }
        public override Reservation ToEntity(ReservationData d) => new(d);
        public override ReservationData ToData(Reservation e) => e?.Data ?? new ReservationData();
       
        public override async Task<bool> AddAsync(Reservation e)
        {
            if (IsRoomAvailable(e)) return await base.AddAsync(e);
            ErrorMessage = ErrorMessages.RoomNotFree;
            return false;

        }
        public override async Task<bool> UpdateAsync(Reservation e)
        {
            if (IsRoomAvailable(e)) return await base.UpdateAsync(e);
            ErrorMessage = ErrorMessages.RoomNotFree;
            return false;
        }

        internal bool IsRoomAvailable(Reservation e)
        {
            var reservationInDataBase = Set.SingleOrDefault(
                r => r.RoomId == e.RoomId && r.ReservationDate == e.ReservationDate && e.Id != r.Id);
            return reservationInDataBase == null;
        }
    }
}











