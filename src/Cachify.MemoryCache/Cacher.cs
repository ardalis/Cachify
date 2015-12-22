using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Cachify
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class Cacher
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<Cacher> _logger;
        public Cacher(IMemoryCache cache,
            ILogger<Cacher> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public T CacheAction<T>(Expression<Func<T>> action, [CallerMemberName] string memberName = "") where T : class
        {
            MethodCallExpression body = (MethodCallExpression)action.Body;

            ICollection<object> parameters = new List<object>();

            foreach (MemberExpression expression in body.Arguments)
            {
                parameters.Add(((FieldInfo)expression.Member).GetValue(((ConstantExpression)expression.Expression).Value));
            }

            var builder = new StringBuilder(100);

            // TODO: See if this can be optimized to avoid reflection
            //var frame = new StackFrame(1);
            //var method = frame.GetMethod();
            //var type = method.DeclaringType;

            var type = typeof(T);

            builder.Append(type.FullName);
            builder.Append(".");
            builder.Append(memberName);

            parameters.ToList().ForEach(x =>
            {
                builder.Append("_");
                builder.Append(x);
            });

            string cacheKey = builder.ToString();

            T value = _cache.Get<T>(cacheKey);

            if (value == null)
            {
                _logger.LogDebug($"Cache MISS for {cacheKey}");
                value = action.Compile().Invoke();
                _cache.Set(cacheKey, value);
            }
            else
            {
                _logger.LogDebug($"Cache hit for {cacheKey}");
            }

            return value;
        }
    }
}
