using System;

namespace SimpleAop
{
    public delegate void AspectInvocationDelegate();
    
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public abstract class OnMethodBoundAspectAttribute : Attribute
    {
        public abstract void OnBefore(IAspectInvocation invocation);
        public abstract void OnAfter(IAspectInvocation invocation);
    }

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