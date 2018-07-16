using System;

namespace SimpleAop.Proxies.Criteria
{
    public interface ICriteriaMetadataInfo
    {
        CriteriaMetadataToken Token { get; }
        Type Type { get; }
        string Name { get; }
    }
}