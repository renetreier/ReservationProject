
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Aids;
using ReservationProject.Data;
using ReservationProject.Domain;
using ReservationProject.Domain.Common;
using ReservationProject.Infra;
using ReservationProject.Infra.Common;

namespace ReservationProject.Tests.Infra.Common
{
    [TestClass]
    public class BaseRepoTests :AbstractClassTests<BaseRepo<RoomData>, object> {

        private class testRepo : BaseRepo<RoomEntity, RoomData>
        {
            public testRepo(ApplicationDbContext c = null)
                : base(c, c?.Rooms) { }
            protected override RoomEntity ToEntity(RoomData d) => new(d);
            protected override RoomData ToData(RoomEntity e) => e.Data;

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

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            Obj.Set.RemoveRange(Obj.Set);
            Obj.Db.SaveChanges();
        }
        protected override BaseRepo<RoomEntity, RoomData> GetObject()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDb").Options;
            var c = new ApplicationDbContext(options);
            return new testRepo(c);
        }

        [TestMethod]
        public async Task GetTest()
        {
            var d = GetRandom.ObjectOf<RoomData>();
            var o = await Obj.GetAsync(d.Id);
            ArePropertiesNotEqual(d, o);

            await Obj.Set.AddAsync(d);
            await Obj.Db.SaveChangesAsync();

            o = await Obj.GetAsync(d.Id);

            ArePropertiesEqual(d, o, nameof(d.RowVersion));
        }
        [TestMethod] public void ErrorMessageTest() => IsProperty<string>();
        [TestMethod] public void EntityInDbTest() => IsProperty<RoomData>();

    }
}