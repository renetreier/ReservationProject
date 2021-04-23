using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReservationProject.Aids;
using ReservationProject.Data;
using ReservationProject.Facade;
using ReservationProject.Infra;
using ReservationProject.Pages.Common;

//TODO Vaja puhastada ja refaktoorida
namespace ReservationProject.Pages
{
    public class RoomsModel : BasePageModel<Room, RoomView>
    {
        public override string PageTitle => "Room";

        public RoomsModel(ApplicationDbContext c) : this(new RoomsRepo(c), c) { }

        protected internal RoomsModel(IRoomsRepo r, ApplicationDbContext c = null) : base(r, c) { }
        protected internal override RoomView ToViewModel(Room r)
            => IsNull(r) ? null : Copy.Members(r, new RoomView());

        protected internal override Room ToEntity(RoomView r)
            => IsNull(r) ? null : Copy.Members(r, new Room());

    }
}
