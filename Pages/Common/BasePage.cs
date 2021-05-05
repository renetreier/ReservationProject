using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReservationProject.Core;
using ReservationProject.Core.Extensions;
using ReservationProject.Domain.Repos;
using ReservationProject.Infra;

namespace ReservationProject.Pages.Common
{
    public abstract class BasePage : PageModel,IBasePage
    {
        public string ErrorMessage { get; protected set; }
        public abstract string PageTitle { get; }
        [BindProperty] public IEntityData Item { get; set; }

        public virtual string PageUrl => PageTitle;
        public virtual string SortOrder { get; set; }
        public virtual string CurrentFilter { get; set; }
        public virtual string SearchString { get; set; }
        public virtual bool HasPreviousPage { get; protected set; }
        public virtual bool HasNextPage { get; protected set; }
        public virtual int? PageIndex { get; set; }
        public abstract string CurrentSort { get; }
    }
    public abstract class BasePageModel<TEntity, TView> : BasePage
        where TEntity : class, IBaseEntity, new()
        where TView : class, IEntityData, new()
    {
        protected readonly ApplicationDbContext Db;
        protected readonly IRepo<TEntity> Repo;
       

        protected BasePageModel(IRepo<TEntity> r, ApplicationDbContext c = null)
        {
            Db = c;
            Repo = r;
        }
        [BindProperty] public new TView Item
        {
            get => (TView)base.Item;
            set => base.Item = value;
        }

        protected internal virtual async Task LoadRelatedItems(TEntity item) { await Task.CompletedTask; }
        protected internal string SetConcurrencyMsg(bool isError) => isError ? ErrorMessages.ConcurrencyOnDelete : null;
        protected internal abstract TView ToViewModel(TEntity e);
        protected internal abstract TEntity ToEntity(TView e);
        protected internal bool IsNull(object o) => o is null;

        internal async Task<TView> Load(string id, bool concurrencyError = false)
        {
            var item = await Repo.GetAsync(id);
            ErrorMessage = SetConcurrencyMsg(concurrencyError);
            return ToViewModel(item);
        }

        internal async Task<bool> Remove() =>
            !IsNull(Repo) && await Repo.DeleteAsync(ToEntity(Item));
        internal async Task<bool> Update() =>
            !IsNull(Repo) && await Repo.UpdateAsync(ToEntity(Item));

        public IActionResult OnGetCreate()
        {
            DoBeforeCreate();
            return Page();
        }

        public async Task<IActionResult> OnGetDeleteAsync(string id, bool concurrencyError = false)
        {
            return IsNull(Item = await Load(id,concurrencyError)) ? NotFound() : Page();
        }

        public async Task<IActionResult> OnGetDetailsAsync(string id)
        {
            return IsNull(Item = await Load(id)) ? NotFound() : Page();
        }

        public async Task<IActionResult> OnGetEditAsync(string id)
        {
            if (!IsNull(Item)) DoBeforeCreate();
            return IsNull(Item = await Load(id)) ? NotFound() : Page();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid) return Page();
            if (await Repo.AddAsync(ToEntity(Item)) == false)
            {
                ErrorMessage = Repo.ErrorMessage;
                return Page();
            }
            return IndexPage();
        }

        public virtual async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            if (await Save(Remove)) return IndexPage();
            if (Repo?.EntityInDb is null) return IndexPage();
            return RedirectToPage("./Delete",
                new { id, concurrencyError = true, handler = "Delete"});
        }
        public async Task<IActionResult> OnPostEditAsync(string id)
        {
            if (IsNull(id)) return NotFound();
            if (!ModelState.IsValid) return Page();
            if (await Save(Update)) return IndexPage();

            SetPreviousValues(ToViewModel(Repo?.EntityInDb));
            Item.RowVersion = Repo?.EntityInDb?.RowVersion;
            ModelState.Remove("Item.RowVersion");
            ErrorMessage = Repo?.ErrorMessage;
            return Page();
        }
        protected internal virtual void DoBeforeCreate() { }

        internal IActionResult IndexPage() =>
            RedirectToPage("./Index", new { handler = "Index" });

        internal async Task<bool> Save(params Func<Task<bool>>[] actions)
        {
            var transaction = IsNull(Db) ? null : await Db.Database.BeginTransactionAsync();
            foreach (var a in actions)
            {
                var b = await a();
                if (b) continue;
                ErrorMessage = Repo?.ErrorMessage;
                if (transaction != null) await transaction.RollbackAsync();
                return false;
            }
            if (!IsNull(Db)) await Db.SaveChangesAsync();
            if (transaction != null) await transaction.CommitAsync();
            return true;
        }

        private void SetPreviousValues(TView dbValues)
        {
            if (IsNull(dbValues)) return;
            foreach (var p in dbValues.GetType().GetProperties())
            {
                if (!p.CanRead) continue;
                var dbValue = p.GetValue(dbValues);
                var clientValue = p.GetValue(Item);
                if (dbValue?.ToString() == clientValue?.ToString()) continue;
                ModelState.AddModelError($"Item.{p.Name}",
                    $"Current value: {dbValue}");
            }
        }
    }
}

