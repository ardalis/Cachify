using System.Reflection;
using Xunit;

namespace Ardalis.Cachify.UnitTests
{
    public class CachifyGetPropertiesShould
    {
        private class TestClass
        {
            public string Prop1 { get; set; }
            public string Prop2 { get; set; }
            public string Prop3 { get; set; }
        }
        [Fact]
        public void ReturnProperties()
        {
            var properties = new TestClass().GetType().GetProperties();

            PropertyInfo[] cachedProperties;

            cachedProperties = new TestClass().Cachify().GetProperties();
            cachedProperties = new TestClass().Cachify().GetProperties();
            cachedProperties = new TestClass().Cachify().GetProperties();

            Assert.Equal(properties, cachedProperties);

        }
    }
}
