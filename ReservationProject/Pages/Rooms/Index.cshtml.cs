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
    public class IndexModel : PageModel
    {
        private readonly ReservationProject.Infra.ApplicationDbContext _context;

        public IndexModel(ReservationProject.Infra.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Room> Room { get;set; }

        public async Task OnGetAsync()
        {
            Room = await _context.Rooms.ToListAsync();
        }
    }
}
