using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReservationProject.Tests.Data {
    [TestClass]
    public class IsDataTested: AssemblyBaseTests {
        public IsDataTested() : base($"{nameof(ReservationProject)}.{nameof(ReservationProject.Data)}") {  }
    }
}
