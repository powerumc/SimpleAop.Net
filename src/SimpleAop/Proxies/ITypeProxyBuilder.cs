using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using SimpleAop.Proxies.Accessors;
using SimpleAop.Proxies.Criteria;
using SimpleAop.Proxies.Statments;

namespace SimpleAop.Proxies
{
	public interface ITypeProxyBuilder : 
		IAccessorBuilder<ITypeProxyBuilder>,
		IReleaseType
	{
		TypeAccessor TypeAccessor { get; }
		FieldAccessor FieldAccessor { get; }
		MethodAccessor MethodAccessor { get; }

		TypeBuilder TypeBuilder { get; }

		ITypeProxyBuilder Attribute(Type attributeType, params object[] param);
		Operand Field(Type returnType, string name);
		IPropertyStatment Property(Type returnType, string name);
		IPropertyStatment Property(Type returnType, string name, CallingConventions callingConventions);
		IStatment Method(string name);
		IStatment Method(Type returnType, string name, Type[] argumentsTypes);
		IStatment Method(Type returnType, string name, Type[] argumentsTypes, MethodInfo parentMethodInfo);
		IStatment Constructor();
		IStatment Constructor(params Type[] argumentsTypes);
		IStatment Constructor(IEnumerable<ParameterCriteriaMetadataInfo> parameterCriteriaMetadataInfos);

		ITypeProxyBuilder Class(string name);
		ITypeProxyBuilder Class(string name, Type parent, Type[] interfaces);
		ITypeProxyBuilder Struct(string name);
		ITypeProxyBuilder Interface(string name);
		ITypeProxyBuilder Delegate(Type returnType, string name, params Type[] argumentsTypes);
		ITypeProxyBuilder Event(Type delegateType, string name);
	}
}