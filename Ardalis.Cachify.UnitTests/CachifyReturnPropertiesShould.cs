using System.Linq;
using System.Reflection;
using Xunit;

namespace Ardalis.Cachify.UnitTests
{
    public class CachifyReturnPropertiesShould
    {
        private class TestClass
        {
            public string Prop1 { get; set; }
            public string Prop2 { get; set; }
            public string Prop3 { get; set; }
        }

        [Fact]
        public void ReturnSamePropertiesAsReflection()
        {
            var properties = new TestClass().GetType().GetProperties(); // reflection

            CachedProperty[] cachedProperties;

            cachedProperties = new TestClass().Cachify().GetProperties();
            cachedProperties = new TestClass().Cachify().GetProperties();
            cachedProperties = new TestClass().Cachify().GetProperties();

            Assert.Equal(properties.Count(), cachedProperties.Count());
            Assert.Equal(properties.FirstOrDefault().Name, cachedProperties.FirstOrDefault().Name);
        }
    }
}
