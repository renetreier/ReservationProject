using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ReservationProject.Aids;

namespace ReservationProject.Tests.Aids {
    [TestClass]
    public class CopyTests:StaticClassTests {
        private class TestClass1 {
            public string Id { get; set; }
            public string Name { get; set; }
            public DateTime DoB { get; set; }
        }
        private class TestClass2 {
            public string AdministratorId { get; set; }
            public string Name { get; set; }
            public DateTime DoB { get; set; }
        }
        [TestInitialize] public void TestInitialize() => Type = typeof(Copy);
        [TestMethod] public void MemberTest() {
            var x = GetRandom.ObjectOf<TestClass1>();
            var y = new TestClass2();
            y = Copy.Members(x, y);
            Assert.AreEqual(x.Name, y.Name);
            Assert.AreEqual(x.DoB, y.DoB);
        }
    }
}
