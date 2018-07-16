using System.Reflection.Emit;

namespace SimpleAop.Proxies.Statments
{
    public interface IStatment : 
        IAssignCodeStatment,
        IBlockStatment,
        ICallStatment,
        IIfStatment,
        ILocalStatment,
        IKeywordStatment,
        INativeStatment<ILGenerator>,
        IExceptionHandleStatment
    {
        ITypeProxyBuilder TypeProxyBuilder { get; }
        ILGenerator IL { get; }
    }
}