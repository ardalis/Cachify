using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ardalis.Cachify
{
    public static class CachifyExtensions
    {
        public static Cachify Cachify(this object theObject)
        {
            return new Cachify(theObject);
        }

        public static CachifyOf<T> CachifyOf<T>(this T theType)
        {
            return new CachifyOf<T>();
        } 
    }
}
