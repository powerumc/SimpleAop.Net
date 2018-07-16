using System;

namespace SimpleAop.Sample.DynamicProxy
{
    public interface ITestClass
    {
        void Print();
        string GetString();
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
    }

    public class ProxyTestClass : TestClass, ITestClass
    {
        void ITestClass.Print()
        {
            base.Print();
        }

        string ITestClass.GetString()
        {
            return base.GetString();
        }
    }
}