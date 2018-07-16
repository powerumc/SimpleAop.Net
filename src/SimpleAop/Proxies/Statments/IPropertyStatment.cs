namespace SimpleAop.Proxies.Statments
{
    public interface IPropertyStatment : IAccessorLambda
    {
        IStatment Get();
        IStatment Set();
        ITypeProxyBuilder GetSet();
    }
}