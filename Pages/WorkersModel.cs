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
        protected internal override Worker ToViewModel(Worker w) => w;
        protected internal override Worker ToEntity(Worker w) => w;
        
       

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid) return Page();

            Item.Id = Guid.NewGuid().ToString();

            db.Workers.Add(Item);
            await db.SaveChangesAsync();
            return RedirectToPage("./Index");
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

            Item = await db.Workers.FindAsync(id);

            if (Item != null)
            {
                db.Workers.Remove(Item);
                await db.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }


        

    }
}
