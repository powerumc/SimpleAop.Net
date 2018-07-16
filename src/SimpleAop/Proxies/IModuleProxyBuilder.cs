using System.Reflection.Emit;

namespace SimpleAop.Proxies
{
    public interface IModuleProxyBuilder : IMetadata<IModuleProxyBuilder>
    {
        IAssemblyProxyBuilder AssemblyProxyBuilder { get; }
        ModuleBuilder ModuleBuilder { get; }
        ITypeProxyBuilder Module();
        ITypeProxyBuilder Module(string moduleName);
    }
}