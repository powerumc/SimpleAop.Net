using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;
using SimpleAop.Proxies;
using SimpleAop.Proxies.Operands;

namespace SimpleAop.Sample.DynamicProxy
{
    class Program
    {
        static void Main(string[] args)
        {
            var assembly = new AssemblyProxyBuilder().Assembly("A");
            var module = assembly.Module("B");
            var @class = module.Class("C", typeof(TestClass), new Type[] {typeof(ITestClass)});

            Print(@class);
        }

        private static void Print(ITypeProxyBuilder @class)
        {
            foreach (var method in typeof(ITestClass).GetMethods(BindingFlags.Public | BindingFlags.Instance))
            {
                var mparams = method.GetParameters().Select(o => o.ParameterType).ToArray();
                var m = @class.TypeBuilder.DefineMethod($"{method.DeclaringType.Name}.{method.Name}",
                    MethodAttributes.Private | MethodAttributes.Final | MethodAttributes.HideBySig |
                    MethodAttributes.Virtual | MethodAttributes.NewSlot,
                    CallingConventions.HasThis,
                    method.ReturnType,
                    mparams);
                
                @class.TypeBuilder.DefineMethodOverride(m, typeof(ITestClass).GetMethod(method.Name, mparams));

                var il = m.GetILGenerator();

                if (method.ReturnType != typeof(void))
                {
                    il.DeclareLocal(method.ReturnType); // var result;
                }
                
                il.EmitWriteLine("THIS IS RESULT BY IL GENERATOR");
                
                il.Emit(OpCodes.Ldarg_0);

                for (var i = 1; i <= method.GetParameters().Length; i++)
                {
                    il.Emit(OpCodes.Ldarg, i);
                }
                
                il.Emit(OpCodes.Call, typeof(TestClass).GetMethod(method.Name, mparams));

                if (method.ReturnType != typeof(void))
                {
                    il.Emit(OpCodes.Stloc_0);
                    il.Emit(OpCodes.Ldloc_0);
                }
                
                il.Emit(OpCodes.Ret);

            }

            var type = @class.ReleaseType();
            var obj = (ITestClass)Activator.CreateInstance(type);
            obj.Print();

            //((ITestClass)new ProxyTestClass()).Print();

            //Console.WriteLine($"--- {result ?? "(null)"} ---");
        }
    }
}