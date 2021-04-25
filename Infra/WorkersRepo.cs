using System.Globalization;
using System.Linq;
using ReservationProject.Data;
using ReservationProject.Domain;
using ReservationProject.Domain.Repos;
using ReservationProject.Infra.Common;

namespace ReservationProject.Infra {
    
    public sealed class WorkersRepo : PagedRepo<WorkerEntity,WorkerData>, IWorkersRepo
    {
        public WorkersRepo(ApplicationDbContext c) : base(c, c?.Workers) { }
        protected override WorkerEntity ToEntity(WorkerData d) => new(d);
        protected override WorkerData ToData(WorkerEntity e) => e?.Data ?? new WorkerData();

        protected internal override IQueryable<WorkerData> ApplyFilters(IQueryable<WorkerData> query)
        {
            if (SearchString is null) return query;
            return query.Where(
                x => x.FirstName.Contains(SearchString) || 
                     x.LastName.Contains(SearchString) || 
                     x.Email.Contains(SearchString) ||
                     x.Salary.ToString(CultureInfo.InvariantCulture).Contains(SearchString));
        }
        //TODO Renksu kala
        //public override async Task<List<WorkerData>> Get()
        //{
        //    return await Set.AsNoTracking().OrderBy(r => r.LastName).ToListAsync();
        //}
    }
}










