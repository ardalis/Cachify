using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Cachify;
using System.Reflection;

namespace BenchmarksConsole
{
    [SampleClass]
    public class Customer
    {
        public int Id { get; set; }

        [SampleProperty]
        public string Name { get; set; }

        [SampleMethod]
        public string SayHello()
        {
            return "Hello " + Name;
        }
    }

    public class SampleClassAttribute : Attribute
    {
        
    }

    public class SampleMethodAttribute : Attribute
    {
        
    }

    public class SamplePropertyAttribute : Attribute
    {
        
    }
    class Program
    {
        static void Main(string[] args)
        {
            int iterations = 10 * 1000 * 1000;

            Console.WriteLine("Cachify<T> Static Extensions");
            Console.WriteLine("----------------------------");
            // Properties
            var propertyAccess = MeasureIterations(iterations, () =>
            {
                var result = typeof(Customer).GetProperties();
            });
            Console.WriteLine("GetProperties: " + propertyAccess);

            var cachedPropertyAccess = MeasureIterations(iterations, () =>
            {
                var result = Cachify<Customer>.Properties;
            });
            Console.WriteLine("GetProperties (Cached): " + cachedPropertyAccess);


            // Class Attributes
            var classAttributeAccess = MeasureIterations(iterations, () =>
            {
                var result = typeof(Customer).GetCustomAttributes(false);
            });
            Console.WriteLine("GetClassAttributes: " + classAttributeAccess);

            var cachedClassAttributeAccess = MeasureIterations(iterations, () =>
            {
                var result = Cachify<Customer>.Attributes;
            });
            Console.WriteLine("GetClassAttributes (Cached): " + cachedClassAttributeAccess);

            // Property Attributes
            var property = typeof (Customer).GetProperties().First(p => p.Name == "Name");
            var propertyAttributeAccess = MeasureIterations(iterations, () =>
            {
                var result = property.GetCustomAttributes();
            });
            Console.WriteLine("GetPropertyAttributes: " + propertyAttributeAccess);

            var cachedProperty = Cachify<Customer>.Properties.First(p => p.Name == "Name");
            var cachedPropertyAttributeAccess = MeasureIterations(iterations, () =>
            {
                var result = cachedProperty.GetAttributes();
            });
            Console.WriteLine("GetPropertyAttributes (Cached): " + cachedPropertyAttributeAccess);

            Console.ReadLine();
        }

        private static long MeasureIterations(int iterations, Action action)
        {
            var stopWatch = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                action();
            }
            stopWatch.Stop();
            return stopWatch.ElapsedMilliseconds;
        }
    }
}
