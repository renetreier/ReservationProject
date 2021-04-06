using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationProject.Soft.Pages.Rooms
{
    public class RoomsModel
    {
        private readonly Infra.ApplicationDbContext _context;

        public RoomsModel(Infra.ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
