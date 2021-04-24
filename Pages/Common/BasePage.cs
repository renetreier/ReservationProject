using System;
using System.Collections.Generic;
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

        //public IList<TEntity> ItemList { get; set; }
        protected internal virtual async Task LoadRelatedItems(TEntity item) { await Task.CompletedTask; }
       // protected internal string SetConcurrencyMsg(bool isError) => isError ? ErrorMessages.ConcurrencyOnDelete : null;
        protected internal abstract TView ToViewModel(TEntity e);
        protected internal abstract TEntity ToEntity(TView e);
        protected internal bool IsNull(object o) => o is null;

        //TODO SIIN VAJA KUIDAGI NÜÜD MINGI NORMAALNE JAMA TEHA :)
        internal async Task<TView> Load(string id)
        {
            var item = await Repo.Get(id);
            if (!IsNull(id)) await LoadRelatedItems(item);
            //ErrorMessage = SetConcurrencyMsg(concurrencyError);
            return ToViewModel(item);
        }
        //internal async Task<bool> remove() =>
        //    !IsNull(Repo) && await Repo.Delete(ToEntity(Item));
        //internal async Task<bool> add() =>
        //    !IsNull(Repo) && await Repo.Add(ToEntity(Item));
        //internal async Task<bool> update() =>
        //    !IsNull(Repo) && await Repo.Update(ToEntity(Item));

        //internal async Task<TEntity> find(string id)
        //    => IsNull(id) ? null : IsNull(Repo) ? null : await Repo.Get(id);


        //internal async Task<bool> save(params Func<Task<bool>>[] actions)
        //{
        //    var transaction = IsNull(Db) ? null : await Db.Database.BeginTransactionAsync();
        //    foreach (var a in actions)
        //    {
        //        var b = await a();
        //        if (b) continue;
        //        ErrorMessage = Repo?.ErrorMessage;
        //        if (transaction != null) await transaction.RollbackAsync();
        //        return false;
        //    }
        //    if (!IsNull(Db)) await Db.SaveChangesAsync();
        //    if (transaction != null) await transaction.CommitAsync();
        //    return true;
        //}
        //protected internal virtual void DoBeforeCreate(TView v) { }
        //protected internal virtual void DoBeforeDelete(TView v) { }
        //protected internal virtual void DoBeforeEdit(TView v) { }

        //internal IActionResult IndexPage() =>
        //    RedirectToPage("./Index", new { handler = "Index" });
        //public async Task<IActionResult> OnGetDeleteAsync(string id, bool concurrencyError = false)
        //    => IsNull(Item = await Load(id, concurrencyError)) ? NotFound() : Page();
        //public async Task<IActionResult> OnGetDetailsAsync(string id)
        //    => IsNull(Item = await Load(id)) ? NotFound() : Page();
        //public async Task<IActionResult> OnGetEditAsync(string id)
        //    => IsNull(Item ??= await Load(id)) ? NotFound() : Page();

        //public IActionResult OnGetCreate()
        //{
        //    DoBeforeCreate(Item);
        //    return Page();
        //}
        //public virtual async Task<IActionResult> OnPostDeleteAsync(string id)
        //{
        //    DoBeforeDelete(Item);
        //    if (await save(remove)) return IndexPage();
        //    if (Repo?.EntityInDb is null) return IndexPage();
        //    return RedirectToPage("./Delete",
        //        new { id, concurrencyError = true, handler = "Delete" });
        //}
        //public virtual async Task<IActionResult> OnPostEditAsync(string id, string[] selectedCourses = null)
        //{
        //    if (!ModelState.IsValid) return Page();
        //    DoBeforeEdit(Item);
        //    if (await save(update)) return IndexPage();
        //    SetPreviousValues(ToViewModel(Repo?.EntityInDb));
        //    Item.RowVersion = Repo?.EntityInDb?.RowVersion;
        //    ModelState.Remove("Item.RowVersion");
        //    return Page();
        //}
        //public virtual async Task<IActionResult> OnPostCreateAsync()
        //{
        //    if (!ModelState.IsValid) return Page();
        //    DoBeforeCreate(Item);
        //    if (await save(add)) return IndexPage();
        //    return Page();
        //}


        //private void SetPreviousValues(TView dbValues)
        //{
        //    if (IsNull(dbValues)) return;
        //    foreach (var p in dbValues.GetType().GetProperties())
        //    {
        //        if (!p.CanRead) continue;
        //        var dbValue = p.GetValue(dbValues);
        //        var clientValue = p.GetValue(Item);
        //        if (dbValue?.ToString() == clientValue?.ToString()) continue;
        //        ModelState.AddModelError($"Item.{p.Name}",
        //            $"Current value: {dbValue}");
        //    }
        //}


        public IActionResult OnGetCreate()
        {
            DoBeforeCreate();
            return Page();
        }

        public async Task<IActionResult> OnGetDeleteAsync(string id)
        {
            return IsNull(Item = await Load(id)) ? NotFound() : Page();
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

        protected internal virtual void DoBeforeCreate() { }

        //TODO siin vist kala? vb seda pole pldse vaja, aga äkki on ka
        //public async Task OnGetAsync()=> ItemList = await Repo.Get();

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid) return Page();
            if (!RoomAvailable())
            {
                ErrorMessage = ErrorMessages.RoomNotFree;
                return Page();
            }
            await Repo.Add(ToEntity(Item));
            if (!IsNull(Db)) await Db.SaveChangesAsync();
            return RedirectToPage("./Index");


        }
        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            if (IsNull(id)) return NotFound();
            await Repo.Delete(ToEntity(Item));
            if (!IsNull(Db)) await Db.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
        public async Task<IActionResult> OnPostEditAsync(string id)
        {
            if (IsNull(id)) return NotFound();
            if (!RoomAvailable())
            {
                ErrorMessage = ErrorMessages.RoomNotFree;
                return Page();
            }
            await Repo.Update(ToEntity(Item));
            if (!IsNull(Db)) await Db.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        protected internal virtual bool RoomAvailable() => true;

    }
}

