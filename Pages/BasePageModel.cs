using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReservationProject.Core;
using ReservationProject.Domain;
using ReservationProject.Infra;

namespace ReservationProject.Pages
{
    public abstract class BasePageModel : PageModel
    {
        public string ErrorMessage { get; protected set; }
        public abstract string PageTitle { get; }
        [BindProperty] public IEntity Item { get; set; }
        public string NameSort { get; protected set; }
        public string DateSort { get; protected set; }
        public string CurrentFilter { get; protected set; }
        public string CurrentSort { get; protected set; }
        public virtual bool HasPreviousPage { get; protected set; }
        public virtual bool HasNextPage { get; protected set; }
        public virtual int PageIndex { get; protected set; }

    }
    public abstract class BasePageModel<TEntity, TView> : BasePageModel
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
        [BindProperty] public new TView Item
        {
            get => (TView)base.Item;
            set => base.Item = (IEntity)value;
        }

        public IList<TEntity> ItemList { get; set; }
        protected internal virtual async Task LoadRelatedItems(TEntity item) { await Task.CompletedTask; }
        protected internal abstract TView ToViewModel(TEntity e);
        protected internal abstract TEntity ToEntity(TView e);

        protected internal bool IsNull(object o) => o is null;

        internal async Task<TView> Load(string id)
        {
            var item = await repo.Get(id);
            if (!IsNull(id)) await LoadRelatedItems(item);
            return ToViewModel(item);
        }

        public IActionResult OnGetCreate()
        {
            DoBeforeCreate();
            return Page();
        }

        public async Task<IActionResult> OnGetDeleteAsync(string id)
        {
            return IsNull(Item=await Load(id)) ? NotFound() : Page();
        }

        public async Task<IActionResult> OnGetDetailsAsync(string id)
        {
            return IsNull(Item=await Load(id)) ? NotFound() : Page();
        }

        public async Task<IActionResult> OnGetEditAsync(string id)
        {
            if (!IsNull(Item))  DoBeforeCreate();
            return IsNull(Item = await Load(id)) ? NotFound() : Page();
        }

        protected internal virtual void DoBeforeCreate() { }

        public async Task OnGetAsync()
        {
            ItemList = await repo.Get();
        } 

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid) return Page();
            await repo.Add(ToEntity(Item));
            if (!IsNull(db)) await db.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            if (IsNull(id)) return NotFound();
            await repo.Delete(ToEntity(Item));
            if (!IsNull(Item)) await db.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
        public async Task<IActionResult> OnPostEditAsync(string id)
        {
            if (IsNull(id)) return NotFound();
            await repo.Update(ToEntity(Item));
            if (!IsNull(Item)) await db.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

    }
}

