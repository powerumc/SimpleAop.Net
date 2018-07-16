namespace SimpleAop.Proxies
{
    public interface IMetadata<out TReturn>
    {
        TReturn Attribute();
        TReturn Attribute(params object[] @object);
    }
    
    public interface IMetadata : IMetadata<IMetadata>
    {
    }
}