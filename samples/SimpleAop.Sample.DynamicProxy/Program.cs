using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using SimpleAop.Proxies;

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
                var m = @class.Public.Method(method.ReturnType, method.Name, method.GetParameters().Select(o => o.ParameterType).ToArray());
                if (method.ReturnType != typeof(void))
                {
                    m.Local(method.ReturnType, Guid.NewGuid().ToString("N"));
                }
                
                m.IL.Emit(OpCodes.Ldarg_0);
                m.IL.Emit(OpCodes.Call, method);

                if (method.ReturnType != typeof(void))
                {
                    m.IL.Emit(OpCodes.Stloc_0);
                    m.IL.Emit(OpCodes.Ldloc_0);
                }
                
                m.IL.Emit(OpCodes.Ret);
            }

            var type = @class.ReleaseType();
            var obj = Activator.CreateInstance(type);
            var proxyMethod = obj.GetType().GetMethod("GetString");
            var result = proxyMethod.Invoke(obj, null);
            
            Console.WriteLine($"--- {result ?? "(null)"} ---");
        }
    }
}