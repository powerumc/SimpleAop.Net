using System;
using System.Reflection.Emit;

namespace SimpleAop.Proxies
{
    internal class ModuleProxyBuilder : IModuleProxyBuilder
    {
        public IAssemblyProxyBuilder AssemblyProxyBuilder { get; private set; }
        public ModuleBuilder ModuleBuilder { get; private set; }

        public ModuleProxyBuilder(IAssemblyProxyBuilder assemblyProxyBuilder)
        {
            this.AssemblyProxyBuilder = assemblyProxyBuilder;
        }

        public ITypeProxyBuilder Module()
        {
            return this.Module(Guid.NewGuid().ToString("N"));
        }

        public ITypeProxyBuilder Module(string moduleName)
        {
            this.ModuleBuilder = this.AssemblyProxyBuilder.AssemblyBuilder.DefineDynamicModule(moduleName);

            return new TypeProxyBuilder(this);
        }


        public IModuleProxyBuilder Attribute()
        {
            throw new NotImplementedException();
        }

        public IModuleProxyBuilder Attribute(params object[] @object)
        {
            throw new NotImplementedException();
        }
    }
}