using System;

namespace SimpleAop.Proxies.Statments
{
    public interface IExceptionHandleStatment
    {
        IStatment Try();
        IStatment Catch();
        IStatment Catch(Type catchType);
        IStatment Finally();
    }
}