
using ReservationProject.Data;
using ReservationProject.Domain;

namespace ReservationProject.Infra {
    public interface IRoomsRepo : IRepo<Room> { }

    public sealed class RoomsRepo : BaseRepo<Room>, IRoomsRepo
    {
        public RoomsRepo(ApplicationDbContext c) : base(c, c?.Rooms) { }
    }
}










