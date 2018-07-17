using System;
using System.Reflection.Emit;

namespace SimpleAop.Proxies
{
    public class ModuleProxyBuilder
    {
        private readonly AssemblyBuilder _assemblyBuilder;

        public ModuleProxyBuilder(AssemblyBuilder assemblyBuilder)
        {
            _assemblyBuilder = assemblyBuilder;
        }

        public TypeProxyBuilder Module(string moduleName = null)
        {
            var moduleBuilder = _assemblyBuilder.DefineDynamicModule(moduleName ?? Guid.NewGuid().ToString("N"));
            
            return new TypeProxyBuilder(moduleBuilder);
        }
    }
}