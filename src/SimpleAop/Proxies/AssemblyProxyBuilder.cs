using System;
using System.Reflection;
using System.Reflection.Emit;

namespace SimpleAop.Proxies
{
    public class AssemblyProxyBuilder
    {
        public ModuleProxyBuilder Assembly(string assemblyName = null)
        {
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
                new AssemblyName(assemblyName ?? Guid.NewGuid().ToString("N")),
                AssemblyBuilderAccess.Run);

            return new ModuleProxyBuilder(assemblyBuilder);
        }
    }
}