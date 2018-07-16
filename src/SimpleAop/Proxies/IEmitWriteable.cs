using SimpleAop.Proxies.Statments;

namespace SimpleAop.Proxies
{
    public interface IEmitWriteable
    {
        void WriteEmit(IStatment statment);
    }
}