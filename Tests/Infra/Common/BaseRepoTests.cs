using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Aids;
using ReservationProject.Data;
using ReservationProject.Domain;
using ReservationProject.Infra;
using ReservationProject.Infra.Common;

namespace ReservationProject.Tests.Infra.Common
{
    [TestClass]
    public abstract class BaseRepoTests : AbstractClassTests<BaseRepo<Room, RoomData>
        ,object>
    {

        private class TestRepo : BaseRepo<Room, RoomData>
        {
            public TestRepo(ApplicationDbContext c = null)
                : base(c, c?.Rooms) { }
            public override Room ToEntity(RoomData d) => new(d);
            public override RoomData ToData(Room e) => e.Data;

            public override int? PageIndex { get; set; }
            public override int TotalPages { get; }
            public override bool HasNextPage { get; }
            public override bool HasPreviousPage { get; }
            public override int PageSize { get; set; }
            public override string CurrentFilter { get; set; }
            public override string SearchString { get; set; }
            public override string SortOrder { get; set; }
            public override string CurrentSort { get; }
        }

        [TestInitialize] public override void TestInitialize()
        {
            base.TestInitialize();
            Obj.Set.RemoveRange(Obj.Set);
            Obj.Db.SaveChanges();
        }
        protected override BaseRepo<Room, RoomData> GetObject()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDb").Options;
            var c = new ApplicationDbContext(options);
            return new TestRepo(c);
        }

        [TestMethod] public async Task GetAsyncTest()
        {
            var d = GetRandom.ObjectOf<RoomData>();
            var o = await Obj.GetAsync(d.Id);
            ArePropertiesNotEqual(d, o.Data);

            await Obj.Set.AddAsync(d);
            await Obj.Db.SaveChangesAsync();

            o = await Obj.GetAsync(d.Id);

            ArePropertiesEqual(d, o.Data, nameof(d.RowVersion));
        }

        [TestMethod] public async Task DeleteAsyncTest()
        {

            var d = GetRandom.ObjectOf<RoomData>();
            await Obj.Set.AddAsync(d);
            await Obj.Db.SaveChangesAsync();
            var o = await Obj.GetAsync(d.Id);
            ArePropertiesEqual(d, o.Data, nameof(d.RowVersion));

            await Obj.DeleteAsync(d);

            o = await Obj.GetAsync(d.Id);
            ArePropertiesNotEqual(d, o.Data);
        }

        [TestMethod] public async Task AddAsyncTest()
        {
            var d = GetRandom.ObjectOf<RoomData>();
            var o = await Obj.GetAsync(d.Id);
            Assert.IsNotNull(o);
            ArePropertiesNotEqual(d, o.Data);

            await Obj.AddAsync(d);

            o = await Obj.GetAsync(d.Id);
            Assert.IsInstanceOfType(o, typeof(Room));
            ArePropertiesEqual(d, o.Data, nameof(d.RowVersion));
        }

        [TestMethod] public async Task UpdateAsyncTest()
        {
            var d = GetRandom.ObjectOf<RoomData>();
            await Obj.Set.AddAsync(d);
            await Obj.Db.SaveChangesAsync();
            var o = await Obj.GetAsync(d.Id);
            ArePropertiesEqual(d, o.Data, nameof(d.RowVersion));

            var newObj = GetRandom.ObjectOf<RoomData>();
            d.BuildingAddress = newObj.BuildingAddress;
            d.RoomName = newObj.RoomName;
            d.RowVersion = newObj.RowVersion;

            await Obj.UpdateAsync(d);

            ArePropertiesEqual(d, newObj, nameof(d.Id));
        }

        [TestMethod] public async Task GetListTest()
        {
            var l = await Obj.GetAsync();
            AreEqual(0, l.Count);
            var count = GetRandom.UInt8(10, 20);
            for (var i = 1; i <= count; i++)
                await Obj.Set.AddAsync(GetRandom.ObjectOf<RoomData>());
            await Obj.Db.SaveChangesAsync();
            l = await Obj.GetAsync();
            AreEqual((int)count, l.Count);
        }

        [TestMethod] public async Task GetTest()
        {
            var d = GetRandom.ObjectOf<RoomData>();
            var o = Obj.Get(d.Id);
            ArePropertiesNotEqual(d, o.Data);

            await Obj.Set.AddAsync(d);
            await Obj.Db.SaveChangesAsync();

            o = Obj.Get(d.Id);

            ArePropertiesEqual(d, o.Data, nameof(d.RowVersion));

        }
        [TestMethod] public void ErrorMessageTest() => IsProperty<string>();

        [TestMethod] public async Task EntityInDbTest()
        {
            var d = GetRandom.ObjectOf<RoomData>();
            await Obj.Set.AddAsync(d);
            await Obj.Db.SaveChangesAsync();
            var o = await Obj.GetAsync(d.Id);
            ArePropertiesEqual(d, o.Data, nameof(d.RowVersion));
            var d1 = GetRandom.ObjectOf<RoomData>();
            d1.Id = o.Id;
 
            AreEqual(false, await Obj.UpdateAsync(new Room(d1)));

            ArePropertiesEqual(d, Obj.EntityInDb.Data, nameof(d.RowVersion));
        }
    }
}