using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Infra.Common;

namespace ReservationProject.Tests.Infra.Common
{
    [TestClass]
    public class DbInitializerTests: StaticClassTests {
        [TestInitialize] public void TestInitialize() {
            Type = typeof(DbInitializer);
        }
    }
}