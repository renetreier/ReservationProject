using ReservationProject.Aids;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReservationProject.Tests.Aids {

    [TestClass]
    public class GetMemberTests {

        private readonly string stringField = null;
        private string stringProperty { get; } = null;

        [TestMethod] public void NameTestField() 
            => Assert.AreEqual("stringField",
                GetMember.Name<GetMemberTests>(x => x.stringField));
        [TestMethod] public void NameTestProperty() 
            => Assert.AreEqual("stringProperty",
                GetMember.Name<GetMemberTests>(x => x.stringProperty));
        [TestMethod] public void NameTestFunction() 
            => Assert.AreEqual("ToString", 
                GetMember.Name<object>(x => x.ToString()));
        [TestMethod] public void NameTestMethod() 
            => Assert.AreEqual("NameTestMethod", 
                GetMember.Name<GetMemberTests>(x => x.NameTestMethod()));
    }
}
