using System.Collections.Generic;
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

        [BindProperty] public TView Item { get; set; }
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
            
            if (Item != null)  DoBeforeCreate();
            return IsNull(Item = await Load(id)) ? NotFound() : Page();
        }

        protected internal virtual void DoBeforeCreate() { }
        public async Task OnGetAsync()=> ItemList = await repo.Get();
    }

}

