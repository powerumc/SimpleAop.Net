namespace SimpleAop.Proxies.Statments
{
    public interface IBlockStatment
    {
        IStatment BeginBlock();
        IStatment EndBlock();
    }
}