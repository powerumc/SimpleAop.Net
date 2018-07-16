using System.Reflection.Emit;
using SimpleAop.Proxies.Criteria;
using SimpleAop.Proxies.Extensions;
using SimpleAop.Proxies.Statments;

namespace SimpleAop.Proxies.Operands
{
    internal class FieldOperand : 
        Operand,
        IValuable<FieldBuilder>
    {
        private ITypeProxyBuilder _typeProxyBuilder;
        private ICriteriaMetadataInfo criteriaMetadataInfo;
        private FieldBuilder fieldBuilder;

        public FieldOperand(ITypeProxyBuilder typeProxyBuilder, ILGenerator ilGenerator, ICriteriaMetadataInfo criteriaMetadataInfo)
            : base(typeProxyBuilder, ilGenerator)
        {
            this._typeProxyBuilder           = typeProxyBuilder;
            this.criteriaMetadataInfo = criteriaMetadataInfo;
        }

        public override void WriteEmit(IStatment statment)
        {
            this.fieldBuilder = fieldBuilder = new FieldBuilderExtension(null, this._typeProxyBuilder.TypeBuilder)
                .CreateField(criteriaMetadataInfo.Name, criteriaMetadataInfo.Type, this._typeProxyBuilder.FieldAccessor.FieldAttribute);
        }

        public override void ReadEmit(IStatment statment)
        {
            // Field 선언시 ILGenerator 상태가 없기 때문에, FieldOperand ReadEmit 호출 시에 IL 넘겨줌
            if (this.IL == null)
                this.IL = statment.IL;

            this.IL.Emit(OpCodes.Ldarg_0);
            this.IL.Emit(OpCodes.Ldfld, this.Value);
        }

        public FieldBuilder Value
        {
            get { return this.fieldBuilder; }
        }
    }
}