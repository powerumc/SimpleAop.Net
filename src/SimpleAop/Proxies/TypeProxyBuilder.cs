using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using SimpleAop.Proxies.Accessors;
using SimpleAop.Proxies.Criteria;
using SimpleAop.Proxies.Extensions;
using SimpleAop.Proxies.Operands;
using SimpleAop.Proxies.Statments;

namespace SimpleAop.Proxies
{
    public class TypeProxyBuilder : ITypeProxyBuilder
    {
        private TypeAccessor typeAccessor;
        private FieldAccessor fieldAccessor;
        private MethodAccessor methodAccessor;
        private AccessorInvocation accessorInvocation;


        public TypeAccessor TypeAccessor => this.typeAccessor;
        public FieldAccessor FieldAccessor => this.fieldAccessor;
        public MethodAccessor MethodAccessor => this.methodAccessor;


        public IModuleProxyBuilder ModuleProxyBuilder { get; private set; }
        public TypeAttributes TypeAttribute { get; private set; }
        public TypeBuilder TypeBuilder { get; private set; }


        public TypeProxyBuilder(IModuleProxyBuilder moduleProxyBuilder)
        {
            this.ModuleProxyBuilder = moduleProxyBuilder;

            // Init
            this.typeAccessor	= new TypeAccessor(this);
            this.methodAccessor = new MethodAccessor(this);
            this.fieldAccessor	= new FieldAccessor(this);

            this.accessorInvocation = new AccessorInvocation(this, typeAccessor, methodAccessor, fieldAccessor);
        }

        private void ResetAccessorAttributes()
        {
            this.typeAccessor.TypeAttributes	= 0;
            this.methodAccessor.MethodAttribute = 0;
            this.fieldAccessor.FieldAttribute	= 0;
        }

        public ITypeProxyBuilder Attribute(Type attributeType, params object[] param)
        {
            new TypeBuilderExtension(this.ModuleProxyBuilder.ModuleBuilder, this.TypeBuilder)
                .CreateAttribute(attributeType, param);

            return this;
        }
		
        public Operand Field(Type returnType, string name)
        {
            var operand = new FieldOperand(this, null, new CriteriaMetadataInfo(returnType, name, CriteriaMetadataToken.Field));
            operand.WriteEmit(null);

            return operand;
        }

        public IPropertyStatment Property(Type returnType, string name)
        {
            return Property(returnType, name, 0);
        }

        public IPropertyStatment Property(Type returnType, string name, CallingConventions callingConventions)
        {
            var property = new TypeBuilderExtension(this.ModuleProxyBuilder.ModuleBuilder, this.TypeBuilder)
                .CreateProperty(name, PropertyAttributes.HasDefault, returnType, null, callingConventions);

            return new PropertyStatment(this, property);
        }

        public IStatment Method(string name)
        {
            return this.Method(typeof(void), name, Type.EmptyTypes);
        }

        public IStatment Method(Type returnType, string name, Type[] argumentsTypes)
        {
            return this.Method(returnType, name, argumentsTypes, null);
        }

        public IStatment Method(Type returnType, string name, Type[] argumentsTypes, MethodInfo parentMethodInfo)
        {
            var method = new TypeBuilderExtension(null, this.TypeBuilder)
                .CreateMethod(this.methodAccessor.MethodAttribute, returnType, name, argumentsTypes, null, methodAccessor.IsOverride);


            return new MethodOperand(this, method.GetILGenerator(), method.GetBaseDefinition());
        }

        public IStatment Constructor()
        {
            return this.Constructor(Type.EmptyTypes);
        }

        public IStatment Constructor(params Type[] argumentsTypes)
        {
            var constructorBuilder = new TypeBuilderExtension(this.ModuleProxyBuilder.ModuleBuilder, this.TypeBuilder)
                .CreateConstructor(this.methodAccessor.MethodAttribute, CallingConventions.HasThis, argumentsTypes);

            ResetAccessorAttributes();

            return new CodeStatment(this, constructorBuilder.GetILGenerator());
        }

        public IStatment Constructor(IEnumerable<ParameterCriteriaMetadataInfo> parameterCriteriaMetadataInfos)
        {
            var constructorBuilder = new TypeBuilderExtension(this.ModuleProxyBuilder.ModuleBuilder, this.TypeBuilder)
                .CreateConstructor(this.methodAccessor.MethodAttribute, CallingConventions.HasThis, parameterCriteriaMetadataInfos);

            ResetAccessorAttributes();

            return new CodeStatment(this, constructorBuilder.GetILGenerator());
        }

        public ITypeProxyBuilder Class(string name)
        {
            this.TypeBuilder = new ModuleBuilderExtension(this.ModuleProxyBuilder.ModuleBuilder, null)
                .CreateType(name, this.typeAccessor.TypeAttributes);

            ResetAccessorAttributes();

            return this;
        }

        public ITypeProxyBuilder Class(string name, Type parent, Type[] interfaces)
        {
            this.TypeBuilder = new ModuleBuilderExtension(this.ModuleProxyBuilder.ModuleBuilder, null)
                .CreateType(name, this.typeAccessor.TypeAttributes, parent, interfaces ?? Type.EmptyTypes);

            ResetAccessorAttributes();

            return this;
        }

        public ITypeProxyBuilder Struct(string name)
        {
            this.typeAccessor.TypeAttributes = this.typeAccessor.TypeAttributes | AttributeConstants.TypeAttribute.Struct;

            return this.Class(name);
        }

        public ITypeProxyBuilder Interface(string name)
        {
            this.typeAccessor.TypeAttributes = AttributeConstants.TypeAttribute.Interface;

            return this.Class(name);
        }


        public ITypeProxyBuilder Delegate(Type returnType, string name, params Type[] argumentsTypes)
        {
            this.TypeBuilder = new ModuleBuilderExtension(this.ModuleProxyBuilder.ModuleBuilder, this.TypeBuilder)
                .CreateDelegate(returnType, name, argumentsTypes);

            return this;
        }

        public ITypeProxyBuilder Event(Type delegateType, string name)
        {
            throw new NotImplementedException();
        }

        public TypeInfo ReleaseType()
        {
            if (this.TypeBuilder == null)
            {
                throw new NullReferenceException(this.TypeBuilder.GetType().Name);
            }

            return this.TypeBuilder.CreateTypeInfo();
        } 

        public ITypeProxyBuilder Public		{ get { var temp = this.accessorInvocation.Public; return this; } }
        public ITypeProxyBuilder Internal		{ get { var temp = this.accessorInvocation.Internal; return this; } }
        public ITypeProxyBuilder Protected	{ get { var temp = this.accessorInvocation.Protected; return this; } }
        public ITypeProxyBuilder Private		{ get { var temp = this.accessorInvocation.Private; return this; } }
        public ITypeProxyBuilder Static		{ get { var temp = this.accessorInvocation.Static; return this; } }
        public ITypeProxyBuilder ReadOnly		{ get { var temp = this.accessorInvocation.ReadOnly; return this; } }
        public ITypeProxyBuilder Abstract		{ get { var temp = this.accessorInvocation.Abstract; return this; } }
        public ITypeProxyBuilder Sealed		{ get { var temp = this.accessorInvocation.Sealed; return this; } }
        public ITypeProxyBuilder Override		{ get { var temp = this.accessorInvocation.Override; return this; } }
        public ITypeProxyBuilder Virtual		{ get { var temp = this.accessorInvocation.Virtual; return this; } }
    }
}