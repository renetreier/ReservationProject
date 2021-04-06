using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ReservationProject.Data;
using ReservationProject.Infra;

namespace ReservationProject.Soft.Pages.Rooms
{
    public class DetailsModel : PageModel
    {
        private readonly ReservationProject.Infra.ApplicationDbContext _context;

        public DetailsModel(ReservationProject.Infra.ApplicationDbContext context)
        {
            _context = context;
        }

        public Room Room { get; set; }

        public async Task<IActionResult> OnGetAsync(string id="")
        {
            if (id == null)
            {
                return NotFound();
            }

            Room = await _context.Rooms.FirstOrDefaultAsync(m => m.RoomId == id);

            if (Room == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
