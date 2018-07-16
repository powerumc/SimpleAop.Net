namespace SimpleAop.Proxies
{
    public interface IAccessorLambda<out TReturn>
    {
        TReturn Public { get; }
        TReturn Internal { get; }
        TReturn Protected { get; }
        TReturn Private { get; }
        TReturn Static { get; }
        TReturn ReadOnly { get; }
        TReturn Abstract { get; }
        TReturn Sealed { get; }
        TReturn Override { get; }
        TReturn Virtual { get; }
    }

    public interface IAccessorLambda : IAccessorLambda<IAccessorLambda>
    {
    }
}