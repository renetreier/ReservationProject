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

        public SelectList Rooms =>
            new(
                Db.Rooms.OrderBy(x => x.RoomName).AsNoTracking(),
                "Id","RoomName",Item?.RoomId);
        public SelectList Workers =>
            new(
                Db.Workers.OrderBy(x => x.LastName).AsNoTracking(),
                "Id","LastName",Item?.WorkerId);

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
