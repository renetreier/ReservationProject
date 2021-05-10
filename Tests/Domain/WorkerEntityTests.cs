using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Aids;
using ReservationProject.Data;
using ReservationProject.Domain;
using ReservationProject.Domain.Common;

namespace ReservationProject.Tests.Domain
{
    [TestClass]
    public class WorkerEntityTests : SealedClassTests<Worker, BaseEntity<WorkerData>>
    {
        protected override Worker GetObject() => new(GetRandom.ObjectOf<WorkerData>());
        [TestMethod] public void LastNameTest() => IsReadOnlyProperty(Obj.Data.LastName ?? "Unspecified");
        [TestMethod] public void FirstNameTest() => IsReadOnlyProperty(Obj.Data.FirstName??"Unspecified");
        [TestMethod] public void FullNameTest() => IsReadOnlyProperty($"{Obj.LastName}, {Obj.FirstName}");
        [TestMethod] public void EmailTest() => IsReadOnlyProperty(Obj.Data.Email??null);
        [TestMethod] public void SalaryTest() => IsReadOnlyProperty(Obj.Data.Salary);

    }
}