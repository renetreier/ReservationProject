using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Aids;
using ReservationProject.Facade;
using ReservationProject.Facade.Common;

namespace ReservationProject.Tests.Facade
{
    [TestClass]
    public class WorkerViewTests : SealedClassTests<WorkerView,BaseView>
    {
        [TestMethod] public void LastNameTest() => IsReadWriteProperty<string>();
        [TestMethod] public void FirstNameTest() => IsReadWriteProperty<string>();
        [TestMethod] public void EmailTest() => IsReadWriteProperty<string>();
        [TestMethod] public void SalaryTest() => IsReadWriteProperty<double>();
        [TestMethod] public void FullNameTest() => IsReadOnlyProperty<string>();

    }
}