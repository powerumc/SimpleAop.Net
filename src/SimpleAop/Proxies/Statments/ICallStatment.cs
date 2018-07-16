using System.Reflection;

namespace SimpleAop.Proxies.Statments
{
    public interface ICallStatment
    {
        IStatment Call();
        IStatment Call(Operand operand);
        IStatment Call(MethodInfo methodInfo, params Operand[] methodArguments);
    }
}