using System.Reflection.Emit;

namespace SimpleAop.Proxies
{
    public interface IAssemblyProxyBuilder :
        IMetadata<IAssemblyProxyBuilder>,
        ITypeProxyBuilder
    {
        AssemblyBuilder AssemblyBuilder { get; }
        string AssemblyLambdaQualifiedName { get; }

        IModuleProxyBuilder Assembly();
        IModuleProxyBuilder Assembly(string assemblyName);
        ITypeProxyBuilder SetParent(string name);
        ITypeProxyBuilder AddInterface(string name);
    }
}