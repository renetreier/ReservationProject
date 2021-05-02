using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Aids;

namespace ReservationProject.Tests.Aids {
    [TestClass] public class GetSolutionTests :BaseTests {
        [TestMethod] public void ReferenceAssembliesTests() {
            var assemblies
                = GetSolution
                    .ReferenceAssemblies("ReservationProject.Tests")
                    .Where(x => x.FullName?.StartsWith("ReservationProject") ?? false)
                    .ToList();

            AreEqual(8, assemblies.Count);
        }
    }
}