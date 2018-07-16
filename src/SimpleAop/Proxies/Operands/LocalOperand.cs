using System.Reflection.Emit;
using SimpleAop.Proxies.Criteria;
using SimpleAop.Proxies.Mappings;
using SimpleAop.Proxies.Statments;

namespace SimpleAop.Proxies.Operands
{
    internal class LocalOperand :
        Operand,
        IValuable<LocalBuilder>
    {
        private ITypeProxyBuilder _typeProxyBuilder;
        private ICriteriaMetadataInfo criteriaMetadataInfo;
        private LocalBuilder localBuilder;
        private static OpCodeI4Mapping mapping = new OpCodeI4Mapping();

        public LocalOperand(ITypeProxyBuilder typeProxyBuilder, ILGenerator ilGenerator, ICriteriaMetadataInfo criteriaMetadataInfo)
            : base(typeProxyBuilder, ilGenerator)
        {
            this._typeProxyBuilder           = typeProxyBuilder;
            this.criteriaMetadataInfo = criteriaMetadataInfo;
        }

        public override void WriteEmit(IStatment statment)
        {
            this.localBuilder = statment.IL.DeclareLocal(this.criteriaMetadataInfo.Type);
            //localBuilder.SetLocalSymInfo(this.criteriaMetadataInfo.Name);
        }

        public override void ReadEmit(IStatment statment)
        {
            this.IL.Emit(OpCodes.Ldloc, this.Value);
        }

        public LocalBuilder Value
        {
            get { return this.localBuilder; }
        }
    }
}