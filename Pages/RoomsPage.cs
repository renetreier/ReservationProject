using ReservationProject.Aids;
using ReservationProject.Data;
using ReservationProject.Domain;
using ReservationProject.Domain.Repos;
using ReservationProject.Facade;
using ReservationProject.Infra;
using ReservationProject.Pages.Common;

namespace ReservationProject.Pages
{
    public class RoomsPage : ViewPage<Room, RoomView>
    {
        public override string PageTitle => "Rooms";

        public RoomsPage(ApplicationDbContext c) : this(new RoomsRepo(c), c) { }

        protected internal RoomsPage(IRoomsRepo r, ApplicationDbContext c = null) : base(r, c) { }
        protected internal override RoomView ToViewModel(Room r)
            => IsNull(r) ? null : Copy.Members(r, new RoomView());

        protected internal override Room ToEntity(RoomView c)
        {
            if (IsNull(c)) return null;
            var d = Copy.Members(c, new RoomData());
            return new Room(d);
        }

    }
}
