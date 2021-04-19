using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReservationProject.Aids;
using ReservationProject.Data;
using ReservationProject.Facade;
using ReservationProject.Infra;
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



        //public async Task<IActionResult> OnPostDeleteAsync(string id)
        //{
        //    if (id == "") return NotFound();

        //    Item = ToViewModel(await db.Rooms.FindAsync(id));

        //    if (Item != null)
        //    {
        //        db.Rooms.Remove(ToEntity(Item));
        //        await db.SaveChangesAsync();
        //    }

        //    return RedirectToPage("./Index");
        //}

        //public async Task<IActionResult> OnPostEditAsync(string id)
        //{
        //    if (id == "") return NotFound();

        //    var roomToUpdate = await db.Rooms.FindAsync(id);

        //    if (roomToUpdate == null) return NotFound();

        //    if (await TryUpdateModelAsync(roomToUpdate, "room",
        //        c => c.RoomName, c => c.BuildingAddress))
        //    {
        //        await db.SaveChangesAsync();
        //    }

        //    return RedirectToPage("./Index");
        //}


    }
}
