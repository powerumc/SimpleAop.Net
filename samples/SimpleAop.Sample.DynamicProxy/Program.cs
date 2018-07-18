using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;
using SimpleAop.Extensions;
using SimpleAop.Proxies;
using SimpleAop.Proxies.Extensions;

namespace SimpleAop.Sample.DynamicProxy
{
    class Program
    {
        static void Main(string[] args)
        {
            var type = DynamicProxyFactory.Create(typeof(ITestClass), typeof(TestClass));
            var obj = (ITestClass) Activator.CreateInstance(type, "A", "B");
            obj.Test("A", "B", 3);
        }
    }
}