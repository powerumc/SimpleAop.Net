using System;
using System.Reflection.Emit;
using SimpleAop.Proxies.Mappings;
using SimpleAop.Proxies.Statments;

namespace SimpleAop.Proxies.Operands
{
    public class AssignValueOperand : Operand
    {
        private ITypeProxyBuilder _typeProxyBuilder;

        private Operand left;
        private object right;

        private static OpCodeI4Mapping mapping = new OpCodeI4Mapping();

        public AssignValueOperand(ITypeProxyBuilder typeProxyBuilder, ILGenerator ilGenerator, Operand left, object right)
            : base(typeProxyBuilder, ilGenerator)
        {
            this._typeProxyBuilder = typeProxyBuilder;

            this.left  = left;
            this.right = right;
        }

        public override void WriteEmit(IStatment statment)
        {
            if (left is LocalOperand)
            {
                this.IL.Emit(OpCodes.Nop);

                var leftLocal = (LocalOperand)left;
                var rightTypeCode = Type.GetTypeCode(right.GetType());

                if (rightTypeCode == TypeCode.String)
                {
                    this.IL.Emit(OpCodes.Ldstr, (string)right);
                }
                else 
                {
                    var opcode = mapping.GetMappingValue((int)right);

                    if (opcode.OperandType == OperandType.InlineNone) this.IL.Emit(opcode);
                    else this.IL.Emit(opcode, (int)right);
                }

                this.IL.Emit(OpCodes.Stloc, leftLocal.Value);
                //this.IL.Emit(OpCodes.Nop);
            }
            else if (left is FieldOperand)
            {
                this.IL.Emit(OpCodes.Nop);

                var leftLocal = (FieldOperand)left;
                var rightTypeCode = Type.GetTypeCode(right.GetType());

                if (rightTypeCode == TypeCode.String)
                {
                    this.IL.Emit(OpCodes.Ldarg_0);
                    this.IL.Emit(OpCodes.Ldstr, (string)right);
                    this.IL.Emit(OpCodes.Stfld, leftLocal.Value);
                }
                else
                {
                    throw new NotSupportedException("FieldOperand/Int");
                    //var opcode = mapping.GetMappingValue((int)right);

                    //if (opcode.OperandType == OperandType.InlineNone) this.IL.Emit(opcode);
                    //else this.IL.Emit(opcode, (int)right);
                }
            }
            else if (left is MethodOperand)
            {
                throw new NotSupportedException("MethodOperand");
            }
        }
    }
}