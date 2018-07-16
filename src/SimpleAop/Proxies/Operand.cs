using System;
using System.Reflection.Emit;
using SimpleAop.Proxies.Statments;

namespace SimpleAop.Proxies
{
    public abstract class Operand : 
        CodeStatment,
        IEmitReadable,
        IEmitWriteable
    {

        public static Operand[] Empty = new Operand[] { };

        public Operand(ITypeProxyBuilder typeProxyBuilder, ILGenerator ilGenreator)
            : base(typeProxyBuilder, ilGenreator)
        {
        }

        public virtual void ReadEmit(IStatment statment)
        {
            throw new NotSupportedException();
        }

        public virtual void WriteEmit(IStatment statment, Operand operand)
        {
            throw new NotSupportedException();
        }
    }
}