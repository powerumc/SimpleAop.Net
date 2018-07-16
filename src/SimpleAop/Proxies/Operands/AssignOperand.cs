using System.Reflection.Emit;
using SimpleAop.Proxies.Mappings;
using SimpleAop.Proxies.Statments;

namespace SimpleAop.Proxies.Operands
{
    internal class AssignOperand : Operand
    {
        private ITypeProxyBuilder _typeProxyBuilder;

        private Operand left;
        private Operand right;

        private OpCodeI4Mapping opcodeI4Mapping = new OpCodeI4Mapping();

        public AssignOperand(ITypeProxyBuilder typeProxyBuilder, ILGenerator ilGenerator, Operand left, Operand right)
            : base(typeProxyBuilder, ilGenerator)
        {
            this._typeProxyBuilder = typeProxyBuilder;

            this.left = left;
            this.right = right;
        }


        /// <summary>	
        /// 	<see cref="AssignOperand"/> 를 구현하는 코드의 Emit Byte 코드를 씁니다. 
        /// </summary>
        /// <param name="statment">	구현부 코드에 쓸 <see cref="IStatment"/> 인터페이스를 구현하는 객체입니다. </param>
        public override void WriteEmit(IStatment statment)
        {
            if (right is LocalOperand)
            {
                var leftLocal = (LocalOperand)left;
                var rightLocal = (LocalOperand)right;

                if (rightLocal.Value.LocalType.IsPrimitive == true)
                {
                    //OpCode opcode = opcodeI4Mapping.GetMappingValue(local.Value.3)
                    IL.Emit(OpCodes.Ldloc, rightLocal.Value);
                    IL.Emit(OpCodes.Stloc, leftLocal.Value);
                }
				
            }
            else if (right is FieldOperand)
            {
            }
            else if (right is MethodOperand)
            {
            }
        }
    }
}