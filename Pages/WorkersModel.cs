using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationProject.Data;
using ReservationProject.Infra;

namespace ReservationProject.Pages
{
    public class WorkersModel : BasePageModel
    {
        private readonly ApplicationDbContext db;

        public WorkersModel(ApplicationDbContext context) => db = context;

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

            db.Workers.Add(Worker);
            await db.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnGetEditAsync(string id)
        {
            if (id == "")
            {
                return NotFound();
            }

            Worker = await db.Workers.FirstOrDefaultAsync(m => m.WorkerId == id);

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

            var workerToUpdate = await db.Workers.FindAsync(id);

            if (workerToUpdate == null)
                return NotFound();

            if (await TryUpdateModelAsync(workerToUpdate, "worker",
                c => c.FirstName, c => c.LastName, c => c.Email, c => c.Salary))
            {
                await db.SaveChangesAsync();
            }
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnGetDeleteAsync(string id)
        {
            if (id == "")
            {
                return NotFound();
            }

            Worker = await db.Workers.FirstOrDefaultAsync(m => m.WorkerId == id);

            if (Worker == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {

            //TODO kui kustutad töötaja, kustutaks ka kõik temaga seotud reserveeringud
            // TODO "Rene" mulle tundub et see töötab, kui kustutad ära siis reserveering kustub ka

            if (id == "")
            {
                return NotFound();
            }

            Worker = await db.Workers.FindAsync(id);

            if (Worker != null)
            {
                db.Workers.Remove(Worker);
                await db.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnGetDetailsAsync(string id)
        {
            if (id == "")
            {
                return NotFound();
            }

            Worker = await db.Workers.FirstOrDefaultAsync(m => m.WorkerId == id);

            if (Worker == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IList<Worker> WorkerList { get; set; }

        public async Task OnGetAsync()
        {
            WorkerList = await db.Workers.ToListAsync();
        }
    }
}
