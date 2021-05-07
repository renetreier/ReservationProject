using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Aids;
using ReservationProject.Core;
using ReservationProject.Data.Common;
using ReservationProject.Domain.Common;
using ReservationProject.Domain.Repos;
using ReservationProject.Infra;
using ReservationProject.Infra.Common;

namespace ReservationProject.Tests
{
    public abstract class InMemoryRepoTests<TRepo, TEntity, TData>
        : SealedClassTests<TRepo, PagedRepo<TEntity, TData>>
        where TRepo : PagedRepo<TEntity, TData>, IRepo<TEntity>, new()
        where TEntity: BaseEntity<TData>
        where TData : BaseData, IEntityData, new() {
        protected abstract TEntity CreateEntity(TData d);
        protected abstract TRepo CreateRepo(ApplicationDbContext c);

        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDb").Options;
            Obj = CreateRepo(new ApplicationDbContext(options));
        }

        [TestMethod] public void ToDataTest() {
            var expected = GetRandom.ObjectOf<TData>();
            var o = CreateEntity(expected);
            var actual = Obj.ToData(o);
            ArePropertiesEqual(expected, actual);
        }
        [TestMethod] public void ToEntityTest() {
            var expected = GetRandom.ObjectOf<TData>();
            var actual = Obj.ToEntity(expected);
            ArePropertiesEqual(expected, actual.Data);
        }
        [TestMethod] public void ApplyFiltersTest() {
            
            var query = Obj.CreateSql();

            Obj.SearchString = GetRandom.String();
            Obj.PageIndex = 0;
            var expected = Obj.ApplyFilters(query).Expression.ToString();
            var actual = Obj.CreateSql().Expression.ToString();
            AreEqual(expected, actual);
        }
    }
}