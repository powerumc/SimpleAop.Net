using SimpleAop.Proxies.Statments;

namespace SimpleAop.Proxies
{
    public interface IEmitReadable
    {
        void ReadEmit(IStatment statment);
    }
}