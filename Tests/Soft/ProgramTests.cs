using ReservationProject.Soft;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReservationProject.Tests.Soft {
    [TestClass]
    public class ProgramTests: BaseTests {
        [TestMethod] public void MainTest() {
            var p = typeof(Program).GetMethod(nameof(Program.Main));
            Assert.IsNotNull(p);
            IsTrue(p.IsStatic);
        }
    }
}
