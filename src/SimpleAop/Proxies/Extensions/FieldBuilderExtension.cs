using System;
using System.Reflection;
using System.Reflection.Emit;

namespace SimpleAop.Proxies.Extensions
{
    public class FieldBuilderExtension : BuilderExtensionBase
    {
        public FieldBuilderExtension(ModuleBuilder moduleBuilder, TypeBuilder typeBuilder)
            : base(moduleBuilder, typeBuilder)
        {
        }

        public FieldBuilder CreateField(string fieldName, Type type, FieldAttributes fieldAttributes)
        {
            var fieldBuilder = this.TypeBuilder.DefineField(fieldName, type, fieldAttributes);

            return fieldBuilder;
        }
    }
}