using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Data;
using ReservationProject.Data.Common;

namespace ReservationProject.Tests.Data
{
    [TestClass]
    public class WorkerDataTests : SealedClassTests<WorkerData,BaseData>
    {
        [TestMethod] public void LastNameTest() => IsReadWriteProperty<string>();
        [TestMethod] public void FirstNameTest() => IsReadWriteProperty<string>();
        [TestMethod] public void EmailTest() => IsReadWriteProperty<string>();
        [TestMethod] public void SalaryTest() => IsReadWriteProperty<double>();

    }
}