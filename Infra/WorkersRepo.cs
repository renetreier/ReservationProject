using System.Linq;
using ReservationProject.Data;
using ReservationProject.Domain;
using ReservationProject.Domain.Repos;
using ReservationProject.Infra.Common;

namespace ReservationProject.Infra {
    
    public sealed class WorkersRepo : PagedRepo<Worker,WorkerData>, IWorkersRepo
    {
        public WorkersRepo() {}
        public WorkersRepo(ApplicationDbContext c) : base(c, c?.Workers) { }
        public override Worker ToEntity(WorkerData d) => new(d);
        public override WorkerData ToData(Worker e) => e?.Data ?? new WorkerData();

        public override IQueryable<WorkerData> ApplyFilters(IQueryable<WorkerData> query)
        {
            if (SearchString is null) return query;
            return query.Where(
                x => x.FirstName.Contains(SearchString) ||
                     x.LastName.Contains(SearchString) ||
                     x.Email.Contains(SearchString));
        }
    }
}










