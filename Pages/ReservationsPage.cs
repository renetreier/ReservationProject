using Microsoft.AspNetCore.Mvc.Rendering;
using ReservationProject.Aids;
using ReservationProject.Data;
using ReservationProject.Domain;
using ReservationProject.Domain.Common;
using ReservationProject.Domain.Repos;
using ReservationProject.Facade;
using ReservationProject.Infra;
using ReservationProject.Pages.Common;

namespace ReservationProject.Pages
{
    public class ReservationsPage:ViewPage<ReservationEntity, ReservationView>
    {
        public override string PageTitle => "Reservations";

        public ReservationsPage(ApplicationDbContext c) : this(new ReservationsRepo(c), c) { }
        protected internal ReservationsPage(IReservationsRepo r, ApplicationDbContext c = null): base(r, c) { }

        protected internal override ReservationView ToViewModel(ReservationEntity c)
        {
            if (IsNull(c)) return null;
            var v = Copy.Members(c.Data, new ReservationView());
            v.WorkerName = c.ReservedWorker?.FullName;
            v.RoomName = c.ReservedRoom?.RoomName;
            return v;
        }
        protected internal override ReservationEntity ToEntity(ReservationView c)
        {
            if (IsNull(c)) return null;
            var d = Copy.Members(c, new ReservationData());
            return new ReservationEntity(d);
        }

        public SelectList Rooms
        {
            get
            {
                var list = new GetRepo().Instance<IRoomsRepo>().Get();
                return new SelectList(list, "Id", "RoomName", Item?.RoomId);
            }
        }

        public SelectList Workers
        {
            get
            {
                var list = new GetRepo().Instance<IWorkersRepo>().Get();
                return new SelectList(list, "Id", "FullName", Item?.WorkerId);
            }
        }
    }
}
