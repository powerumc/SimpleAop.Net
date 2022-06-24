using System;
using System.Threading.Tasks;

namespace SimpleAop
{
    public delegate void AspectInvocationDelegate();

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public abstract class OnMethodBoundAspectAttribute : Attribute
    {
        public bool IsAwait { get; set; } = true;

        public abstract Task OnBeforeAsync(IAspectInvocation invocation);
        public abstract Task OnAfterAsync(IAspectInvocation invocation);
    }
}