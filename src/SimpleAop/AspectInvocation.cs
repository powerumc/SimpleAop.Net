using System.Reflection;

namespace SimpleAop
{
    public class AspectInvocation : IAspectInvocation
    {
        public object[] Parameters { get; set; }
        public MethodBase Method { get; set; }
        public object Object { get; set; }
    }
}