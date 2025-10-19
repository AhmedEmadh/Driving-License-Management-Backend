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
    public class clsCountry_test: TestClass, IListTest
    {
        dynamic? result;
        [SetUp]
        public void Setup()
        {
            result = clsCountry.GetAllCountriesList();
        }
        [Test]
        public void DataIsNotNull()
        {
            
        }
        [Test]
        public void DataIsNotEmpty()
        {
            //AAA
            //Arrange

            //Act
            var result = clsCountry.GetAllCountriesList();
            //Assert
            Assert.That(result.Count, Is.Not.Null.And.GreaterThan(0));
        }

    }
}
