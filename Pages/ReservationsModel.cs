using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationProject.Data;
using ReservationProject.Infra;

namespace ReservationProject.Pages
{
    public class ReservationsModel:BasePageModel<Reservation, Reservation>
    {
        public ReservationsModel(ApplicationDbContext c) : this(new ReservationsRepo(c), c) { }
        protected internal ReservationsModel(IReservationsRepo r, ApplicationDbContext c = null): base(r, c) { }

        protected internal override Reservation ToViewModel(Reservation r) => r;
        protected internal override Reservation ToEntity(Reservation r) => r;

      


        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid) return Page();

            Item.Id = Guid.NewGuid().ToString();
            //TODO kontroll kas olemas?
            db.Reservations.Add(Item);
            await db.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            if (id == "") return NotFound();

            Item = await db.Reservations.FindAsync(id);

            if (Item != null)
            {
                db.Reservations.Remove(Item);
                await db.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        //public async Task<IActionResult> OnGetEditAsync(string id)
        //{

        //    //if (id == "") return NotFound();

        //    //LoadRooms(db);
        //    //LoadWorkers(db);
        //    //Reservation = await db.Reservations.FirstOrDefaultAsync(m => m.Id == id);

        //    //if (Reservation == null) return NotFound();

        //    //return Page();
        //    Reservation = await repo.Get(id);
        //    return Reservation is null ? NotFound() : Page();
        //}

        public async Task<IActionResult> OnPostEditAsync(string id)
        {
            if (id == "") return NotFound();

            var reservationToUpdate = await db.Reservations.FindAsync(id);

            if (reservationToUpdate == null) return NotFound();

            if (await TryUpdateModelAsync(reservationToUpdate, "reservation",
                    c => c.ReservationDate, c => c.RoomId, c => c.WorkerId))
            {
                    await db.SaveChangesAsync();
            }
            return RedirectToPage("./Index");
        }


        public SelectList Rooms =>
            new(
                db.Rooms.OrderBy(x => x.RoomName).AsNoTracking(),
                nameof(Item.ReservedRoom.Id),
                nameof(Item.ReservedRoom.RoomName),
                Item?.RoomId);
        public SelectList Workers =>
            new(
                db.Workers.OrderBy(x => x.LastName).AsNoTracking(),
                nameof(Item.ReservedWorker.Id),
                nameof(Item.ReservedWorker.FullName),
                Item?.WorkerId);

        protected internal override async Task LoadRelatedItems(Reservation item)
        {
            if (isNull(item)) return;
            if (isNull(db)) return;
            item.ReservedRoom = await db.Rooms.AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == item.RoomId);
            item.ReservedWorker = await db.Workers.AsNoTracking()
                .FirstOrDefaultAsync(w => w.Id == item.WorkerId);
        }
        
    }
}
