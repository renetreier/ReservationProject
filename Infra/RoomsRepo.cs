﻿using System.Linq;
using ReservationProject.Data;
using ReservationProject.Domain;
using ReservationProject.Domain.Repos;
using ReservationProject.Infra.Common;

namespace ReservationProject.Infra {

    public sealed class RoomsRepo : PagedRepo<Room, RoomData>, IRoomsRepo
    {
        public RoomsRepo(){}
        public RoomsRepo(ApplicationDbContext c) : base(c, c?.Rooms) { }
        public override Room ToEntity(RoomData d) => new(d);
        public override RoomData ToData(Room e) =>  e?.Data ?? new RoomData() ;

        public override IQueryable<RoomData> ApplyFilters(IQueryable<RoomData> query)
        {
            if (SearchString is null) return query;
            return query.Where(
                x => x.RoomName.Contains(SearchString) ||
                     x.BuildingAddress.Contains(SearchString));
        }

    }
}










