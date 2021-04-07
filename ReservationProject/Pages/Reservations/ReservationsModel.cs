using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationProject.Data;
using ReservationProject.Infra;
using ReservationProject.Pages;

namespace ReservationProject.Soft.Pages.Reservations
{
    public class ReservationsModel:BasePageModel
    {
        private readonly ApplicationDbContext db;

        public ReservationsModel(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult OnGetCreate()
        {
            return Page();
        }

        [BindProperty]
        public Reservation Reservation { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //TODO kontroll kas olemas?
            db.Reservations.Add(Reservation);
            await db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        public async Task<IActionResult> OnGetDeleteAsync(string id = "")
        {
            if (id == null)
            {
                return NotFound();
            }

            Reservation = await db.Reservations.FirstOrDefaultAsync(m => m.WorkerId == id);

            if (Reservation == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id = "")
        {
            if (id == null)
            {
                return NotFound();
            }

            Reservation = await db.Reservations.FindAsync(id);

            if (Reservation != null)
            {
                db.Reservations.Remove(Reservation);
                await db.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
        public async Task<IActionResult> OnGetDetailsAsync(string id = "")
        {
            if (id == null)
            {
                return NotFound();
            }

            Reservation = await db.Reservations.FirstOrDefaultAsync(m => m.WorkerId == id);

            if (Reservation == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnGetAsync(string id = "")
        {
            if (id == null)
            {
                return NotFound();
            }

            Reservation = await db.Reservations.FirstOrDefaultAsync(m => m.WorkerId == id);

            if (Reservation == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            db.Attach(Reservation).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(Reservation.WorkerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ReservationExists(string id)
        {
            return db.Reservations.Any(e => e.WorkerId == id);
        }
        public IList<Reservation> Reservations { get; set; }

        public async Task OnGetIndexAsync()
        {
            Reservations = await db.Reservations.ToListAsync();
        }
    }
}
