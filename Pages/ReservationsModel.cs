using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationProject.Aids;
using ReservationProject.Data;
using ReservationProject.Facade;
using ReservationProject.Infra;
using ReservationProject.Pages.Common;

namespace ReservationProject.Pages
{
    public class ReservationsModel:BasePageModel<Reservation, ReservationView>
    {
        public override string PageTitle => "Reservation";

        public ReservationsModel(ApplicationDbContext c) : this(new ReservationsRepo(c), c) { }
        protected internal ReservationsModel(IReservationsRepo r, ApplicationDbContext c = null): base(r, c) { }

        protected internal override ReservationView ToViewModel(Reservation r)
            => IsNull(r) ? null : Copy.Members(r, new ReservationView());

        protected internal override Reservation ToEntity(ReservationView r)
            => IsNull(r) ? null : Copy.Members(r, new Reservation());


        public SelectList Rooms =>
            new(
                Db.Rooms.OrderBy(x => x.RoomName).AsNoTracking(),
                nameof(Item.ReservedRoom.Id),
                nameof(Item.ReservedRoom.RoomName),
                Item?.RoomId);
        public SelectList Workers =>
            new(
                Db.Workers.OrderBy(x => x.LastName).AsNoTracking(),
                nameof(Item.ReservedWorker.Id),
                nameof(Item.ReservedWorker.FullName),
                Item?.WorkerId);

        protected internal override async Task LoadRelatedItems(Reservation item)
        {
            if (IsNull(item)) return;
            if (IsNull(Db)) return;
            item.ReservedRoom = await Db.Rooms.AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == item.RoomId);
            item.ReservedWorker = await Db.Workers.AsNoTracking()
                .FirstOrDefaultAsync(w => w.Id == item.WorkerId);
        }

        protected internal override bool RoomAvailable()
        {
            var reservationInDataBase = Db.Reservations.SingleOrDefault(//TODO siia peaks saama, et ta ei ole DB vaid REPO
                r => r.RoomId == Item.RoomId && r.ReservationDate == Item.ReservationDate);
            if (reservationInDataBase != null)
                return false;
            return true;
        }

        //protected internal override void DoBeforeCreate(dynamic itemToChange)
        //{
        //    ItemList = Db.Reservations.AsNoTracking().ToList();
        //    foreach (var item in ItemList)
        //    {
        //        if (item.RoomId == itemToChange.RoomId && item.ReservationDate == itemToChange.ReservationDate)
        //        {
        //            ErrorMessage = "ei ole vaba aeg";
        //            RedirectToPage("./Index");
        //            break;
        //        }
        //    }
        //}



    }
}
