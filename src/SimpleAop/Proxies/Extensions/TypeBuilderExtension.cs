using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using SimpleAop.Proxies.Criteria;

namespace SimpleAop.Proxies.Extensions
{
    internal class TypeBuilderExtension : BuilderExtensionBase
    {
        public TypeBuilderExtension(ModuleBuilder moduleBuilder, TypeBuilder typeBuilder)
            : base(moduleBuilder, typeBuilder)
        {
        }

        public ConstructorBuilder CreateConstructor(MethodAttributes methodAttributes, CallingConventions callingConventions, Type[] parameterTypes)
        {
            var list = parameterTypes.Select(parameter => new ParameterCriteriaMetadataInfo(parameter)).ToList();

            return CreateConstructor(methodAttributes, callingConventions, list);
        }

        public ConstructorBuilder CreateConstructor(MethodAttributes methodAttributes, CallingConventions callingConventions, IEnumerable<ParameterCriteriaMetadataInfo> parameterCriteriaMetadataInfos)
        {
            if (isStaticMethod(methodAttributes))
            {
                methodAttributes = MethodAttributes.Static | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName | MethodAttributes.Private | MethodAttributes.HideBySig;
                callingConventions = CallingConventions.Standard;
            }
            else
            {
                callingConventions = CallingConventions.HasThis;
            }


            var criteriaMetadataInfos = parameterCriteriaMetadataInfos.ToList();
            var constructorBuilder = this.TypeBuilder.DefineConstructor(methodAttributes, callingConventions, criteriaMetadataInfos.Select( o => o.Type).ToArray());
			
            var iSeqence = 0;
            foreach (var parameter in criteriaMetadataInfos)
            {
                iSeqence++;
                constructorBuilder.DefineParameter(iSeqence, parameter.ParameterAttribute, parameter.Name);
            }

            var il = constructorBuilder.GetILGenerator();

            if (isStaticMethod(methodAttributes))	// 정적 생성자는 Object 개체 파생이 아니므로 Object 생성을 하지 않음
            {
                il.Emit(OpCodes.Nop);
            }
            else
            {
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Call, this.TypeBuilder.BaseType.GetConstructors()[0]);
            }


            return constructorBuilder;
        }

        public MethodBuilder CreateMethod(MethodAttributes methodAttributes, Type returnType, string name, Type[] argumentTypes, MethodInfo parentMethodInfo, bool isOverride)
        {
            if (this.TypeBuilder == null)
                throw new NullReferenceException(this.TypeBuilder.GetType().Name);

            var methodBuilder = this.TypeBuilder.DefineMethod(name, methodAttributes, returnType, argumentTypes);

            if (isOverride == true && parentMethodInfo == null)
                throw new ArgumentNullException(nameof(parentMethodInfo));

            if (isOverride == true)
            {
                this.TypeBuilder.DefineMethodOverride(methodBuilder, parentMethodInfo);
            }

            return methodBuilder;
        }

        public PropertyBuilder CreateProperty(string name, PropertyAttributes attribute, Type returnType, Type[] parameterTypes)
        {
            return CreateProperty(name, attribute, returnType, parameterTypes, 0);
        }

        public PropertyBuilder CreateProperty(string name, PropertyAttributes attribute, Type returnType, Type[] parameterTypes, CallingConventions callingConventions)
        {
            if ( this.TypeBuilder == null )
                throw new NullReferenceException(this.TypeBuilder.GetType().Name);

            var builder = this.TypeBuilder.DefineProperty(name, attribute, callingConventions, returnType, null, null, parameterTypes, null, null);

            return builder;
        }

        public void CreateAttribute(Type type, params object[] param)
        {
            if ( this.TypeBuilder == null )
                throw new NullReferenceException(this.TypeBuilder.GetType().Name);

            var attribute = new CustomAttributeBuilder(type.GetConstructors()[0], param);
            this.TypeBuilder.SetCustomAttribute(attribute);
        }


        private static bool isStaticMethod(MethodAttributes methodAttributes)
        {
            return (methodAttributes & MethodAttributes.Static) == MethodAttributes.Static;
        }


    }
}