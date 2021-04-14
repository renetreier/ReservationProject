using ReservationProject.Aids;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReservationProject.Tests.Aids {
    [TestClass] public class GetRandomTests {
        [TestMethod] public void Int32Test()
            => Assert.AreNotEqual(GetRandom.Int32(), GetRandom.Int32());
        [TestMethod] public void Int32TestMin() {
            var min = GetRandom.Int32();
            var i = GetRandom.Int32(min);
            Assert.IsTrue(i >= min);
        }
        [TestMethod] public void Int32TestMax() {
            var max = GetRandom.Int32();
            var i = GetRandom.Int32(max: max);
            Assert.IsTrue(i <= max);
        }
        [TestMethod] public void Int32TestRange() {
            var min = GetRandom.Int32();
            var max = GetRandom.Int32(min);
            var i = GetRandom.Int32(min, max);
            Assert.IsTrue(min <= i && i <= max);
        }
    }
}
