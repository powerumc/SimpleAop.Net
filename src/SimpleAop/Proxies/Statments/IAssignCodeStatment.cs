namespace SimpleAop.Proxies.Statments
{
    public interface IAssignCodeStatment : IEmitWriteable
    {
        Operand Assign(Operand left, Operand right);
        Operand AssignValue(Operand left, object value);
        Operand AssignValueToProperty(Operand left);
    }
}