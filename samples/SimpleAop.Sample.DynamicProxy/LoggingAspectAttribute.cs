using System;
using System.Threading.Tasks;

namespace SimpleAop.Sample.DynamicProxy
{
    public class LoggingAspectAttribute : OnMethodBoundAspectAttribute
    {
        public override Task OnBeforeAsync(IAspectInvocation invocation)
        {
            Console.WriteLine($"--- Before: {invocation.Method}, {invocation.Object}, {string.Join(",", invocation.Parameters)} ---");

            return Task.CompletedTask;
        }

        public override Task OnAfterAsync(IAspectInvocation invocation)
        {
            Console.WriteLine("--- After ---");

            return Task.CompletedTask;
        }
    }
}