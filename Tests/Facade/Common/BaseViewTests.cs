using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Aids;
using ReservationProject.Core;
using ReservationProject.Facade.Common;

namespace ReservationProject.Tests.Facade.Common {
    [TestClass]
    public class BaseViewTests: AbstractClassTests<BaseView, UniqueItem> {
        private class TestClass:BaseView { }

        protected override BaseView GetObject() => GetRandom.ObjectOf<TestClass>();
        [TestMethod] public void RowVersionTest() => IsReadWriteProperty<byte[]>();

    }
}
