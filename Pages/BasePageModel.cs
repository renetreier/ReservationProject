using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ReservationProject.Pages
{ public class BasePageModel : PageModel
    {
        
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
        
    }
}