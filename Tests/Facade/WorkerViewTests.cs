using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Data;
using ReservationProject.Data.Common;

namespace ReservationProject.Tests.Facade
{
    [TestClass]
    public class WorkerViewTests : SealedClassTests<WorkerData,BaseData>
    {
        [TestMethod] public void LastNameTest() => IsReadWriteProperty<string>();
        [TestMethod] public void FirstNameTest() => IsReadWriteProperty<string>();
        [TestMethod] public void EmailTest() => IsReadWriteProperty<string>();
        [TestMethod] public void SalaryTest() => IsReadWriteProperty<double>();
        [TestMethod] public void FullNameTest() => IsReadWriteProperty<string>();

    }
}