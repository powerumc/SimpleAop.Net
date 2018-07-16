namespace SimpleAop.Proxies
{
    public interface IValuable
    {
        object Value { get; }
    }
    
    public interface IValuable<out TReturn>
    {
        TReturn Value { get; }
    }
}