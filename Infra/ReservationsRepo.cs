﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReservationProject.Data;
using ReservationProject.Domain;
using ReservationProject.Infra.Common;

namespace ReservationProject.Infra {
    public interface IReservationsRepo : IRepo<Reservation> { }

    public sealed class ReservationsRepo : BaseRepo<Reservation>, IReservationsRepo
    {
        public ReservationsRepo(ApplicationDbContext c) : base(c, c?.Reservations) { }

        public override async Task<List<Reservation>> Get()
        {
            return await Set.AsNoTracking().OrderBy(r=>r.ReservationDate).Include(c => c.ReservedRoom)
                .Include(c => c.ReservedWorker).ToListAsync();
        }
    }
}











