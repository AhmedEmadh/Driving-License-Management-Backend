using Driving_License_Management_BusinessLogicLayer;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.ComponentModel;

using System.Data;
using Category = NUnit.Framework.CategoryAttribute;
namespace UnitTests
{
    [TestFixture]
    public class clsApplicationType_test : TestClass, IListTest
    {
        dynamic? result;
        [SetUp]
        public void Setup()
        {
            result = clsApplicationType.GetAllApplicationTypesList();
        }

        [Test]
        public void DataIsNotNull()
        {
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void DataIsNotEmpty()
        {
            //AAA
            //Arrange

            //Act
            //Assert
            Assert.That(result?.Count, Is.Not.Null.And.GreaterThan(0));
        }

        
    }
}
