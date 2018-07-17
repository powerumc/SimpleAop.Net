using System;
using System.Reflection;
using System.Reflection.Emit;
using SimpleAop.Proxies.Statments;

namespace SimpleAop.Proxies.Operands
{
    public class MethodOperand : 
        Operand,
        IValuable<MethodInfo>
    {
        private ITypeProxyBuilder _typeProxyBuilder;
        private MethodInfo methodInfo;
        private Operand[] parameterOperands;

        public MethodInfo Value => this.methodInfo;

        public MethodOperand(ITypeProxyBuilder typeProxyBuilder, ILGenerator ilGenerator, MethodInfo methodInfo)
            : base(typeProxyBuilder, ilGenerator)
        {
            this._typeProxyBuilder = typeProxyBuilder;
            this.methodInfo = methodInfo;
        }

        public MethodOperand(ITypeProxyBuilder typeProxyBuilder, ILGenerator ilGenerator, MethodInfo methodInfo, params Operand[] parameterOperands)
            : this(typeProxyBuilder, ilGenerator, methodInfo)
        {
            this.parameterOperands = parameterOperands;
        }

        public override void WriteEmit(IStatment statment)
        {
            if (_typeProxyBuilder.MethodAccessor.IsStatic || this.methodInfo.IsStatic)
            {
                this.WriteEmitOfStaticMethod(statment.IL);
            }
            else
            {
                this.WriteEmitOfInstanceMethod(statment.IL);
            }
        }

        private void WriteEmitOfInstanceMethod(ILGenerator il)
        {
            throw new NotImplementedException();
        }

        private void WriteEmitOfStaticMethod(ILGenerator il)
        {
            if (this.parameterOperands != null)
            {
                var methodInfoParameters = this.methodInfo.GetParameters();

                if (methodInfoParameters.Length != parameterOperands.Length)
                    throw new Exception("Parameters length mismatch");

                // 호출 메서드의 파라메터 타입이 Object 인 경우 Boxing
                for (var i = 0; i < this.parameterOperands.Length; i++)
                {
                    this.parameterOperands[i].ReadEmit(this);

                    var operandDeclareType = this.GetOperandDeclareType((IValuable)this.parameterOperands[i]);

                    if (methodInfoParameters[i].ParameterType == typeof(Object) &&
                        methodInfoParameters[i].ParameterType != operandDeclareType)
                    {
                        this.IL.Emit(OpCodes.Box, operandDeclareType);
                    }
                }
            }

            il.Emit(OpCodes.Call, methodInfo);
        }

        private Type GetOperandDeclareType(IValuable operand)
        {
            if (operand is LocalOperand) return ((LocalOperand)operand).Value.LocalType;
            if (operand is FieldOperand) return ((FieldOperand)operand).Value.FieldType;
			
            // TODO PropertyOperand 추가 해야함
            throw new NotSupportedException(operand.ToString());
        }
        
    }
}