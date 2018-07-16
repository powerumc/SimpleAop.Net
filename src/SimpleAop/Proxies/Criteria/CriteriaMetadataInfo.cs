using System;

namespace SimpleAop.Proxies.Criteria
{
    public class CriteriaMetadataInfo : ICriteriaMetadataInfo
    {

        public CriteriaMetadataToken Token { get; }
        public Type Type { get; }
        public string Name { get; }

        public CriteriaMetadataInfo(Type type, string name, CriteriaMetadataToken token)
        {
            this.Type = type;
            this.Name = name;
            this.Token = token;
        }
    }
}