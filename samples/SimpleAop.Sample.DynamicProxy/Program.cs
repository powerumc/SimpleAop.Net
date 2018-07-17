using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;
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
                var m = @class.DefineMethod($"_{method.DeclaringType.Name}.{method.Name}_",
                    MethodAttributes.Private | MethodAttributes.Final | MethodAttributes.HideBySig |
                    MethodAttributes.Virtual | MethodAttributes.NewSlot,
                    CallingConventions.HasThis,
                    method.ReturnType,
                    methodParams);
                
                @class.DefineMethodOverride(m, interfaceType.GetMethod(method.Name, methodParams));

                var il = m.GetILGenerator();

                var localReturnValue = il.DeclareReturnValue(method);
                var localCurrentMethod = il.DeclareLocal(typeof(MethodBase));
                var localAspectInvocation = il.DeclareLocal(typeof(AspectInvocation));
                var localClassAttributes = il.DeclareLocal(typeof(object[]));
                var localLoop = il.DeclareLocal(typeof(int));
                
                il.Call(typeof(MethodBase).GetMethod(nameof(MethodBase.GetCurrentMethod)));
                il.Emit(OpCodes.Stloc, localCurrentMethod);
                
                // var aspectInvocation = new AspectInvocation(method, this, null);
                il.Emit(OpCodes.Ldloc, localCurrentMethod);
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldnull);
                il.New(typeof(AspectInvocation).GetConstructors()[0]);
                il.Emit(OpCodes.Stloc, localAspectInvocation);
                
                // this.GetType().GetCustomAttributes(typeof(OnMethodBoundAspectAttribute));
                var methodGetType = implementType.GetMethod("GetType");
                il.Emit(OpCodes.Ldarg_0);
                il.Call(methodGetType);
                il.Emit(OpCodes.Ldtoken, typeof(OnMethodBoundAspectAttribute));
                il.Call(typeof(Type).GetMethod("GetTypeFromHandle"));
                il.Emit(OpCodes.Ldc_I4_0);
                il.CallVirt(methodGetType.ReturnType.GetMethod("GetCustomAttributes", new[] {typeof(Type), typeof(bool)}));
                il.Emit(OpCodes.Stloc, localClassAttributes);
                
                
                
                
                
                
                
                
                il.EmitWriteLine("THIS IS RESULT BY IL GENERATOR");
                il.LoadParameters(method);
                il.Call(implementType.GetMethod(method.Name, methodParams));

                il.Return(method, localReturnValue);
            }

            var type = @class.CreateType();
            var obj = (ITestClass)Activator.CreateInstance(type);
            obj.Print();

            //((ITestClass)new ProxyTestClass()).Print();

            //Console.WriteLine($"--- {result ?? "(null)"} ---");
        }
    }
}