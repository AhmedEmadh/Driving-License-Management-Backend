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
    internal class clsTest_test: TestClass,IListTest
    {
        dynamic? result;
        [SetUp]
        public void Setup()
        {
            result = clsTest.GetAllTestsList();
        }
        [Test]
        public void DataIsNotEmpty()
        {
            Assert.That(result?.Count, Is.GreaterThan(0));
        }
        [Test]
        public void DataIsNotNull()
        {
            Assert.That(result, Is.Not.Null);
        }
    }
}
