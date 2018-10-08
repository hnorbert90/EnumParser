namespace EnumParser.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EnumParser;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Moq;

    [TestClass]
    public class EnumManagerTests
    {
        enum TesztEnum : int
        {
            e1 = 1
        }

        private static IEnumDescriptor enumDescriptor;

        private static IEnumTypeGenerator enumGenerator;

        private static IEnumManager enumManager;

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            Mock<IEnumDescriptor> enumDescriptorMock = new Mock<IEnumDescriptor>();
            enumDescriptorMock.Setup(instance => instance.DataType).Returns(typeof(int));
            enumDescriptorMock.Setup(instance => instance.Enumerations).Returns(new List<string>() { "e1,1" });
            enumDescriptorMock.Setup(instance => instance.EnumName).Returns("TesztEnum");
            enumDescriptor = enumDescriptorMock.Object;

            Mock<IEnumTypeGenerator> enumTypeGeneratorMock = new Mock<IEnumTypeGenerator>();

            enumTypeGeneratorMock.Setup(instance => instance.CreateEnumType(It.IsAny<IEnumDescriptor>())).Returns(typeof(TesztEnum));

            enumGenerator = enumTypeGeneratorMock.Object;

            enumManager = new EnumManager(enumGenerator);
        }

        [TestMethod]
        public void TestAddEnumType()
        {
            var result = enumManager.AddEnumType(enumDescriptor);
            Assert.IsNotNull(result);
            Assert.IsTrue(enumManager.Any(e => e.Key.Equals("TesztEnum")));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddEnumTypeWithNull()
        {
            var result = enumManager.AddEnumType(null);
        }
    }
}