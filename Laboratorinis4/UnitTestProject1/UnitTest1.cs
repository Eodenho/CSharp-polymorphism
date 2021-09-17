using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Laboratorinis4.Classes;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public abstract class RegisterTest
    {
        [TestMethod]
        public void AddedToRegister_CheckIfAdded_ReturnsTrue()
        {
            Register register = new Register();
            register.Add(CreateSampleValue1());
            bool check = register.Contains(CreateSampleValue1());
            Assert.IsTrue(check);
        }
        [TestMethod]
        public void NotAddedToRegister_CheckIfAdded_Returnsfalse()
        {
            Register register = new Register();
            bool check = register.Contains(CreateSampleValue1());
            Assert.IsFalse(check);
        }
        [TestMethod]
        public void AddedToRegister_CheckCount_ShouldReturnOne()
        {
            Register register = new Register();
            register.Add(CreateSampleValue1());
            int count = register.Count();
            count.Should().Be(1);
        }
        [TestMethod]
        public void NotAddedToRegister_CheckCount_ShouldReturnZero()
        {
            Register register = new Register();
            int count = register.Count();
            count.Should().Be(0);
        }
        [TestMethod]
        public void AddedToRegister_GetItem_ShouldReturnTrue()
        {
            Register register = new Register();
            register.Add(CreateSampleValue1());
            Transport transport = register.Get(0);
            bool check = transport.Equals(CreateSampleValue1());
            Assert.IsTrue(check);
        }
        [TestMethod]
        public void NotAddedToRegister_GetItem_ShouldReturnNull()
        {
            Register register = new Register();
            Transport transport = register.Get(0);
            transport.Should().Be(null);
        }
        [TestMethod]
        public void CarAddedToRegister_FindTrucks_ShouldReturnEmptyList()
        {
            Register register = new Register();
            register.Add(CreateSampleValue1());
            int count = register.FindTrucks().Count;
            count.Should().Be(0);
        }
        [TestMethod]
        public void TruckAddedToRegister_FindTrucks_ShouldReturnListOf1()
        {
            Register register = new Register();
            register.Add(CreateSampleValue3());
            int count = register.FindTrucks().Count;
            count.Should().Be(1);
        }
        [TestMethod]
        public void EmptyRegister_CalculateMicroBusAge_ShouldReturnFalse()
        {
            Register register = new Register();
            double age = register.CalculateMicrobusYearsAverage();
            bool check = age > 0;
            Assert.IsFalse(check);
        }
        [TestMethod]
        public void MicrobusAddedToRegister_CalculateMicroBusAge_ShouldReturnTrue()
        {
            Register register = new Register();
            register.Add(CreateSampleValue2());
            double age = register.CalculateMicrobusYearsAverage();
            bool check = age >= 0;
            Assert.IsTrue(check);
        }
        [TestMethod]
        public void ItemsAddedToRegister_FindTransportsWithServicingDueDate_ShouldReturn2()
        {
            Register register = new Register();
            register.Add(CreateSampleValue1());
            register.Add(CreateSampleValue2());
            register.Add(CreateSampleValue3());
            double count = register.FindTransportsWithServicingDueDate().Count;
            count.Should().Be(2);
        }
        [TestMethod]
        public void EmptyRegister_FindTransportsWithServicingDueDate_ShouldReturn0()
        {
            Register register = new Register();
            double count = register.FindTransportsWithServicingDueDate().Count;
            count.Should().Be(0);
        }
        [TestMethod]
        public void MicrobusAddedToRegister_FindBestCar_ShouldReturnNull()
        {
            Register register = new Register();
            register.Add(CreateSampleValue2());
            Transport transport = register.FindBestCar();
            transport.Should().Be(null);
        }
        [TestMethod]
        public void CarAddedToRegister_FindBestCar_ShouldReturnNotNull()
        {
            Register register = new Register();
            register.Add(CreateSampleValue1());
            Transport transport = register.FindBestCar();
            transport.Should().NotBeNull();
        }
        [TestMethod]
        public void CarAddedToRegister_FindBestMicrobus_ShouldReturnNull()
        {
            Register register = new Register();
            register.Add(CreateSampleValue1());
            Transport transport = register.FindBestMicrobus();
            transport.Should().Be(null);
        }
        [TestMethod]
        public void MicrobusAddedToRegister_FindBestMicrobus_ShouldReturnNotNull()
        {
            Register register = new Register();
            register.Add(CreateSampleValue2());
            Transport transport = register.FindBestMicrobus();
            transport.Should().NotBeNull();
        }
        [TestMethod]
        public void CarAddedToRegister_FindBestTruck_ShouldReturnNull()
        {
            Register register = new Register();
            register.Add(CreateSampleValue1());
            Transport transport = register.FindBestTruck();
            transport.Should().Be(null);
        }
        [TestMethod]
        public void TruckAddedToRegister_FindBestTruck_ShouldReturnNotNull()
        {
            Register register = new Register();
            register.Add(CreateSampleValue3());
            Transport transport = register.FindBestTruck();
            transport.Should().NotBeNull();
        }
        protected abstract Transport CreateSampleValue1();
        protected abstract Transport CreateSampleValue2();
        protected abstract Transport CreateSampleValue3();
    }
    [TestClass]
    public class RegisterReferenceAndValueTests : RegisterTest
    {
        protected override Transport CreateSampleValue1()
        {
            var start = new DateTime(2010, 5, 4);
            var value = new DateTime(2019, 5, 4);
            return new Car("AAA000","Audi","A4", start, value,"Dyzelis",7.5,150000);
        }
        protected override Transport CreateSampleValue2()
        {
            var start = new DateTime(2010, 5, 4);
            var value = new DateTime(2021, 5, 4);
            return new Microbus("BBB000", "Volkswagen", "BUS", start, value, "Dyzelis", 7.5, 6);
        }
        protected override Transport CreateSampleValue3()
        {
            var start = new DateTime(2010, 4, 4);
            var value = new DateTime(2019, 6, 4);
            return new Truck("CCC000", "Mercedes", "Axor", start, value, "Dyzelis", 7.5, 150);
        }
    }
}
