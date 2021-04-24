using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReservationProject.Data;
using ReservationProject.Domain;
using ReservationProject.Infra.Common;

namespace ReservationProject.Infra {
    public interface IWorkersRepo : IRepo<Worker> { }

    public sealed class WorkersRepo : BaseRepo<Worker>, IWorkersRepo
    {
        public WorkersRepo(ApplicationDbContext c) : base(c, c?.Workers) { }
        public override async Task<List<Worker>> Get()
        {
            return await Set.AsNoTracking().OrderBy(r => r.FullName).ToListAsync();
        }
    }
}










