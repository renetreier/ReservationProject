using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReservationProject.Tests.Core {
    [TestClass] public class IsCoreTested: AssemblyBaseTests 
    {
        public IsCoreTested() 
            : base($"{nameof(ReservationProject)}.{nameof(ReservationProject.Core)}") {  }
    }
}
