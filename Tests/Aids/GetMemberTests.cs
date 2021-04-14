using ReservationProject.Aids;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReservationProject.Tests.Aids {

    [TestClass]
    public class GetMemberTests {

        private readonly string StringField = null;
        private string StringProperty { get; } = null;

        [TestMethod] public void NameTestField() 
            => Assert.AreEqual("StringField",
                GetMember.Name<GetMemberTests>(x => x.StringField));
        [TestMethod] public void NameTestProperty() 
            => Assert.AreEqual("StringProperty",
                GetMember.Name<GetMemberTests>(x => x.StringProperty));
        [TestMethod] public void NameTestFunction() 
            => Assert.AreEqual("ToString", 
                GetMember.Name<object>(x => x.ToString()));
        [TestMethod] public void NameTestMethod() 
            => Assert.AreEqual("NameTestMethod", 
                GetMember.Name<GetMemberTests>(x => x.NameTestMethod()));
    }
}
