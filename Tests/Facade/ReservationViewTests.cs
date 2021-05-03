using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Facade;
using ReservationProject.Facade.Common;

namespace ReservationProject.Tests.Facade
{
    [TestClass]
    public class ReservationViewTests : SealedClassTests<ReservationView,BaseView>
    {
        [TestMethod] public void RoomIdTest() => IsReadWriteProperty<string>();
        [TestMethod] public void ReservationDateTest() => IsReadWriteProperty<DateTime>();
        [TestMethod] public void WorkerIdTest() => IsReadWriteProperty<string>();

        [TestMethod]
        public void WorkerNameTest() => IsReadWriteProperty<string>();
        [TestMethod]
        public void RoomNameTest() => IsReadWriteProperty<string>();
    }
}