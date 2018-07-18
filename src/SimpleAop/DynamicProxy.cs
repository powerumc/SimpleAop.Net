using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using SimpleAop.Extensions;
using SimpleAop.Proxies;
using SimpleAop.Proxies.Extensions;

namespace SimpleAop
{
    internal class DynamicProxy
    {
        private readonly Type _interfaceType;
        private readonly Type _implementationType;
        private readonly TypeBuilder _typeBuilder;
        private static readonly TypeProxyBuilder TypeProxyBuilder;

        static DynamicProxy()
        {
            TypeProxyBuilder = new AssemblyProxyBuilder().Assembly(Guid.NewGuid().ToString("N"))
                .Module(Guid.NewGuid().ToString("N"));
        }

        public DynamicProxy(Type interfaceType, Type implementationType)
        {
            _interfaceType = interfaceType;
            _implementationType = implementationType;
            _typeBuilder =
                TypeProxyBuilder.Class(Guid.NewGuid().ToString("N"), implementationType, new[] {interfaceType});
        }

        public Type CreateProxy()
        {
            foreach (var constructor in _implementationType.GetConstructors())
            {
                var constructorTypes = constructor.GetParameters().Select(o => o.ParameterType).ToArray();
                var c = _typeBuilder.DefineConstructor(
                    MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.RTSpecialName | MethodAttributes.SpecialName,
                    CallingConventions.Standard,
                    constructorTypes);
                
                var il = c.GetILGenerator();
                il.Emit(OpCodes.Ldarg_0);

                for (var i = 0; i < constructorTypes.Length; i++)
                {
                    il.Emit(OpCodes.Ldarg, i + 1);
                }

                il.Call(_implementationType.GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, constructorTypes, null));
                il.Emit(OpCodes.Nop);
                il.Emit(OpCodes.Ret);
            }
            
            foreach (var method in _interfaceType.GetMethods(BindingFlags.Public | BindingFlags.Instance))
            {
                var methodTypes = method.GetParameters().Select(o => o.ParameterType).ToArray();
                var m = _typeBuilder.DefineMethod($"{method.Name}",
                    MethodAttributes.Private | MethodAttributes.Final | MethodAttributes.HideBySig |
                    MethodAttributes.Virtual | MethodAttributes.NewSlot,
                    CallingConventions.HasThis,
                    method.ReturnType,
                    methodTypes);
                
                _typeBuilder.DefineMethodOverride(m, _interfaceType.GetMethod(method.Name, methodTypes));
                
                var il = m.GetILGenerator();
                var localReturnValue = il.DeclareReturnValue(method);
                
                var localCurrentMethod = il.DeclareLocal(typeof(MethodBase));
                var localParameters = il.DeclareLocal(typeof(object[]));
                
                // var currentMethod = MethodBase.GetCurrentMethod();
                il.Call(typeof(MethodBase).GetMethod(nameof(MethodBase.GetCurrentMethod)));
                il.Emit(OpCodes.Stloc, localCurrentMethod);
                
                // var baseMethod = method.BaseType.GetMethod(...);
                var localBaseMethod = il.DeclareLocal(typeof(MethodBase));
                il.Emit(OpCodes.Ldloc, localCurrentMethod);
                il.Call(typeof(OnMethodBoundAspectAttributeExtension).GetMethod(nameof(OnMethodBoundAspectAttributeExtension.GetCustomAttributeOnBaseMethod)));
                il.Emit(OpCodes.Stloc, localBaseMethod);
                
                
                // var parameters = new[] {a, b, c};
                il.Emit(OpCodes.Ldc_I4, methodTypes.Length);
                il.Emit(OpCodes.Newarr, typeof(object));
                if (methodTypes.Length > 0)
                {
                    for (var i = 0; i < methodTypes.Length; i++)
                    {
                        il.Emit(OpCodes.Dup);
                        il.Emit(OpCodes.Ldc_I4, i);
                        il.Emit(OpCodes.Ldarg, i + 1);
                        if (methodTypes[i].IsValueType)
                        {
                            il.Emit(OpCodes.Box, methodTypes[i].UnderlyingSystemType);
                        }

                        il.Emit(OpCodes.Stelem_Ref);
                    }
                }
                il.Emit(OpCodes.Stloc, localParameters);

                // var aspectInvocation = new AspectInvocation(method, this, parameters);
                var localAspectInvocation = il.DeclareLocal(typeof(AspectInvocation));
                il.Emit(OpCodes.Ldloc, localCurrentMethod);
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldloc, localParameters);

