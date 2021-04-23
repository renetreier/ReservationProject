
using ReservationProject.Data;
using ReservationProject.Domain;
using ReservationProject.Infra.Common;

namespace ReservationProject.Infra {
    public interface IRoomsRepo : IRepo<Room> { }

    public sealed class RoomsRepo : BaseRepo<Room>, IRoomsRepo
    {
        public RoomsRepo(ApplicationDbContext c) : base(c, c?.Rooms) { }
    }
}










