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
}