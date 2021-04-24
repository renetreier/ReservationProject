using ReservationProject.Aids;
using ReservationProject.Data;
using ReservationProject.Domain;
using ReservationProject.Domain.Repos;
using ReservationProject.Facade;
using ReservationProject.Infra;
using ReservationProject.Pages.Common;

namespace ReservationProject.Pages
{
    public class RoomsPage : ViewPage<RoomEntity, RoomView>
    {
        public override string PageTitle => "Rooms";

        public RoomsPage(ApplicationDbContext c) : this(new RoomsRepo(c), c) { }

        protected internal RoomsPage(IRoomsRepo r, ApplicationDbContext c = null) : base(r, c) { }
        protected internal override RoomView ToViewModel(RoomEntity r)
            => IsNull(r) ? null : Copy.Members(r, new RoomView());

        protected internal override RoomEntity ToEntity(RoomView r)
            => IsNull(r) ? null : Copy.Members(r, new RoomEntity());

    }
}
