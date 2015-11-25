using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ardalis.Cachify
{
    public class CachedProperty
    {
        public CachedProperty(PropertyInfo propertyInfo)
        {
            PropertyInfo = propertyInfo;
        }
        public PropertyInfo PropertyInfo { get; set; }
        private IEnumerable<Attribute> _attributes;

        public IEnumerable<Attribute> GetAttributes()
        {
            if (_attributes == null)
            {
                _attributes = this.PropertyInfo.GetCustomAttributes();
            }
            return _attributes;
        }

        public string Name
        {
            get { return PropertyInfo.Name; }
        }
    }
}