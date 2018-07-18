using System;

namespace SimpleAop.Sample.DynamicProxy
{
    public class LoggingAspectAttribute : OnMethodBoundAspectAttribute
    {
        public override void OnBefore(IAspectInvocation invocation)
        {
            Console.WriteLine($"--- Before: {invocation.Method}, {invocation.Object}, {string.Join(",", invocation.Parameters)} ---");
        }

        public override void OnAfter(IAspectInvocation invocation)
        {
            Console.WriteLine("--- After ---");
        }
    }
}