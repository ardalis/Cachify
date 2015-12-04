using System;
using System.Linq;
using Ardalis.Cachify.UnitTests.CachifyOfT;
using Xunit;

namespace Ardalis.Cachify.UnitTests.Cachify
{
    public class CachifyGetAttributesShould
    {
        [MyTestAttribute]
        private class TestClass
        {
        }

        private class MyTestAttribute : Attribute
        { }

        [Fact]
        public void ReturnSameAttributesAsReflection()
        {
            var attributes = new TestClass().GetType().GetCustomAttributes(true); // reflection

            Attribute[] cachedAttributes;

            cachedAttributes = new TestClass().Cachify().GetAttributes();
            cachedAttributes = new TestClass().Cachify().GetAttributes();
            cachedAttributes = new TestClass().Cachify().GetAttributes();

            Assert.Equal(attributes.Count(), cachedAttributes.Count());
            Assert.Equal(attributes.FirstOrDefault().GetType(), cachedAttributes.FirstOrDefault().GetType());
        }
    }
}
