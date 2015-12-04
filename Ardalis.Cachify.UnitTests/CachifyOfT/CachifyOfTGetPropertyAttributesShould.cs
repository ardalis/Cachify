using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Ardalis.Cachify.UnitTests.CachifyOfT
{
    public class CachifyOfTGetPropertyAttributesShould
    {
        private class TestClass
        {
            [MyTest]
            public string Name { get; set; }
        }

        private class MyTestAttribute : Attribute
        { }

        [Fact]
        public void ReturnSameAttributesAsReflection()
        {
            var propertyInfo = typeof (TestClass).GetProperties().First(p=>p.Name=="Name");
            var attributes = propertyInfo.GetCustomAttributes();

            Attribute[] cachedAttributes;
            cachedAttributes = Cachify<TestClass>.Properties.First(p => p.Name == "Name")
                .GetAttributes()
                .ToArray();
            cachedAttributes = Cachify<TestClass>.Properties.First(p => p.Name == "Name")
                .GetAttributes()
                .ToArray();
            cachedAttributes = Cachify<TestClass>.Properties.First(p => p.Name == "Name")
                .GetAttributes()
                .ToArray();

            Assert.Equal(attributes, cachedAttributes);

        }
    }
}
