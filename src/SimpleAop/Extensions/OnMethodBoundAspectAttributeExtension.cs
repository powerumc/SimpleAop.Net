using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SimpleAop.Extensions
{
    public static class OnMethodBoundAspectAttributeExtension
    {
        public static OnMethodBoundAspectAttribute[] GetOnMethodBoundAspectAttributes(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            
            if (type.BaseType == null)
                throw new NullReferenceException(nameof(type.BaseType));
                
            return type.BaseType.GetCustomAttributes(typeof(OnMethodBoundAspectAttribute), false).Cast<OnMethodBoundAspectAttribute>().ToArray();
        }
        
        public static OnMethodBoundAspectAttribute[] GetOnMethodBoundAspectAttributes(this MethodBase method)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));
            
            return method.GetCustomAttributes(typeof(OnMethodBoundAspectAttribute), false).Cast<OnMethodBoundAspectAttribute>().ToArray();
        }
        
        public static void ForEachOnBefore(this OnMethodBoundAspectAttribute[] attributes, IAspectInvocation invocation)
        {
            foreach (var attribute in attributes)
            {
                attribute.OnBefore(invocation);
            }
        }
        
        public static void ForEachOnAfter(this OnMethodBoundAspectAttribute[] attributes, IAspectInvocation invocation)
        {
            foreach (var attribute in attributes)
            {
                attribute.OnAfter(invocation);
            }
        }

        public static MethodBase GetCustomAttributeOnBaseMethod(this MethodBase currentMethod)
        {
            if (currentMethod == null)
                throw new ArgumentNullException(nameof(currentMethod));
            
            if (currentMethod.DeclaringType == null)
                throw new NullReferenceException(nameof(currentMethod.DeclaringType));
            
            return currentMethod.DeclaringType.BaseType.GetMethod(currentMethod.Name, currentMethod.GetParameters().Select(o => o.ParameterType).ToArray());
        }
    }
}