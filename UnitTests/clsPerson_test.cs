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
    internal class clsPerson_test: TestClass, IListTest
    {
        dynamic? result;
        [SetUp]
        public void Setup()
        {
            result = clsPerson.GetAllPeopleList();
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
