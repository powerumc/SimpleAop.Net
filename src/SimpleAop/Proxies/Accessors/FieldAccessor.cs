using System.Reflection;

namespace SimpleAop.Proxies.Accessors
{
    public class FieldAccessor :
        IAccessorLambda,
        IAccessorConfirmLambda
    {
        public FieldAttributes FieldAttribute;

        private ITypeProxyBuilder _typeProxyBuilder;

        public FieldAccessor(ITypeProxyBuilder typeProxyBuilder)
        {
            this._typeProxyBuilder = typeProxyBuilder;
        }

        public IAccessorLambda Public
        {
            get
            {
                this.FieldAttribute |= AttributeConstants.FieldAttribute.Public;
                return this;
            }
        }

        public IAccessorLambda Internal
        {
            get
            {
                this.FieldAttribute |= AttributeConstants.FieldAttribute.Internal;
                return this;
            }
        }

        public IAccessorLambda Protected
        {
            get
            {
                this.FieldAttribute |= AttributeConstants.FieldAttribute.Protected;
                return this;
            }
        }

        public IAccessorLambda Private
        {
            get
            {
                this.FieldAttribute |= AttributeConstants.FieldAttribute.Private;
                return this;
            }
        }

        public IAccessorLambda Static
        {
            get
            {
                this.FieldAttribute |= AttributeConstants.FieldAttribute.Static;
                return this;
            }
        }

        public IAccessorLambda ReadOnly
        {
            get
            {
                this.FieldAttribute |= AttributeConstants.FieldAttribute.ReadOnly;
                return this;
            }
        }

        public IAccessorLambda Abstract
        {
            get
            {
                this.FieldAttribute |= 0;
                return this;
            }
        }

        public IAccessorLambda Sealed
        {
            get
            {
                this.FieldAttribute |= 0;
                return this;
            }
        }

        public IAccessorLambda Override
        {
            get
            {
                this.FieldAttribute |= 0;
                return this;
            }
        }

        public IAccessorLambda Virtual
        {
            get
            {
                this.FieldAttribute |= 0;
                return this;
            }
        }

        public IAccessorLambda Final
        {
            get
            {
                this.FieldAttribute |= 0;
                return this;
            }
        }

        public bool IsPublic => (this.FieldAttribute & AttributeConstants.FieldAttribute.Public) == AttributeConstants.FieldAttribute.Public;
        public bool IsInternal => (this.FieldAttribute & AttributeConstants.FieldAttribute.Internal) == AttributeConstants.FieldAttribute.Internal;
        public bool IsStatic => (this.FieldAttribute & AttributeConstants.FieldAttribute.Static) == AttributeConstants.FieldAttribute.Static;
        public bool IsProtected => (this.FieldAttribute & AttributeConstants.FieldAttribute.Protected) == AttributeConstants.FieldAttribute.Protected;
        public bool IsPrivate => (this.FieldAttribute & AttributeConstants.FieldAttribute.Private) == AttributeConstants.FieldAttribute.Private;
        public bool IsReadOnly => (this.FieldAttribute & AttributeConstants.FieldAttribute.ReadOnly) == AttributeConstants.FieldAttribute.ReadOnly;

        public bool IsSealed => true;
        public bool IsOverride => true;
        public bool IsAbstract => true;
        public bool IsVirtual => true;
    }
}