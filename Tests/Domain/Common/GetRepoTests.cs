using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Domain.Common;
using ReservationProject.Domain.Repos;
using ReservationProject.Tests.Domain.Repos;

namespace ReservationProject.Tests.Domain.Common {

    [TestClass] public class GetRepoTests :SealedClassTests<GetRepo, object> {
        private IServiceProvider Provider { get; set; }
        protected override GetRepo GetObject() => new(Provider);
        [TestCleanup] public void TestCleanup() => GetRepo.SetProvider(null);
        [TestMethod] public void ProviderIsNullTest() => IsNull(Obj.Provider);
        [TestMethod] public void InstanceIsNullTest() => IsNull(GetRepo.ProviderInstance);
        [TestMethod]
        public void CanCreateTest()
        {
            InitMock();
            AreNotEqual(Provider, GetRepo.ProviderInstance);
            AreEqual(Provider, Obj.Provider);
        }
        [TestMethod]
        public void SetProviderTest()
        {
            var p = new MockServiceProvider(null);
            GetRepo.SetProvider(p);
            IsNull(Obj.Provider);
            AreEqual(p, GetRepo.ProviderInstance);
        }
        [TestMethod]
        public void CreateAfterSetTest()
        {
            var p = new MockServiceProvider(null);
            GetRepo.SetProvider(p);
            Obj = new GetRepo();
            AreEqual(p, GetRepo.ProviderInstance);
            AreEqual(p, Obj.Provider);
        }
        [TestMethod]
        public void InstanceWithTypeTest()
        {
            var repo = InitMock();
            var r = Obj.Instance(typeof(IRoomsRepo));
            AreEqual(repo, r);
        }
        [TestMethod]
        public void InstanceTest()
        {
            var repo = InitMock();
            var r = Obj.Instance<IRoomsRepo>();
            AreEqual(repo, r);
        }
        private MockRoomsRepo InitMock()
        {
            var r = new MockRoomsRepo();
            Provider = new MockServiceProvider(r);
            Obj = GetObject();
            return r;
        }
    }
}