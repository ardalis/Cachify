using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ardalis.Cachify.Reflection
{
    public static class CachifyExtensions
    {
        public static Cachify Cachify(this object theObject)
        {
            return new Cachify(theObject);
        }

        public static Cachify<T> CachifyOf<T>(this T theType)
        {
            return new Cachify<T>();
        } 
    }
}
