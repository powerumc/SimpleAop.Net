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
        void Test(string a, object b, int c);
    }

    public class TestClass : ITestClass
    {
        public TestClass(string a, string b)
        {
            
        }
        
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
        public void Test(string a, object b, int c)
        {
            Console.WriteLine("Hello World Test");
        }
    }

    public class ProxyTestClass : TestClass
    {
        public ProxyTestClass(string a, string b) : base(a, b)
        {
            
        }
    }
}