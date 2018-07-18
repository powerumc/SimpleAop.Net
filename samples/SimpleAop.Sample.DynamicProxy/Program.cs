using System;

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