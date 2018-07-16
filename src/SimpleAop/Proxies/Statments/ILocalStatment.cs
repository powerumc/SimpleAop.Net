using System;

namespace SimpleAop.Proxies.Statments
{
    public interface ILocalStatment
    {
        Operand Local(Type type, string name);
    }
}