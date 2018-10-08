namespace EnumParser.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EnumParser;
    using System.Collections.Generic;
    using Moq;

    [TestClass()]
    public class EnumHelperTests
    {
        enum TesztEnum : int
        {
            e1 = 1
        }

        private static IEnumDescriptor enumDescriptor;

        private static IEnumHelper enumHelper;

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            Mock<IEnumDescriptor> enumDescriptorMock = new Mock<IEnumDescriptor>();
            enumDescriptorMock.Setup(instance => instance.DataType).Returns(typeof(int));
            enumDescriptorMock.Setup(instance => instance.Enumerations).Returns(new List<string>() { "e1,1" });
            enumDescriptor = enumDescriptorMock.Object;

            enumHelper = new EnumHelper(typeof(TesztEnum), enumDescriptor);
        }

        [TestMethod]
        public void TestGetValue()
        {
            var result = enumHelper.GetValue("e1");
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void TestGetName()
        {
            var result = enumHelper.GetName(1);
            Assert.AreEqual("e1", result);
        }
    }
}