                il.New(typeof(AspectInvocation).GetConstructors()[0]);
                il.Emit(OpCodes.Stloc, localAspectInvocation);
                
                // var classAttributes = GetType().GetOnMethodBoundAspectAttributes();
                var localClassAttributes = il.DeclareLocal(typeof(OnMethodBoundAspectAttribute[]));
                il.Emit(OpCodes.Ldarg_0);
                il.Call(_implementationType.GetMethod(nameof(GetType)));
                il.Call(typeof(OnMethodBoundAspectAttributeExtension).GetMethod(nameof(OnMethodBoundAspectAttributeExtension.GetOnMethodBoundAspectAttributes), new[] {typeof(Type)}));
                il.Emit(OpCodes.Stloc, localClassAttributes);
                
                // var methodAttributes = method.GetOnMethodBoundAspectAttributes();
                var localMethodAttributes = il.DeclareLocal(typeof(OnMethodBoundAspectAttribute[]));
                il.Emit(OpCodes.Ldloc, localBaseMethod);
                il.Call(typeof(OnMethodBoundAspectAttributeExtension).GetMethod(nameof(OnMethodBoundAspectAttributeExtension.GetOnMethodBoundAspectAttributes), new[] {typeof(MethodBase)}));
                il.Emit(OpCodes.Stloc_S, localMethodAttributes);
                
                
                // classAttributes.ForEachOnBefore(invocation);
                il.Emit(OpCodes.Ldloc, localClassAttributes);
                il.Emit(OpCodes.Ldloc, localAspectInvocation);
                il.Call(typeof(OnMethodBoundAspectAttributeExtension).GetMethod(nameof(OnMethodBoundAspectAttributeExtension.ForEachOnBefore)));
                il.Emit(OpCodes.Nop);
                
                // methodAttributes.ForEachOnBefore(invocation);
                il.Emit(OpCodes.Ldloc, localMethodAttributes);
                il.Emit(OpCodes.Ldloc, localAspectInvocation);
                il.Call(typeof(OnMethodBoundAspectAttributeExtension).GetMethod(nameof(OnMethodBoundAspectAttributeExtension.ForEachOnBefore)));
                il.Emit(OpCodes.Nop);
                
                il.LoadParameters(method);
                il.Call(_implementationType.GetMethod(method.Name, methodTypes));
                
                // methodAttributes.ForEachOnAfter(invocation);
                il.Emit(OpCodes.Ldloc, localMethodAttributes);
                il.Emit(OpCodes.Ldloc, localAspectInvocation);
                il.Call(typeof(OnMethodBoundAspectAttributeExtension).GetMethod(nameof(OnMethodBoundAspectAttributeExtension.ForEachOnAfter)));
                il.Emit(OpCodes.Nop);
                
                // classAttributes.ForEachOnAfter(invocation);
                il.Emit(OpCodes.Ldloc, localClassAttributes);
                il.Emit(OpCodes.Ldloc, localAspectInvocation);
                il.Call(typeof(OnMethodBoundAspectAttributeExtension).GetMethod(nameof(OnMethodBoundAspectAttributeExtension.ForEachOnAfter)));
                il.Emit(OpCodes.Nop);
                
                il.Return(method, localReturnValue);
            }

            return _typeBuilder.CreateTypeInfo();
        }
    }
}