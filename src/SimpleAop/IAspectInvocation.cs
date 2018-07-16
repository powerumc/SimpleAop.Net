using System.Reflection;

namespace SimpleAop
{
    public interface IAspectInvocation
    {
        object[] Parameters { get; set; }
        MethodBase Method { get; set; }
        object Object { get; set; }
    }
}