using System;
using System.Linq;
using System.Reflection;

namespace Ardalis.Cachify
{
    public class Cachify<T>
    {
        public static readonly Attribute[] Attributes;
        public static readonly MemberInfo[] Members;
        public static readonly CachedProperty[] Properties;
        static Cachify()
        {
            Type theType = typeof (T);
            if (Attributes == null)
            {
                Attributes = GetAttributes(theType);
            }
            if (Members == null)
            {
                Members = GetMembers(theType);
            }
            if (Properties == null)
            {
                Properties = GetProperties(theType);
            }
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

        public static CachedProperty FindProperty(string propertyName)
        {
            string upperName = propertyName.ToLowerInvariant();
            return Properties.FirstOrDefault(p => p.Name.ToLowerInvariant() == upperName);
        }
    }
}