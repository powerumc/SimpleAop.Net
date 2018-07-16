using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using SimpleAop.Proxies.Accessors;
using SimpleAop.Proxies.Criteria;
using SimpleAop.Proxies.Statments;

namespace SimpleAop.Proxies
{
    public class AssemblyProxyBuilder : IAssemblyProxyBuilder
    {
        public AssemblyBuilder AssemblyBuilder { get; private set; }
        public string Name { get; private set; }
        public string AssemblyLambdaQualifiedName { get; private set; }

        public IModuleProxyBuilder Assembly()
        {
            return this.Assembly(null);
        }

        public IModuleProxyBuilder Assembly(string assemblyName)
        {
            this.Name = assemblyName;
            this.AssemblyBuilder =
                AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(assemblyName), AssemblyBuilderAccess.Run);

            return new ModuleProxyBuilder(this);
        }

        public TypeBuilder TypeBuilder => throw new NotImplementedException();
        public TypeAccessor TypeAccessor => throw new NotImplementedException();
        public FieldAccessor FieldAccessor => throw new NotImplementedException();
        public MethodAccessor MethodAccessor => throw new NotImplementedException();

        public ITypeProxyBuilder Attribute(Type attributeType, params object[] param)
        {
            throw new NotImplementedException();
        }

        public ITypeProxyBuilder SetParent(string name)
        {
            throw new NotImplementedException();
        }

        public ITypeProxyBuilder AddInterface(string name)
        {
            throw new NotImplementedException();
        }

        public IAssemblyProxyBuilder Attribute()
        {
            throw new NotImplementedException();
        }

        public IAssemblyProxyBuilder Attribute(params object[] @object)
        {
            throw new NotImplementedException();
        }

        public Operand Field(Type returnType, string name)
        {
            throw new NotImplementedException();
        }

        public IPropertyStatment Property(Type returnType, string name)
        {
            throw new NotImplementedException();
        }

        public IPropertyStatment Property(Type returnType, string name, CallingConventions callingConventions)
        {
            throw new NotImplementedException();
        }

        public IStatment Method(string name)
        {
            throw new NotImplementedException();
        }

        public IStatment Method(Type returnType, string name, Type[] argumentsTypes)
        {
            throw new NotImplementedException();
        }

        public IStatment Method(Type returnType, string name, Type[] argumentsTypes, MethodInfo parentMethodInfo)
        {
            throw new NotImplementedException();
        }

        public IStatment Constructor()
        {
            throw new NotImplementedException();
        }

        public IStatment Constructor(params Type[] argumentsTypes)
        {
            throw new NotImplementedException();
        }

        public IStatment Constructor(IEnumerable<ParameterCriteriaMetadataInfo> parameterCriteriaMetadataInfos)
        {
            throw new NotImplementedException();
        }

        public ITypeProxyBuilder Class(string name)
        {
            var typeLambda = this.Assembly().Module().Class(name);

            return typeLambda;
        }

        public ITypeProxyBuilder Class(string name, Type parent, Type[] interfaces)
        {
            var typeLambda = this.Assembly().Module().Class(name, parent, interfaces);

            return typeLambda;
        }

        public ITypeProxyBuilder Struct(string name)
        {
            throw new NotImplementedException();
        }

        public ITypeProxyBuilder Interface(string name)
        {
            throw new NotImplementedException();
        }

        public ITypeProxyBuilder Delegate(Type returnType, string name, params Type[] argumentsTypes)
        {
            throw new NotImplementedException();
        }

        public ITypeProxyBuilder Event(Type delegateType, string name)
        {
            throw new NotImplementedException();
        }

        public ITypeProxyBuilder Public => throw new NotImplementedException();
        public ITypeProxyBuilder Internal => throw new NotImplementedException();
        public ITypeProxyBuilder Protected => throw new NotImplementedException();
        public ITypeProxyBuilder Private => throw new NotImplementedException();
        public ITypeProxyBuilder Static => throw new NotImplementedException();
        public ITypeProxyBuilder ReadOnly => throw new NotImplementedException();
        public ITypeProxyBuilder Abstract => throw new NotImplementedException();
        public ITypeProxyBuilder Sealed => throw new NotImplementedException();
        public ITypeProxyBuilder Override => throw new NotImplementedException();
        public ITypeProxyBuilder Virtual => throw new NotImplementedException();

        public TypeInfo ReleaseType()
        {
            throw new NotSupportedException(nameof(ReleaseType));
        }
    }
}