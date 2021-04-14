using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationProject.Data;
using ReservationProject.Infra;
//TODO Vaja puhastada ja refaktoorida
namespace ReservationProject.Pages
{
    public class RoomsModel : BasePageModel<Room, Room>
    {
        public RoomsModel(ApplicationDbContext c) : this(new RoomsRepo(c), c) { }

        protected internal RoomsModel(IRoomsRepo r, ApplicationDbContext c = null) : base(r, c) { }
        protected internal override Room ToViewModel(Room r) => r;
        protected internal override Room ToEntity(Room r) => r;


        //[BindProperty] 

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid) return Page();

            Item.Id = Guid.NewGuid().ToString();

            db.Rooms.Add(Item);
            await db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            if (id == "") return NotFound();

            Item = await db.Rooms.FindAsync(id);

            if (Item != null)
            {
                db.Rooms.Remove(Item);
                await db.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostEditAsync(string id)
        {
            if (id == "") return NotFound();

            var roomToUpdate = await db.Rooms.FindAsync(id);

            if (roomToUpdate == null) return NotFound();

            if (await TryUpdateModelAsync(roomToUpdate, "room",
                c => c.RoomName, c => c.BuildingAddress))
            {
                await db.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }


    }
}
