﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ardalis.Cachify
{
    public class Cachify
    {
        private readonly object _theObject;
        private readonly Type _theObjectType;
        private static readonly Dictionary<Type, CachedProperty[]> TypePropertyCache = new Dictionary<Type, CachedProperty[]>();
        private static readonly Dictionary<Type, MemberInfo[]> TypeMemberCache = new Dictionary<Type, MemberInfo[]>();
        private static readonly Dictionary<Type, Attribute[]> TypeAttributeCache = new Dictionary<Type, Attribute[]>();


        public Cachify(object theObject)
        {
            _theObject = theObject;
            _theObjectType = _theObject.GetType();
        }

        public CachedProperty[] GetProperties()
        {
            CachedProperty[] result;
            if (!TypePropertyCache.TryGetValue(_theObjectType, out result))
            {
                result = _theObjectType.GetProperties().Select(p => new CachedProperty(p)).ToArray();
                TypePropertyCache.Add(_theObjectType, result);
            }
            return result;
        }

        private MemberInfo[] GetMembers()
        {
            MemberInfo[] result;
            if (!TypeMemberCache.TryGetValue(_theObjectType, out result))
            {
                result = _theObjectType.GetMembers();
                TypeMemberCache.Add(_theObjectType, _theObjectType.GetMembers());
            }
            return result;
        }

        private Attribute[] GetAttributes()
        {
            Attribute[] result;
            if (!TypeAttributeCache.TryGetValue(_theObjectType, out result))
            {
                result = _theObjectType.GetCustomAttributes(true) as Attribute[];
                if (result != null)
                {
                    TypeAttributeCache.Add(_theObjectType, _theObjectType.GetCustomAttributes(true) as Attribute[]);
                }
            }
            return result;
        }

        public CachedProperty FindProperty(string propertyName)
        {
            string lowerName = propertyName.ToLowerInvariant();
            return this.GetProperties()
                .FirstOrDefault(p => p.Name.ToLowerInvariant() == lowerName);
        }

    }
}