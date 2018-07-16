using System;
using System.Reflection;

namespace SimpleAop.Proxies.Criteria
{
    public class ParameterCriteriaMetadataInfo : CriteriaMetadataInfo
    {
        public ParameterAttributes ParameterAttribute { get; private set; }

        public ParameterCriteriaMetadataInfo(Type type)
            : this(type, string.Empty)
        {
        }

        public ParameterCriteriaMetadataInfo(Type type, string name,
            ParameterAttributes parameterAttribute = ParameterAttributes.HasDefault)
            : base(type, name, CriteriaMetadataToken.Method)
        {
            this.ParameterAttribute = parameterAttribute;
        }
    }
}