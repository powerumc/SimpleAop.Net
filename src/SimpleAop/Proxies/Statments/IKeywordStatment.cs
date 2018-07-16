namespace SimpleAop.Proxies.Statments
{
    public interface IKeywordStatment
    {

        IStatment New(ITypeProxyBuilder typeProxyBuilder);
        IStatment Return();
        IStatment Return(Operand operand);
        IStatment Break();
        IStatment Continue();
    }
}