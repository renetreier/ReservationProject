using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Pages
{ public class BasePageModel : PageModel
    {
        //todo kas siia ei saaks kokku tuua asju Clients ja Workermodelist näiteks? mingid meetodid ma mõtlen
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