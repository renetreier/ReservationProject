using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationProject.Data;
using ReservationProject.Infra;

namespace ReservationProject.Soft.Pages.Workers
{
    public class WorkersModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public WorkersModel(ApplicationDbContext context) => _context = context;

        public IActionResult OnGetCreate()
        {
            return Page();
        }
        [BindProperty] public Worker Worker { get; set; }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Worker.WorkerId = Guid.NewGuid().ToString();

            _context.Workers.Add(Worker);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnGetEditAsync(string id)
        {
            if (id == "")
            {
                return NotFound();
            }

            Worker = await _context.Workers.FirstOrDefaultAsync(m => m.WorkerId == id);

            if (Worker == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostEditAsync(string id)
        {
            if (id == "")
                return NotFound();

            var workerToUpdate = await _context.Workers.FindAsync(id);

            if (workerToUpdate == null)
                return NotFound();

            if (await TryUpdateModelAsync(workerToUpdate, "worker",
                c => c.FirstName, c => c.LastName, c => c.Email, c => c.Salary))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            return RedirectToPage("./Index");
        }

        private bool WorkerExists(string id)
        {
            return _context.Workers.Any(e => e.WorkerId == id);
        }

        public async Task<IActionResult> OnGetDeleteAsync(string id)
        {
            if (id == "")
            {
                return NotFound();
            }

            Worker = await _context.Workers.FirstOrDefaultAsync(m => m.WorkerId == id);

            if (Worker == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            //TODO kui kustutad töötaja, kustutaks ka kõik temaga seotud reserveeringud
            if (id == "")
            {
                return NotFound();
            }

            Worker = await _context.Workers.FindAsync(id);

            if (Worker != null)
            {
                _context.Workers.Remove(Worker);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnGetDetailsAsync(string id)
        {
            if (id == "")
            {
                return NotFound();
            }

            Worker = await _context.Workers.FirstOrDefaultAsync(m => m.WorkerId == id);

            if (Worker == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IList<Worker> WorkerList { get; set; }

        public async Task OnGetAsync()
        {
            WorkerList = await _context.Workers.ToListAsync();
        }
    }
}
