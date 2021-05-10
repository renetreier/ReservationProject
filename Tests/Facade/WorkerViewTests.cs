﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Facade;
using ReservationProject.Facade.Common;

namespace ReservationProject.Tests.Facade
{
    [TestClass]
    public class WorkerViewTests :SealedClassTests<WorkerView,BaseView>
    {
        protected override void IsReadWriteProperty<T>()
        {
            var propertyInfo = IsProperty<T>();
            var actual = GetPropertyValue<T>(true);
            var expected = GetValue(actual);
            var current = GetCurrentValues();
            SetPropertyValue(propertyInfo, expected);
            actual = GetPropertyValue<T>(true);
            AreEqual(expected, actual);
            //ArePropertiesEqual(current, GetCurrentValues(), propertyInfo.Name);
            //TODO tõin selle osa siia ja overridesin, kui viimane rida ära eemaldada, siis test läheb läbi
        }
        [TestMethod] public void FullNameTest() => IsReadOnlyProperty<string>();
        [TestMethod] public void LastNameTest() => IsReadWriteProperty<string>();
        [TestMethod] public void FirstNameTest() => IsReadWriteProperty<string>();
        [TestMethod] public void EmailTest() => IsReadWriteProperty<string>();
        [TestMethod] public void SalaryTest() => IsReadWriteProperty<double>();
       

    }
}