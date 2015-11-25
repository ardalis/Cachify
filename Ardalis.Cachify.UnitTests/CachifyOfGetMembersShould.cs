using System;
using System.Reflection;
using Xunit;

namespace Ardalis.Cachify.UnitTests
{
    public class CachifyOfGetMembersShould
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

            cachedMembers = CachifyOf<TestClass>.Members;
            cachedMembers = CachifyOf<TestClass>.Members;
            cachedMembers = CachifyOf<TestClass>.Members;

            Assert.Equal(members, cachedMembers);

        }
    }
}
