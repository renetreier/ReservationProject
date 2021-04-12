
using ReservationProject.Data;
using ReservationProject.Domain;

namespace ReservationProject.Infra {
    public interface IReservationsRepo : IRepo<Reservation> { }

    public sealed class ReservationsRepo : BaseRepo<Reservation>, IReservationsRepo
    {
        public ReservationsRepo(ApplicationDbContext c) : base(c, c?.Reservations) { }
    }
}










