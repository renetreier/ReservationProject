using System.Threading.Tasks;
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

        [BindProperty] public TView Item { get; protected set; }
        protected internal virtual async Task LoadRelatedItems(TEntity item) { await Task.CompletedTask; }
        protected internal abstract TView ToViewModel(TEntity e);


        internal async Task<TView> Load(string id)
        {
            var item = await repo.Get(id);
            if (item != null) await LoadRelatedItems(item);
            return ToViewModel(item);
        }
        public IActionResult OnGetCreate()
        {
            DoBeforeCreate();
            return Page();
        }

        public async Task<IActionResult> OnGetDeleteAsync(string id)
        {
            Item = await Load(id);
            return Item is null ? NotFound() : Page();
        }

        public async Task<IActionResult> OnGetDetailsAsync(string id)
        {
            Item = await Load(id);
            return Item is null ? NotFound() : Page();
        }

        public async Task<IActionResult> OnGetEditAsync(string id)
        {
            DoBeforeCreate();
            Item = await Load(id);
            return Item is null ? NotFound() : Page();
        }

        protected internal virtual void DoBeforeCreate() { }

        public string NameSort { get; protected set; }
        public string DateSort { get; protected set; }
        public string CurrentFilter { get; protected set; }
        public string CurrentSort { get; protected set; }
        public virtual bool HasPreviousPage { get; }
        public virtual bool HasNextPage { get; }
        public virtual int PageIndex { get; }
    }

}

