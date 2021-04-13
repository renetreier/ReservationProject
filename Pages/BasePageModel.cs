using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReservationProject.Core;
using ReservationProject.Domain;
using ReservationProject.Infra;

namespace ReservationProject.Pages
{
    public abstract class BasePageModel<TEntity, TView> : PageModel
        where TEntity : class, IEntity, new()
        where TView : class, new()
    {
        protected readonly ApplicationDbContext db;
        protected readonly IRepo<TEntity> repo;

        protected BasePageModel(IRepo<TEntity> r, ApplicationDbContext c = null)
        {
            db = c;
            repo = r;
        }

        //protected readonly Infra.ApplicationDbContext dataBase;
        //public BasePageModel(Infra.ApplicationDbContext context) => dataBase = context;
        //private Client Client { get; set; }
        public string NameSort { get; protected set; }
        public string DateSort { get; protected set; }
        public string CurrentFilter { get; protected set; }
        public string CurrentSort { get; protected set; }
        public virtual bool HasPreviousPage { get; }
        public virtual bool HasNextPage { get; }

        public virtual int PageIndex { get; }
        //[BindProperty] public IEntityData Item { get; set; }


        public IActionResult OnGetCreate()
        {
            DoBeforeCreate();
            return Page();
        }

        protected internal virtual void DoBeforeCreate()
        {

        }
    }

}

