namespace SimpleAop.Proxies
{
    public interface IAccessorConfirmLambda<out TReturn>
    {
        TReturn IsPublic { get; }
        TReturn IsInternal { get; }
        TReturn IsProtected { get; }
        TReturn IsPrivate { get; }
        TReturn IsStatic { get; }
        TReturn IsReadOnly { get; }
        TReturn IsAbstract { get; }
        TReturn IsSealed { get; }
        TReturn IsOverride { get; }
        TReturn IsVirtual { get; }
    }
    
    public interface IAccessorConfirmLambda : IAccessorConfirmLambda<bool>
    {
    }
}