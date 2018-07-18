using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using SimpleAop.Extensions;

namespace SimpleAop.Sample.DynamicProxy
{
    public interface ITestClass
    {
        void Print();
        string GetString();
        Task<string> GetStringAsync();
        void Test(string a, object b, int c, List<string> d);
    }

    public class TestClass : ITestClass
    {
        [LoggingAspect]
        public void Print()
        {
            Console.WriteLine("Hello World 1");
        }

        public string GetString()
        {
            return "Hello World 2";
        }

        public Task<string> GetStringAsync()
        {
            return Task.FromResult("Hello World 3");
        }

        [LoggingAspect]
        public void Test(string a, object b, int c, List<string> d)
        {
            var arr = new object[] {a, b, c, d};

            var method = MethodBase.GetCurrentMethod();
            var baseMethod = method.GetCustomAttributeOnBaseMethod();
        }
    }

    public class ProxyTestClass : TestClass, ITestClass
    {
        void ITestClass.Print()
        {
            base.Print();
        }

        string ITestClass.GetString()
        {
            var result = default(string);
            var method = MethodBase.GetCurrentMethod();
            var invocation = new AspectInvocation(method, this, null);

            
            var classAttributes = GetType().GetOnMethodBoundAspectAttributes();
            var methodAttributes = method.GetOnMethodBoundAspectAttributes();

            classAttributes.ForEachOnBefore(invocation);
            
            foreach (var attribute in classAttributes)
            {
                attribute.OnBefore(invocation);
            }
            foreach (var attribute in methodAttributes)
            {
                attribute.OnBefore(invocation);
            }

            result = base.GetString();
            
            foreach (var attribute in classAttributes)
            {
                attribute.OnAfter(invocation);
            }
            
            foreach (var attribute in methodAttributes)
            {
                attribute.OnAfter(invocation);
            }
            
            return result;
        }

        Task<string> ITestClass.GetStringAsync()
        {
            return base.GetStringAsync();
        }

        void test1()
        {
            var arr = new[] {1, 2, 3, 4, 5};

            foreach (var value in arr)
            {
                Console.WriteLine(value);
            }
        }
    }
}