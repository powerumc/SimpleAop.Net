using System.Reflection;

namespace SimpleAop.Proxies.Accessors
{
    public class MethodAccessor :
        IAccessorLambda,
        IAccessorConfirmLambda
    {
        public MethodAttributes MethodAttribute { get; internal set; }

        private ITypeProxyBuilder _typeProxyBuilder;

        public MethodAccessor(ITypeProxyBuilder typeProxyBuilder)
        {
            this._typeProxyBuilder = typeProxyBuilder;
        }

        public bool IsOverride { get; private set; }


        public IAccessorLambda Public
        {
            get
            {
                this.MethodAttribute |= AttributeConstants.MethodAttribute.Public;
                return this;
            }
        }

        public IAccessorLambda Internal
        {
            get
            {
                this.MethodAttribute |= AttributeConstants.MethodAttribute.Internal;
                return this;
            }
        }

        public IAccessorLambda Protected
        {
            get
            {
                this.MethodAttribute |= AttributeConstants.MethodAttribute.Protected;
                return this;
            }
        }

        public IAccessorLambda Private
        {
            get
            {
                this.MethodAttribute |= AttributeConstants.MethodAttribute.Private;
                return this;
            }
        }

        public IAccessorLambda Static
        {
            get
            {
                this.MethodAttribute |= AttributeConstants.MethodAttribute.Static;
                return this;
            }
        }

        public IAccessorLambda Abstract
        {
            get
            {
                this.MethodAttribute |= AttributeConstants.MethodAttribute.Abstract;
                return this;
            }
        }

        public IAccessorLambda Virtual
        {
            get
            {
                this.MethodAttribute |= AttributeConstants.MethodAttribute.Virtual;
                return this;
            }
        }

        public IAccessorLambda Final
        {
            get
            {
                this.MethodAttribute |= AttributeConstants.MethodAttribute.Final;
                return this;
            }
        }

        public IAccessorLambda Sealed
        {
            get
            {
                this.MethodAttribute |= 0;
                return this;
            }
        }

        public IAccessorLambda ReadOnly
        {
            get
            {
                this.MethodAttribute |= 0;
                return this;
            }
        }

        public IAccessorLambda Override
        {
            get
            {
                this.IsOverride = true;
                return this;
            }
        }

        public bool IsPublic => (this.MethodAttribute & AttributeConstants.MethodAttribute.Public) == AttributeConstants.MethodAttribute.Public;
        public bool IsInternal => (this.MethodAttribute & AttributeConstants.MethodAttribute.Internal) == AttributeConstants.MethodAttribute.Internal;
        public bool IsStatic => (this.MethodAttribute & AttributeConstants.MethodAttribute.Static) == AttributeConstants.MethodAttribute.Static;
        public bool IsAbstract => (this.MethodAttribute & AttributeConstants.MethodAttribute.Abstract) == AttributeConstants.MethodAttribute.Abstract;
        public bool IsProtected => (this.MethodAttribute & AttributeConstants.MethodAttribute.Protected) == AttributeConstants.MethodAttribute.Protected;
        public bool IsPrivate => (this.MethodAttribute & AttributeConstants.MethodAttribute.Private) == AttributeConstants.MethodAttribute.Private;
        public bool IsVirtual => (this.MethodAttribute & AttributeConstants.MethodAttribute.Virtual) == AttributeConstants.MethodAttribute.Virtual;

        public bool IsSealed => true;
        public bool IsReadOnly => true;
    }
}