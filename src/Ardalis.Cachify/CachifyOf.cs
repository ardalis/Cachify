using System;
using System.Linq;
using System.Reflection;

namespace Ardalis.Cachify
{
    public class CachifyOf<T>
    {
        public static readonly Attribute[] Attributes;
        public static readonly MemberInfo[] Members;
        public static readonly CachedProperty[] Properties;
        static CachifyOf()
        {
            Type theType = typeof (T);
            Attributes = GetAttributes(theType);
            Members = GetMembers(theType);
            Properties = GetProperties(theType);
        }

        private static MemberInfo[] GetMembers(Type myType)
        {
            return myType.GetMembers();
        }

        private static CachedProperty[] GetProperties(Type myType)
        {
            return myType.GetProperties().Select(p => new CachedProperty(p)).ToArray();
        }

        private static Attribute[] GetAttributes(Type myType)
        {
            return Attribute.GetCustomAttributes(myType);
        }
    }
}