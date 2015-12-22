using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ardalis.Cachify.Reflection
{
    public class CachedProperty
    {
        public CachedProperty(PropertyInfo propertyInfo)
        {
            PropertyInfo = propertyInfo;
        }
        public PropertyInfo PropertyInfo { get; private set; }
        private IEnumerable<Attribute> _attributes;

        public IEnumerable<Attribute> GetAttributes()
        {
            return _attributes ?? (_attributes = this.PropertyInfo.GetCustomAttributes(true).Cast<Attribute>());
        }

        public string Name => PropertyInfo.Name;
    }
}