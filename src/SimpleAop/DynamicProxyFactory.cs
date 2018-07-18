using System;

namespace SimpleAop
{
    public static class DynamicProxyFactory
    {
        public static Type Create(Type interfaceType, Type implementationType)
        {
            return new DynamicProxy(interfaceType, implementationType).CreateProxy();
        }

        public static Type Create<TInterface, TImplementation>()
        {
            return new DynamicProxy(typeof(TInterface), typeof(TImplementation)).CreateProxy();
        }
    }
}