using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Data;
using ReservationProject.Data.Common;

namespace ReservationProject.Tests.Facade
{
    [TestClass]
    public class ReservationViewTests : SealedClassTests<ReservationData,BaseData>
    {
        [TestMethod] public void RoomIdTest() => IsReadWriteProperty<string>();
        [TestMethod] public void ReservationDateTest() => IsReadWriteProperty<DateTime>();
        [TestMethod] public void WorkerIdTest() => IsReadWriteProperty<string>();

        [TestMethod]
        public void WorkerNameTest() => IsReadOnlyProperty<string>();
        [TestMethod]
        public void RoomNameTest() => IsReadOnlyProperty<string>();
    }
}