using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Aids;

namespace ReservationProject.Tests {
    public class EnumBaseTests<T> : BaseTests where T: Enum {
        protected T Value;
        protected Type Type;
        [TestInitialize] public void TestInitialize() {
            Type = typeof(T);
            Value = GetRandom.EnumOf<T>();
        }
    }
}
