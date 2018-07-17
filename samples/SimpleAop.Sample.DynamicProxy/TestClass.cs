using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SimpleAop.Sample.DynamicProxy
{
    public interface ITestClass
    {
        void Print();
        string GetString();
        Task<string> GetStringAsync();
        IEnumerable<string> GetStringEnumerable();
    }

    public class TestClass : ITestClass
    {
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

        public IEnumerable<string> GetStringEnumerable()
        {
            yield return "A";
            yield return "B";
            yield return "C";
        }
    }

    [LoggingAspect]
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
            
            var classAttributes = GetType().BaseType.GetCustomAttributes(typeof(OnMethodBoundAspectAttribute), false);
            var methodAttributes = method.GetCustomAttributes(typeof(OnMethodBoundAspectAttribute), false);

            foreach (OnMethodBoundAspectAttribute attribute in classAttributes)
            {
                attribute.OnBefore(invocation);
            }

            foreach (OnMethodBoundAspectAttribute attribute in methodAttributes)
            {
                attribute.OnBefore(invocation);
            }

            result = base.GetString();
            
            foreach (OnMethodBoundAspectAttribute attribute in classAttributes)
            {
                attribute.OnAfter(invocation);
            }
            
            foreach (OnMethodBoundAspectAttribute attribute in methodAttributes)
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