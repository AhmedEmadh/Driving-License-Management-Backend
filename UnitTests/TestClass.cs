using Driving_License_Management_BusinessLogicLayer;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.InitalizationClasses;

namespace UnitTests
{
    [TestFixture]
    public abstract class TestClass
    {
        [OneTimeSetUp]
        public void Initalize()
        {
            InitalizationCode.InitalizeDataAccessLayer();
        }
    }
}
