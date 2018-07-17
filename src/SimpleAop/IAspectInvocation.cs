using System.Reflection;

namespace SimpleAop
{
    public interface IAspectInvocation
    {
        object[] Parameters { get; }
        MethodBase Method { get; }
        object Object { get; }
    }
}