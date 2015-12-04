using System;
using Xunit;

namespace Ardalis.Cachify.UnitTests
{
    public class CachifyOfTGetAttributesShould
    {
        [MyTest]
        private class TestClass
        {
        }

        private class MyTestAttribute : Attribute
        { }

        [Fact]
        public void ReturnSameAttributesAsReflection()
        {
            var attributes = Attribute.GetCustomAttributes(typeof (TestClass));

            object[] cachedAttributes;

            

            cachedAttributes = Cachify<TestClass>.Attributes;
            cachedAttributes = Cachify<TestClass>.Attributes;
            cachedAttributes = Cachify<TestClass>.Attributes;

            Assert.Equal(attributes, cachedAttributes);

        }
    }
}
