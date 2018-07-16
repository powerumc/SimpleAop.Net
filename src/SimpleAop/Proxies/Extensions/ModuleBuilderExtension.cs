using System;
using System.Reflection;
using System.Reflection.Emit;

namespace SimpleAop.Proxies.Extensions
{
    internal class ModuleBuilderExtension : BuilderExtensionBase
    {
        public ModuleBuilderExtension(ModuleBuilder moduleBuilder, TypeBuilder typeBuilder)
            : base(moduleBuilder, typeBuilder)
        {
        }


        public TypeBuilder CreateType(string name, TypeAttributes typeAttributes)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            return this.ModuleBuilder.DefineType(name, typeAttributes);
        }

        public TypeBuilder CreateType(string name, TypeAttributes typeAttributes, Type parentType, Type[] interfaces)
        {
            if ( name == null )
                throw new ArgumentNullException(nameof(name));

            return this.ModuleBuilder.DefineType(name, typeAttributes, parentType, interfaces);
        }


        public EnumBuilder CreateEnum(string name, TypeAttributes typeAttributes, Type underlyingType)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            return this.ModuleBuilder.DefineEnum(name, typeAttributes, underlyingType);
        }


        public TypeBuilder CreateDelegate(Type returnType, string name, params Type[] invokeMethodParameters)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            var delegateTypeBuilder = this.CreateType(name, AttributeConstants.TypeAttribute.Delegate);
            delegateTypeBuilder.SetParent(typeof(MulticastDelegate));

            var typeBuilderExtension = new TypeBuilderExtension(this.ModuleBuilder, delegateTypeBuilder);

            var constructorBuilder = typeBuilderExtension.CreateConstructor(
                AttributeConstants.MethodAttribute.Public | AttributeConstants.MethodAttribute.Constructor,
                CallingConventions.Standard,
                new Type[] {typeof(string), typeof(IntPtr)});
            constructorBuilder.DefineParameter(0, ParameterAttributes.None, "object");
            constructorBuilder.DefineParameter(1, ParameterAttributes.None, "method");

            constructorBuilder.SetImplementationFlags(MethodImplAttributes.Runtime | MethodImplAttributes.Managed);

            var delegateParameters = new Type[invokeMethodParameters.Length];

            for (var i = 0; i < invokeMethodParameters.Length; i++)
            {
                delegateParameters[i] = invokeMethodParameters[i];
            }

            var invokeMethodBuilder = typeBuilderExtension.CreateMethod(
                AttributeConstants.MethodAttribute.Public | AttributeConstants.MethodAttribute.Virtual |
                MethodAttributes.HideBySig | MethodAttributes.NewSlot,
                returnType,
                "Invoke",
                delegateParameters,
                null,
                false);
            invokeMethodBuilder.SetImplementationFlags(MethodImplAttributes.Runtime | MethodImplAttributes.Managed);

            return delegateTypeBuilder;
        }
    }
}