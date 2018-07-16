using System;
using System.Reflection.Emit;

namespace SimpleAop.Proxies.Extensions
{
    public class ParameterBuilderExtension : BuilderExtensionBase
    {
        public ParameterBuilderExtension(ModuleBuilder moduleBuilder, TypeBuilder typeBuilder)
            : base(moduleBuilder, typeBuilder)
        {
        }

        public void CreateGenericTypeParameter()
        {
            throw new NotImplementedException();
        }
    }
}