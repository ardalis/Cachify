using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Ardalis.Cachify.Reflection;

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
            GetPropertiesStatic(iterations);
            GetClassAttributesStatic(iterations);
            GetPropertyAttributesStatic(iterations);
            Console.WriteLine();

            Console.WriteLine("Cachify Instance Extensions");
            Console.WriteLine("---------------------------");
            GetPropertiesInstance(iterations);
            GetClassAttributesInstance(iterations);
            // no sense getting property attributes as instance since it'll be same code

            Console.ReadLine();
        }

        private static void GetPropertyAttributesStatic(int iterations)
        {
            var property = typeof(Customer).GetProperties().First(p => p.Name == "Name");
            var propertyAttributeAccess = MeasureIterations(iterations, () => { var result = property.GetCustomAttributes(); });
            Console.WriteLine("GetPropertyAttributes: " + propertyAttributeAccess);

            var cachedProperty = Cachify<Customer>.Properties.First(p => p.Name == "Name");
            var cachedPropertyAttributeAccess = MeasureIterations(iterations,
                () => { var result = cachedProperty.GetAttributes(); });
            Console.WriteLine("GetPropertyAttributes (Cached): " + cachedPropertyAttributeAccess);
        }

        private static void GetClassAttributesStatic(int iterations)
        {
            var classAttributeAccess = MeasureIterations(iterations,
                () => { var result = typeof(Customer).GetCustomAttributes(false); });
            Console.WriteLine("GetClassAttributes: " + classAttributeAccess);

            var cachedClassAttributeAccess = MeasureIterations(iterations, () => { var result = Cachify<Customer>.Attributes; });
            Console.WriteLine("GetClassAttributes (Cached): " + cachedClassAttributeAccess);
        }

        private static void GetClassAttributesInstance(int iterations)
        {
            var customer = new Customer();

            var classAttributeAccess = MeasureIterations(iterations,
                () => { var result = customer.GetType().GetCustomAttributes(false); });
            Console.WriteLine("Instance GetClassAttributes: " + classAttributeAccess);

            var cachedClassAttributeAccess = MeasureIterations(iterations, () => { var result = customer.Cachify().GetAttributes(); });
            Console.WriteLine("Instance Cachify GetAttributes: " + cachedClassAttributeAccess);
        }

        private static void GetPropertiesStatic(int iterations)
        {
            var propertyAccess = MeasureIterations(iterations, () => { var result = typeof(Customer).GetProperties(); });
            Console.WriteLine("GetProperties: " + propertyAccess);

            var cachedPropertyAccess = MeasureIterations(iterations, () => { var result = Cachify<Customer>.Properties; });
            Console.WriteLine("GetProperties (Cached): " + cachedPropertyAccess);
        }

        private static void GetPropertiesInstance(int iterations)
        {
            var customer = new Customer();

            var propertyAccess = MeasureIterations(iterations, () => { var result = customer.GetType().GetProperties(); });
            Console.WriteLine("Instance GetType GetProperties: " + propertyAccess);

            // Note: This form must still call GetType() every iteration
            var cachedPropertyAccess = MeasureIterations(iterations, () => { var result = customer.Cachify().GetProperties(); });
            Console.WriteLine("Instance Cachify GetProperties: " + cachedPropertyAccess);
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
