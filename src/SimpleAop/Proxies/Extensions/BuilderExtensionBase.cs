using System.Reflection.Emit;

namespace SimpleAop.Proxies.Extensions
{
    public abstract class BuilderExtensionBase
    {
        protected TypeBuilder TypeBuilder { get; }
        protected ModuleBuilder ModuleBuilder { get; }

        protected BuilderExtensionBase(ModuleBuilder moduleBuilder, TypeBuilder typeBuilder)
        {
            this.ModuleBuilder = moduleBuilder;
            this.TypeBuilder   = typeBuilder;
        }
    }
}