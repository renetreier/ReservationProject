
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReservationProject.Data;
using ReservationProject.Domain;
using ReservationProject.Infra.Common;

namespace ReservationProject.Infra {
    public interface IRoomsRepo : IRepo<Room> { }

    public sealed class RoomsRepo : BaseRepo<Room>, IRoomsRepo
    {
        public RoomsRepo(ApplicationDbContext c) : base(c, c?.Rooms) { }
        public override async Task<List<Room>> Get()
        {
            return await Set.AsNoTracking().OrderBy(r => r.RoomName).ToListAsync();
        }
    }
}










