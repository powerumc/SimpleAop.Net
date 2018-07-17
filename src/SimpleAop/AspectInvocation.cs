using System.Reflection;

namespace SimpleAop
{
    public class AspectInvocation : IAspectInvocation
    {
        public AspectInvocation(MethodBase method, object o, object[] parameters)
        {
            Method = method;
            Object = o;
            Parameters = parameters;
        }

        public MethodBase Method { get; set; }
        public object Object { get; set; }
        public object[] Parameters { get; set; }
    }
}