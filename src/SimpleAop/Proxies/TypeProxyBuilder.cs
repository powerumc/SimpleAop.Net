using System;
using System.Reflection;
using System.Reflection.Emit;

namespace SimpleAop.Proxies
{
    public class TypeProxyBuilder
    {
        private readonly ModuleBuilder _moduleBuilder;

        public TypeProxyBuilder(ModuleBuilder moduleBuilder)
        {
            _moduleBuilder = moduleBuilder;
        }

        public TypeBuilder Class(string name, Type parent, Type[] interfacesTypes)
        {
            return _moduleBuilder.DefineType(name, TypeAttributes.Class | TypeAttributes.Public, parent,
                interfacesTypes);
        }
    }
}