using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Infra;
using ReservationProject.Infra.Common;

namespace ReservationProject.Tests.Infra.Common
{
    [TestClass]
    public class DbInitializerTests: StaticClassTests
    {
        protected internal ApplicationDbContext TestDb;

        [TestInitialize] public virtual void TestInitialize() 
        {
            Type = typeof(DbInitializer);
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDb").Options;
            
            TestDb = new ApplicationDbContext(options);
            TestDb.RemoveRange(TestDb.Reservations);
            TestDb.RemoveRange(TestDb.Rooms);
            TestDb.RemoveRange(TestDb.Workers);
        }

        [TestMethod] public void InitializeTest()
        {
            DbInitializer.Initialize(TestDb);
            AreEqual(3, TestDb.Workers.Count());
            AreEqual(3, TestDb.Rooms.Count());
            AreEqual(7, TestDb.Reservations.Count());
        }
    }
}