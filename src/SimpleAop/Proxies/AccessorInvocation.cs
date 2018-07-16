using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleAop.Proxies
{
    public class AccessorInvocation : IAccessorLambda
    {
        private IEnumerable<IAccessorLambda> accessors;
        private ITypeProxyBuilder _typeProxyBuilder;

        private Func<IEnumerable<IAccessorLambda>, IAccessorLambda> func = (accessor) =>
        {
            IAccessorLambda typeLambda = null;
            accessor.All(o =>
            {
                typeLambda = o.Public;
                return true;
            });

            return typeLambda;
        };


        public AccessorInvocation(ITypeProxyBuilder typeProxyBuilder, params IAccessorLambda[] accessors)
            : this(typeProxyBuilder, accessors.AsEnumerable())
        {
        }

        public AccessorInvocation(ITypeProxyBuilder typeProxyBuilder, IEnumerable<IAccessorLambda> accessors)
        {
            this._typeProxyBuilder = typeProxyBuilder;
            this.accessors = accessors;
        }

        private IAccessorLambda InvokeAccessor(
            System.Linq.Expressions.Expression<Func<IAccessorLambda, IAccessorLambda>> invokeExpression)
        {
            accessors.All(o =>
            {
                invokeExpression.Compile()(o);
                return true;
            });

            //return this.typeLambda;
            return this;
        }

        public IAccessorLambda Public
        {
            get { return this.InvokeAccessor(o => o.Public); }
        }

        public IAccessorLambda Internal
        {
            get { return this.InvokeAccessor(o => o.Internal); }
        }

        public IAccessorLambda Protected
        {
            get { return this.InvokeAccessor(o => o.Protected); }
        }

        public IAccessorLambda Private
        {
            get { return this.InvokeAccessor(o => o.Private); }
        }

        public IAccessorLambda Static
        {
            get { return this.InvokeAccessor(o => o.Static); }
        }

        public IAccessorLambda ReadOnly
        {
            get { return this.InvokeAccessor(o => o.ReadOnly); }
        }

        public IAccessorLambda Abstract
        {
            get { return this.InvokeAccessor(o => o.Abstract); }
        }

        public IAccessorLambda Sealed
        {
            get { return this.InvokeAccessor(o => o.Sealed); }
        }

        public IAccessorLambda Override
        {
            get { return this.InvokeAccessor(o => o.Override); }
        }

        public IAccessorLambda Virtual
        {
            get { return this.InvokeAccessor(o => o.Virtual); }
        }
    }
}