using System.Reflection;
using Xunit;

namespace Ardalis.Cachify.UnitTests.CachifyOfT
{
    public class CachifyOfTGetMembersShould
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
        public void ReturnSameMembersAsReflection()
        {
            var members = typeof (TestClass).GetMembers();

            MemberInfo[] cachedMembers;

            cachedMembers = Cachify<TestClass>.Members;
            cachedMembers = Cachify<TestClass>.Members;
            cachedMembers = Cachify<TestClass>.Members;

            Assert.Equal(members, cachedMembers);

        }
    }
}
