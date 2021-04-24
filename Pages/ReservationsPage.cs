using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationProject.Aids;
using ReservationProject.Data;
using ReservationProject.Domain;
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
            v.WorkerName = c.Worker?.LastName;
            v.RoomName = c.Room?.RoomName;
            return v;
        }
        protected internal override ReservationEntity ToEntity(ReservationView c)
        {
            var d = Copy.Members(c, new ReservationData());
            return new ReservationEntity(d);
        }

        //TODO need 2 ei tööta
        public SelectList Rooms =>
            new(
                Db.Rooms.OrderBy(x => x.RoomName).AsNoTracking(),
                "RoomId","RoomName",Item.RoomId);
        public SelectList Workers =>
            new(
                Db.Workers.OrderBy(x => x.LastName).AsNoTracking(),
                "WorkerId","LastName",Item.WorkerId);

        protected internal override async Task LoadRelatedItems(ReservationEntity item)
        {
            if (IsNull(item)) return;
            if (IsNull(Db)) return;
            item.Room = await Db.Rooms.AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == item.RoomId);
            item.Worker = await Db.Workers.AsNoTracking()
                .FirstOrDefaultAsync(w => w.Id == item.WorkerId);
        }


        protected internal override bool RoomAvailable()
        {
            var reservationInDataBase =
                Db?.Reservations.SingleOrDefault( //TODO siia peaks saama, et ta ei ole DB vaid REPO
                    r => r.RoomId == Item.RoomId && r.ReservationDate == Item.ReservationDate && Item.Id != r.Id);
            if (reservationInDataBase != null)
                return false;
            return true;
        }
    }
}
