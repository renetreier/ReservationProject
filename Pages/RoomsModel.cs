using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ReservationProject.Data;
using ReservationProject.Infra;
using ReservationProject.Pages;
//TODO Vaja puhastada ja refaktoorida
namespace ReservationProject.Pages
{
    public class RoomsModel:BasePageModel
    {
        private readonly ApplicationDbContext db;

        public RoomsModel(ApplicationDbContext context)=> db = context;
        public IActionResult OnGetCreate()
        {
            return Page();
        }

        [BindProperty]
        public Room Room { get; set; }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Room.RoomId = Guid.NewGuid().ToString();

            db.Rooms.Add(Room);
            await db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        public async Task<IActionResult> OnGetDeleteAsync(string id)
        {
            if (id == "")
            {
                return NotFound();
            }

            Room = await db.Rooms.FirstOrDefaultAsync(m => m.RoomId == id);

            if (Room == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            if (id == "")
            {
                return NotFound();
            }

            Room = await db.Rooms.FindAsync(id);

            if (Room != null)
            {
                db.Rooms.Remove(Room);
                await db.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
        public async Task<IActionResult> OnGetDetailsAsync(string id)
        {
            if (id =="")
            {
                return NotFound();
            }

            Room = await db.Rooms.FirstOrDefaultAsync(m => m.RoomId == id);

            if (Room == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnGetEditAsync(string id)
        {
            if (id == "")
            {
                return NotFound();
            }

            Room = await db.Rooms.FirstOrDefaultAsync(m => m.RoomId == id);

            if (Room == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostEditAsync(string id)
        {
            if (id == "")
                return NotFound();

            var roomToUpdate = await db.Rooms.FindAsync(id);

            if (roomToUpdate == null)
                return NotFound();

            if (await TryUpdateModelAsync(roomToUpdate, "room",
                c => c.RoomName, c => c.BuildingAddress))
            {
                await db.SaveChangesAsync();
            }
            return RedirectToPage("./Index");
        }

        public IList<Room> RoomList { get; set; }

        public async Task OnGetAsync()
        {
            RoomList = await db.Rooms.ToListAsync();
        }
    }
}
