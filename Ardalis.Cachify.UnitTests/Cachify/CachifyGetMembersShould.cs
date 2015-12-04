using System.Linq;
using System.Reflection;
using Xunit;

namespace Ardalis.Cachify.UnitTests.Cachify
{
    public class CachifyGetMembersShould
    {
        private class TestClass
        {
            public void Foo()
            {
            }

            private void Bar()
            {
            }
        }

        [Fact]
        public void ReturnSamePropertiesAsReflection()
        {
            var members = new TestClass().GetType().GetMembers(); // reflection

            MemberInfo[] cachedMembers;

            cachedMembers = new TestClass().Cachify().GetMembers();
            cachedMembers = new TestClass().Cachify().GetMembers();
            cachedMembers = new TestClass().Cachify().GetMembers();

            Assert.Equal(members.Count(), cachedMembers.Count());
            Assert.Equal(members.FirstOrDefault().Name, cachedMembers.FirstOrDefault().Name);
        }
    }
}
