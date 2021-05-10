using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Aids;
using ReservationProject.Data;
using ReservationProject.Domain;
using ReservationProject.Infra;

namespace ReservationProject.Tests.Infra
{
    [TestClass]
    public class ReservationsRepoTests
        : InMemoryRepoTests<ReservationsRepo, ReservationEntity, ReservationData>
    {
        protected override ReservationEntity CreateEntity(ReservationData d) => new(d);

        protected override ReservationsRepo CreateRepo(ApplicationDbContext c) => new(c);


        [TestMethod] public async Task AddAsyncTest()
        {
            var d1 = GetRandom.ObjectOf<ReservationData>();
            var o1 = await Obj.GetAsync(d1.Id);
            Assert.IsNotNull(o1);
            await Obj.AddAsync(d1);

            var d2 = GetRandom.ObjectOf<ReservationData>();
            d2.ReservationDate = d1.ReservationDate;
            d2.RoomId = d1.RoomId;
            var o2 = Obj.ToEntity(d2);
            Assert.IsNotNull(o2);
            IsFalse(await Obj.AddAsync(o2));
        }

        [TestMethod] public async Task UpdateAsyncTest()
        {
            var d1 = GetRandom.ObjectOf<ReservationData>();
            var o1 = await Obj.GetAsync(d1.Id);
            Assert.IsNotNull(o1);
            await Obj.AddAsync(d1);

            var d2 = GetRandom.ObjectOf<ReservationData>();
            d2.ReservationDate = d1.ReservationDate;
            d2.RoomId = d1.RoomId;
            var o2 = Obj.ToEntity(d2);
            Assert.IsNotNull(o2);
            IsFalse(await Obj.UpdateAsync(o2));
        }
        [TestMethod]
        public async Task IsRoomAvailableTest()
        {
            var d1 = GetRandom.ObjectOf<ReservationData>();
            await Obj.AddAsync(d1);
            IsFalse(await Obj.AddAsync(d1));
        }

       
    }
}