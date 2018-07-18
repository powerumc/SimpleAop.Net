using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;
using SimpleAop.Extensions;
using SimpleAop.Proxies;
using SimpleAop.Proxies.Extensions;

namespace SimpleAop.Sample.DynamicProxy
{
    class Program
    {
        static void Main(string[] args)
        {
            var assembly = new AssemblyProxyBuilder().Assembly("A");
            var module = assembly.Module("B");
            var @class = module.Class("C", typeof(TestClass), new[] {typeof(ITestClass)});

            Print(@class, typeof(ITestClass), typeof(TestClass));
        }

        private static void Print(TypeBuilder @class, Type interfaceType, Type implementType)
        {
            foreach (var method in interfaceType.GetMethods(BindingFlags.Public | BindingFlags.Instance))
            {
                var methodParams = method.GetParameters().Select(o => o.ParameterType).ToArray();
                var m = @class.DefineMethod($"{method.Name}",
                    MethodAttributes.Private | MethodAttributes.Final | MethodAttributes.HideBySig |
                    MethodAttributes.Virtual | MethodAttributes.NewSlot,
                    CallingConventions.HasThis,
                    method.ReturnType,
                    methodParams);
                
                @class.DefineMethodOverride(m, interfaceType.GetMethod(method.Name, methodParams));
                

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
                il.Emit(OpCodes.Ldc_I4, methodParams.Length);
                il.Emit(OpCodes.Newarr, typeof(object));
                if (methodParams.Length > 0)
                {
                    for (var i = 0; i < methodParams.Length; i++)
                    {
                        il.Emit(OpCodes.Dup);
                        il.Emit(OpCodes.Ldc_I4, i);
                        il.Emit(OpCodes.Ldarg, i + 1);
                        if (methodParams[i].IsValueType)
                        {
                            il.Emit(OpCodes.Box, methodParams[i].UnderlyingSystemType);
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
                il.Call(implementType.GetMethod(nameof(GetType)));
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
                il.Call(implementType.GetMethod(method.Name, methodParams));
                
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

            var type = @class.CreateType();
            var obj = (ITestClass)Activator.CreateInstance(type);
            obj.Test("A", "B", 3, new List<string>());
            obj.Print();

            //((ITestClass)new ProxyTestClass()).Print();

            //Console.WriteLine($"--- {result ?? "(null)"} ---");
        }
    }
}