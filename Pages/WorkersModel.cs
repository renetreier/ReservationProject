using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReservationProject.Aids;
using ReservationProject.Data;
using ReservationProject.Facade;
using ReservationProject.Infra;

namespace ReservationProject.Pages
{
    public class WorkersModel : BasePageModel<Worker, WorkerView>
    {
        public WorkersModel(ApplicationDbContext c) : this(new WorkersRepo(c), c) { }
        protected internal WorkersModel(IWorkersRepo r, ApplicationDbContext c = null) : base(r, c) { }
        protected internal override WorkerView ToViewModel(Worker w)
            => IsNull(w) ? null : Copy.Members(w, new WorkerView());
        
        protected internal override Worker ToEntity(WorkerView w)
            => IsNull(w) ? null : Copy.Members(w, new Worker());


        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid) return Page();

            Item.Id = Guid.NewGuid().ToString();

            db.Workers.Add(ToEntity(Item));
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

            Item = ToViewModel(await db.Workers.FindAsync(id));

            if (Item != null)
            {
                db.Workers.Remove(ToEntity(Item));
                await db.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }


        

    }
}
