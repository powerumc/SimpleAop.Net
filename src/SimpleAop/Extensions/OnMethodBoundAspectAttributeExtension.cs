using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SimpleAop.Extensions
{
    public static class OnMethodBoundAspectAttributeExtension
    {
        public static IEnumerable<OnMethodBoundAspectAttribute> GetOnMethodBoundAspectAttributes(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            
            if (type.BaseType == null)
                throw new NullReferenceException(nameof(type.BaseType));
                
            return type.BaseType
                .GetCustomAttributes(typeof(OnMethodBoundAspectAttribute), false)
                .Cast<OnMethodBoundAspectAttribute>();
        }
        
        public static IEnumerable<OnMethodBoundAspectAttribute> GetOnMethodBoundAspectAttributes(this MethodBase method)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));
            
            return method
                .GetCustomAttributes(typeof(OnMethodBoundAspectAttribute), false)
                .Cast<OnMethodBoundAspectAttribute>();
        }
        
        public static async Task ForEachOnBeforeAsync(this IEnumerable<OnMethodBoundAspectAttribute> attributes, IAspectInvocation invocation)
        {
            if (attributes.All(attr => attr.IsAwait))
            {
                foreach (var attribute in attributes)
                {
                    await attribute.OnBeforeAsync(invocation);
                }
            }
            else
            {
                await Task.WhenAll(attributes.Select(attr => attr.OnBeforeAsync(invocation)));
            }
        }
        
        public static async Task ForEachOnAfterAsync(this IEnumerable<OnMethodBoundAspectAttribute> attributes, IAspectInvocation invocation)
        {
            if (attributes.All(attr => attr.IsAwait))
            {
                foreach (var attribute in attributes)
                {
                    await attribute.OnAfterAsync(invocation);
                }
            }
            else
            {
                await Task.WhenAll(attributes.Select(attr => attr.OnAfterAsync(invocation)));
            }
        }

        public static MethodBase GetCustomAttributeOnBaseMethod(this MethodBase currentMethod)
        {
            if (currentMethod == null)
                throw new ArgumentNullException(nameof(currentMethod));
            
            if (currentMethod.DeclaringType == null)
                throw new NullReferenceException(nameof(currentMethod.DeclaringType));

            if (currentMethod.DeclaringType.BaseType == null)
                throw new NullReferenceException(nameof(currentMethod.DeclaringType.BaseType));

            var parameterTypes = currentMethod.GetParameters().Select(o => o.ParameterType).ToArray();
            
            return currentMethod.DeclaringType.BaseType.GetMethod(currentMethod.Name, parameterTypes);
        }
    }
}