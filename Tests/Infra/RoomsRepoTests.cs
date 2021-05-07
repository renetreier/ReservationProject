using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Data;
using ReservationProject.Domain;
using ReservationProject.Infra;
using ReservationProject.Infra.Common;

namespace ReservationProject.Tests.Infra
{
    [TestClass]
    public class RoomsRepoTests
        :InMemoryRepoTests<RoomsRepo, RoomEntity, RoomData> {
        protected override RoomEntity CreateEntity(RoomData d) => new (d);

        protected override RoomsRepo CreateRepo(ApplicationDbContext c) => new (c);
    }
}