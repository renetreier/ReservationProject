using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Aids;
using ReservationProject.Facade;
using ReservationProject.Facade.Common;
using ReservationProject.Tests.Aids;

namespace ReservationProject.Tests.Facade
{
    [TestClass]
    public class WorkerViewTests :SealedClassTests<WorkerView,BaseView>
    {
        //private class TestClass :WorkerView{ }
        //protected override WorkerView GetObject() => GetRandom.ObjectOf<TestClass>();
        [TestMethod] public void FullNameTest() => IsReadOnlyProperty<string>();
        [TestMethod] public void LastNameTest() => IsReadWriteProperty<string>();
        [TestMethod] public void FirstNameTest() => IsReadWriteProperty<string>();
        [TestMethod] public void EmailTest() => IsReadWriteProperty<string>();
        [TestMethod] public void SalaryTest() => IsReadWriteProperty<double>();
       

    }
}