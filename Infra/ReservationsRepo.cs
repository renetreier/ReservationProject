using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReservationProject.Data;
using ReservationProject.Domain;

namespace ReservationProject.Infra {
    public interface IReservationsRepo : IRepo<Reservation> { }

    public sealed class ReservationsRepo : BaseRepo<Reservation>, IReservationsRepo
    {
        public ReservationsRepo(ApplicationDbContext c) : base(c, c?.Reservations) { }

        public override async Task<List<Reservation>> Get()
        {
            return await set.AsNoTracking().Include(c => c.ReservedRoom)
                .Include(c => c.ReservedWorker).ToListAsync();
        }
    }
}











