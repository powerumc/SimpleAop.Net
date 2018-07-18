using System;

namespace SimpleAop.Sample.AspNetCoreWeb.Aspects
{
    public class LoggingAspect : OnMethodBoundAspectAttribute
    {
        public override void OnBefore(IAspectInvocation invocation)
        {
            Console.WriteLine("-------------- Before Logging Aspect -------------");
            Console.WriteLine($"Method: {invocation.Method}");
            Console.WriteLine($"Object: {invocation.Object}");
            Console.WriteLine($"Paramters: {string.Join(",", invocation.Parameters)}");
        }

        public override void OnAfter(IAspectInvocation invocation)
        {
            Console.WriteLine("-------------- After Logging Aspect --------------");
        }
    }
}