using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationProject.Data;
using ReservationProject.Infra;

namespace ReservationProject.Pages
{
    public class WorkersModel : BasePageModel<Worker, Worker>
    {
        public WorkersModel(ApplicationDbContext c) : this(new WorkersRepo(c), c) { }
        protected internal WorkersModel(IWorkersRepo r, ApplicationDbContext c = null) : base(r, c) { }

        //public IActionResult OnGetCreate()=> Page();

        [BindProperty] public Worker Worker { get; set; }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid) return Page();

            Worker.Id = Guid.NewGuid().ToString();

            db.Workers.Add(Worker);
            await db.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnGetEditAsync(string id)
        {
            Worker = await repo.Get(id);
            return Worker is null ? NotFound() : Page();
        }

        public async Task<IActionResult> OnPostEditAsync(string id)
        {
            if (id == "") return NotFound();

            var workerToUpdate = await db.Workers.FindAsync(id);

            if (workerToUpdate == null) return NotFound();

            if (await TryUpdateModelAsync(workerToUpdate, "worker",
                c => c.FirstName, c => c.LastName, c => c.Email, c => c.Salary))
            {
                await db.SaveChangesAsync();
            }
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {

            //TODO kui kustutad töötaja, kustutaks ka kõik temaga seotud reserveeringud
            // TODO "Rene" mulle tundub et see töötab, kui kustutad ära siis reserveering kustub ka

            if (id == "") return NotFound();

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
            Worker = await repo.Get(id);
            return Worker is null ? NotFound() : Page();
        }

        public IList<Worker> WorkerList { get; set; }

        public async Task OnGetAsync()
        {
            WorkerList = await repo.Get();
        }

        protected internal override Worker ToViewModel(Worker e) => e;

    }
}
