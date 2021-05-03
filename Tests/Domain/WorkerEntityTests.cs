using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Data;
using ReservationProject.Domain;
using ReservationProject.Domain.Common;

namespace ReservationProject.Tests.Domain
{
    [TestClass]
    public class WorkerEntityTests : SealedClassTests<WorkerEntity, BaseEntity<WorkerData>>
    {

    }
}