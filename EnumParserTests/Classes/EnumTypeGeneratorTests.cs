namespace EnumParser.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EnumParser;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Moq;

    [TestClass]
    public sealed class EnumTypeGeneratorTests
    {
        private static IEnumDescriptor enumDescriptor;

        private static IEnumTypeGenerator enumGenerator;

        private static IEnumValueResolver enumValueResolver;

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            Mock<IEnumDescriptor> enumDescriptorMock = new Mock<IEnumDescriptor>();
            enumDescriptorMock.Setup(instance => instance.DataType).Returns(typeof(int));
            enumDescriptorMock.Setup(instance => instance.Enumerations).Returns(new List<string>() { "e1,1" });
            enumDescriptorMock.Setup(instance => instance.EnumName).Returns("TesztEnum");
            enumDescriptorMock.Setup(instance => instance.IsFlag).Returns(true);

            enumDescriptor = enumDescriptorMock.Object;

            Mock<IEnumValueResolver> enumValueResolverMock = new Mock<IEnumValueResolver>();
            enumValueResolverMock.Setup(instance => instance.ResolveEnumValue(It.IsAny<Type>(), It.IsAny<string>())).Returns(new Tuple<string, ValueType>("e1", 1));
            enumValueResolver = enumValueResolverMock.Object;

            enumGenerator = new EnumTypeGenerator(enumValueResolver);
        }

        [TestMethod]
        public void TestCreateEnumTypeWithCorrectInput()
        {
            Type result = enumGenerator.CreateEnumType(enumDescriptor);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsEnum);
            Assert.AreEqual("e1", result.GetEnumNames().GetValue(0));
            Assert.AreEqual(1, Convert.ChangeType(result.GetEnumValues().GetValue(0), result.GetEnumUnderlyingType()));
            Assert.IsTrue(result.GetEnumUnderlyingType() == typeof(int));
            Assert.IsTrue(result.GetCustomAttributes(false).Single(attribute => attribute.GetType() == typeof(FlagsAttribute)) != null);
        }
    }
}