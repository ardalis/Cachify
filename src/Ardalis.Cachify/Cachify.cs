using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ardalis.Cachify
{
    public class Cachify
    {
        private readonly object _theObject;
        private readonly Type _theObjectType;

        public Cachify(object theObject)
        {
            _theObject = theObject;
            _theObjectType = _theObject.GetType();
        }

        private static readonly Dictionary<Type, PropertyInfo[]> TypePropertyCache = new Dictionary<Type, PropertyInfo[]>();
        public PropertyInfo[] GetProperties()
        {
            PropertyInfo[] result;
            if (!TypePropertyCache.TryGetValue(_theObjectType, out result))
            {
                TypePropertyCache.Add(_theObjectType, _theObjectType.GetProperties());
            }
            return result;
        }
    }
}