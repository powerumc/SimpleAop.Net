using System;

namespace SimpleAop
{
    public delegate void AspectInvocationDelegate();
    
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class OnMethodBoundAspectAttribute : Attribute
    {
        public virtual void OnEnter(IAspectInvocation aspectInvocation, AspectInvocationDelegate next)
        {
            next();
        }

        public virtual void OnException(Exception exception)
        {
            
        }
    }
}