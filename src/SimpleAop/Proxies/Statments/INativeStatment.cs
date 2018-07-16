namespace SimpleAop.Proxies.Statments
{
    public interface INativeStatment<out TGenerator>
    {
        TGenerator Emit { get; }
        IStatment EmitFromSource();
    }
}