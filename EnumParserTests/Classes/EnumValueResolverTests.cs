namespace EnumParser.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EnumParser;
    using System;

    [TestClass]
    public sealed class EnumValueResolverTests
    {
        private const string rawEnumHex = "EnumValueName,0x1";
        private const string rawEnumDec = "EnumValueName,1";

        private static IEnumValueResolver enumValueResolver;

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            enumValueResolver = new EnumValueResolver(',');
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestResolveEnumValueWithNullRawEnumValue()
        {
            var result = enumValueResolver.ResolveEnumValue(typeof(int), null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestResolveEnumValueWithNullType()
        {
            var result = enumValueResolver.ResolveEnumValue(null, rawEnumHex);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestResolveEnumValueWithUnSupportedType()
        {
            var result = enumValueResolver.ResolveEnumValue(typeof(double), rawEnumHex);
        }

        [TestMethod]
        public void TestResolveEnumValueWithMissingEnumValueName()
        {
            string rawValue = "0x0001";
            Tuple<string, ValueType> expectedValue = new Tuple<string, ValueType>("", 1);
            Assert.AreEqual(expectedValue, enumValueResolver.ResolveEnumValue(typeof(int), rawValue));
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestResolveEnumValueWithMissingEnumValue()
        {
            enumValueResolver.ResolveEnumValue(typeof(int), "Raw");
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void TestResolveEnumValueWithOverflow()
        {
            enumValueResolver.ResolveEnumValue(typeof(byte), "Raw,0x1111");
        }

        [TestMethod]
        public void TestResolveEnumValueWithHexValue()
        {
            Tuple<string, ValueType> expectedValue = new Tuple<string, ValueType>("EnumValueName", 1);
            Assert.AreEqual(expectedValue, enumValueResolver.ResolveEnumValue(typeof(int),rawEnumHex));
        }

        [TestMethod]
        public void TestResolveEnumValueWithDecValue()
        {
            Tuple<string, ValueType> expectedValue = new Tuple<string, ValueType>("EnumValueName", 1);
            Assert.AreEqual(expectedValue, enumValueResolver.ResolveEnumValue(typeof(int), rawEnumDec));
        }

        [TestMethod]
        public void TestResolveEnumValueWithVariousUnderlyingTypes()
        {
            string enumValueName = "EnumValueName";
            ValueType value = 1;
            Type[] types = { typeof(byte), typeof(ushort), typeof(short), typeof(int), typeof(uint), typeof(long), typeof(ulong) };

            foreach (Type type in types)
            {
                var result = enumValueResolver.ResolveEnumValue(type, rawEnumDec);

                Assert.AreEqual(enumValueName, result.Item1);
                Assert.AreEqual(Convert.ChangeType(value, type) , result.Item2);
            }
        }
    }
}