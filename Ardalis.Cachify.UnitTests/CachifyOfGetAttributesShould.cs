using System;
using Xunit;

namespace Ardalis.Cachify.UnitTests
{
    public class CachifyOfGetAttributesShould
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

            

            cachedAttributes = CachifyOf<TestClass>.Attributes;
            cachedAttributes = CachifyOf<TestClass>.Attributes;
            cachedAttributes = CachifyOf<TestClass>.Attributes;

            Assert.Equal(attributes, cachedAttributes);

        }
    }
}
