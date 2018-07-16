using System;
using System.Reflection;
using System.Reflection.Emit;
using SimpleAop.Proxies.Criteria;
using SimpleAop.Proxies.Operands;

namespace SimpleAop.Proxies.Statments
{
    public class CodeStatment : IStatment
    {
        public ITypeProxyBuilder TypeProxyBuilder { get; private set; }
        public ILGenerator IL { get; internal set; }

        public CodeStatment(ITypeProxyBuilder typeProxyBuilder, ILGenerator ilGenreator)
        {
            this.TypeProxyBuilder = typeProxyBuilder;
            this.IL = ilGenreator;
        }

        public Operand Assign(Operand left, Operand right)
        {
            var operand = new AssignOperand(this.TypeProxyBuilder, this.IL, left, right);
            operand.WriteEmit(this);

            return operand;
        }

        public Operand AssignValue(Operand left, object right)
        {
            var operand = new AssignValueOperand(this.TypeProxyBuilder, this.IL, left, right);
            operand.WriteEmit(this);

            return operand;
        }

        public Operand AssignValueToProperty(Operand left)
        {
            throw new NotImplementedException();
        }

        public IStatment BeginBlock()
        {
            throw new NotImplementedException();
        }

        public IStatment EndBlock()
        {
            throw new NotImplementedException();
        }

        public IStatment Call()
        {
            throw new NotImplementedException();
        }

        public IStatment Call(Operand operand)
        {
            operand.WriteEmit(this);

            return this;
        }

        public IStatment Call(MethodInfo methodInfo, params Operand[] methodArguments)
        {
            var operand = new MethodOperand(this.TypeProxyBuilder, this.IL, methodInfo, methodArguments);
            operand.WriteEmit(this);

            return operand;
        }

        public Operand Local(Type type, string name)
        {
            ICriteriaMetadataInfo criteriaMetadataInfo = new CriteriaMetadataInfo(type, name, CriteriaMetadataToken.Local);
            var operand = new LocalOperand(this.TypeProxyBuilder, this.IL, criteriaMetadataInfo);
            operand.WriteEmit(this);

            return operand;
        }

        //public ICodeLambda New()
        //{
        //    return this.New(Operand.Empty);
        //}

        public IStatment New(ITypeProxyBuilder typeProxyBuilder)
        {
            if (typeProxyBuilder.TypeAccessor.IsStatic)
            {
                throw new Exception("Static object can not create");
            }
            else
            {
                // 파라메터를 스택에 로드
                //foreach (var parameter in constructorParameterOperand)
                //{
                //    parameter.ReadEmit(this);
                //}

                this.IL.Emit(OpCodes.Newobj, this.TypeProxyBuilder.TypeBuilder.DeclaringType);
            }

            return this;
        }

        public IStatment Return()
        {
            this.IL.Emit(OpCodes.Ret);

            return this;
        }

        public IStatment Return(Operand operand)
        {
            operand.ReadEmit(this);
            this.IL.Emit(OpCodes.Ret);

            return this;
        }

        public IStatment Break()
        {
            throw new NotImplementedException();
        }

        public IStatment Continue()
        {
            throw new NotImplementedException();
        }

        public ILGenerator Emit => this.IL;

        public IStatment EmitFromSource()
        {
            throw new NotImplementedException();
        }

        public IStatment Try()
        {
            throw new NotImplementedException();
        }

        public IStatment Catch()
        {
            throw new NotImplementedException();
        }

        public IStatment Catch(Type catchType)
        {
            throw new NotImplementedException();
        }

        public IStatment Finally()
        {
            throw new NotImplementedException();
        }

        public virtual void WriteEmit(IStatment statment)
        {
            throw new NotImplementedException();
        }
    }
}