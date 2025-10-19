using Driving_License_Management_BusinessLogicLayer;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestFixture]
    internal class clsDriver_test : TestClass, IListTest
    {
        dynamic? result;
        [SetUp]
        public void Setup()
        {
            result = clsDriver.GetAllDriversList();
        }
        [Test]
        public void DataIsNotEmpty()
        {
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void DataIsNotNull()
        {
            Assert.That(result?.Count, Is.GreaterThan(0));
        }
    }
}
