using System.Reflection;

namespace SimpleAop.Proxies.Accessors
{
    public class TypeAccessor : 
        IAccessorLambda,
        IAccessorConfirmLambda
    {
        private ITypeProxyBuilder _typeProxyBuilder;

        public TypeAttributes TypeAttributes { get; internal set; }

        public TypeAccessor(ITypeProxyBuilder typeProxyBuilder)
        {
            this._typeProxyBuilder = typeProxyBuilder;
        }

        public IAccessorLambda Public
        {
            get
            {
                this.TypeAttributes |= AttributeConstants.TypeAttribute.Public;
                return this;
            }
        }

        public IAccessorLambda Internal
        {
            get
            {
                this.TypeAttributes |= AttributeConstants.TypeAttribute.Internal;
                return this;
            }
        }

        public IAccessorLambda Static
        {
            get
            {
                this.TypeAttributes |= AttributeConstants.TypeAttribute.Static;
                return this;
            }
        }

        public IAccessorLambda Abstract
        {
            get
            {
                this.TypeAttributes |= AttributeConstants.TypeAttribute.Abstract;
                return this;
            }
        }

        public IAccessorLambda Sealed
        {
            get
            {
                this.TypeAttributes |= AttributeConstants.TypeAttribute.Selaed;
                return this;
            }
        }

        public IAccessorLambda Protected
        {
            get
            {
                this.TypeAttributes |= 0;
                return this;
            }
        }

        public IAccessorLambda Private
        {
            get
            {
                this.TypeAttributes |= 0;
                return this;
            }
        }

        public IAccessorLambda Override
        {
            get
            {
                this.TypeAttributes |= 0;
                return this;
            }
        }

        public IAccessorLambda Virtual
        {
            get
            {
                this.TypeAttributes |= 0;
                return this;
            }
        }

        public IAccessorLambda ReadOnly
        {
            get
            {
                this.TypeAttributes |= 0;
                return this;
            }
        }

        public bool IsPublic => (this.TypeAttributes & AttributeConstants.TypeAttribute.Public) ==
                                AttributeConstants.TypeAttribute.Public;

        public bool IsInternal => (this.TypeAttributes & AttributeConstants.TypeAttribute.Internal) ==
                                  AttributeConstants.TypeAttribute.Internal;

        public bool IsStatic => (this.TypeAttributes & AttributeConstants.TypeAttribute.Static) ==
                                AttributeConstants.TypeAttribute.Static;

        public bool IsAbstract => (this.TypeAttributes & AttributeConstants.TypeAttribute.Abstract) ==
                                  AttributeConstants.TypeAttribute.Abstract;

        public bool IsSealed => (this.TypeAttributes & AttributeConstants.TypeAttribute.Selaed) ==
                                AttributeConstants.TypeAttribute.Selaed;

        public bool IsReadOnly => true;

        public bool IsProtected => true;

        public bool IsPrivate => true;

        public bool IsOverride => true;

        public bool IsVirtual => true;
    }
}