using System;
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
        
        void Print(string a, string b);
        void PrintParams(string a, params string[] args);
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

        public void Print(string a, string b)
        {
            Console.WriteLine($"{a}+{b}");
        }

        public void PrintParams(string a, params string[] args)
        {
            Console.WriteLine(a);
            foreach (var str in args)
            {
                Console.WriteLine(str);
            }
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
            var invocation = new AspectInvocation
            {
                Method = MethodBase.GetCurrentMethod(),
                Object = this,
                Parameters = null
            };
            
            var classAttributes = GetType().GetCustomAttributes(typeof(OnMethodBoundAspectAttribute), false);
            var methodAttributes = MethodBase.GetCurrentMethod().GetCustomAttributes(typeof(OnMethodBoundAspectAttribute), false);

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

        void ITestClass.Print(string a, string b)
        {
            base.Print(a, b);
        }

        void ITestClass.PrintParams(string a, params string[] args)
        {
            base.PrintParams(a, args);
        }
    }
}