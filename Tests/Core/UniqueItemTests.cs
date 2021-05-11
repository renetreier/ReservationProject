using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Aids;
using ReservationProject.Core;

namespace ReservationProject.Tests.Core {
    [TestClass] public class UniqueItemTests: AbstractClassTests<UniqueItem, object>
    {
        private class TestClass:UniqueItem { }
        protected override UniqueItem GetObject() => GetRandom.ObjectOf<TestClass>();
        [TestMethod] public void IdTest() => IsReadWriteProperty<string>();
        [TestMethod]
        public void DefaultIdTest() 
        {
            Obj = new TestClass();
            IsFalse(string.IsNullOrWhiteSpace(Obj.Id));
        }
        [TestMethod]
        public void DefaultIdIsGuidTest()
        {
            Obj = new TestClass();
            var guid = Guid.Parse(Obj.Id);
            Assert.AreEqual(Obj.Id, guid.ToString());
        }
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void AnyStringIsNotGuidTest()
        {
            var s = GetRandom.String();
            var _ = Guid.Parse(s);
        }
    }
}
