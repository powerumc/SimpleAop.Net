using System;
using System.Reflection;
using System.Reflection.Emit;
using SimpleAop.Proxies.Extensions;

namespace SimpleAop.Proxies.Statments
{
    public class PropertyStatment : IPropertyStatment
    {

        public ITypeProxyBuilder TypeProxyBuilder { get; private set; }
        private PropertyBuilder propertyBuilder;

        public PropertyStatment(ITypeProxyBuilder typeProxyBuilder, PropertyBuilder propertyBuilder)
        {
            this.TypeProxyBuilder = typeProxyBuilder;
            this.propertyBuilder = propertyBuilder;
        }


        public IStatment Get()
        {
            var type = new TypeBuilderExtension(null, this.TypeProxyBuilder.TypeBuilder);
            var methodAttr = this.TypeProxyBuilder.MethodAccessor.MethodAttribute | MethodAttributes.NewSlot | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.SpecialName | MethodAttributes.HideBySig;
            var method = type.CreateMethod(methodAttr, propertyBuilder.PropertyType, string.Concat("get_", propertyBuilder.Name), Type.EmptyTypes, null, false);
			
            propertyBuilder.SetGetMethod(method);

            return new CodeStatment(this.TypeProxyBuilder, method.GetILGenerator());
        }

        public IStatment Set()
        {
            var type = new TypeBuilderExtension(null, this.TypeProxyBuilder.TypeBuilder);

            var methodAttr = this.TypeProxyBuilder.MethodAccessor.MethodAttribute | MethodAttributes.NewSlot | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.SpecialName | MethodAttributes.HideBySig;
            var method = type.CreateMethod(methodAttr, typeof(void), string.Concat("set_", propertyBuilder.Name), new Type[] { propertyBuilder.PropertyType }, null, false);
            method.DefineParameter(1, ParameterAttributes.HasDefault, "value");

            propertyBuilder.SetSetMethod(method);

            return new CodeStatment(this.TypeProxyBuilder, method.GetILGenerator());
        }

        public ITypeProxyBuilder GetSet()
        {
            this.TypeProxyBuilder.FieldAccessor.FieldAttribute = FieldAttributes.Private;
            var field = this.TypeProxyBuilder.Field(this.propertyBuilder.PropertyType, string.Concat("__", propertyBuilder.Name));
			
            var get = this.Get();
            {
                get.Return(field);
            }

            var set = this.Set();
            {
                set.IL.Emit(OpCodes.Ldarg_0);
                set.IL.Emit(OpCodes.Ldarg_1);
                set.IL.Emit(OpCodes.Stfld, ( (IValuable<FieldBuilder>)field ).Value);
				
                set.Return();
            }

            return this.TypeProxyBuilder;
        }

        public IAccessorLambda Public => throw new NotImplementedException();
        public IAccessorLambda Internal => throw new NotImplementedException();
        public IAccessorLambda Protected => throw new NotImplementedException();
        public IAccessorLambda Private => throw new NotImplementedException();
        public IAccessorLambda Static => throw new NotImplementedException();
        public IAccessorLambda ReadOnly => throw new NotImplementedException();
        public IAccessorLambda Abstract => throw new NotImplementedException();
        public IAccessorLambda Sealed => throw new NotImplementedException();
        public IAccessorLambda Override => throw new NotImplementedException();
        public IAccessorLambda Virtual => throw new NotImplementedException();
    }
}