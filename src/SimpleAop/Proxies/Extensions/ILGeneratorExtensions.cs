using System;
using System.Reflection;
using System.Reflection.Emit;

namespace SimpleAop.Proxies.Extensions
{
    internal static class ILGeneratorExtensions
    {
        public static LocalBuilder DeclareReturnValue(this ILGenerator il, MethodInfo method)
        {
            if (method.ReturnType != typeof(void))
            {
                return il.DeclareLocal(method.ReturnType);
            }

            return null;
        }

        public static void Return(this ILGenerator il, MethodInfo method, LocalBuilder local)
        {
            if (method.ReturnType != typeof(void))
            {
                il.Emit(OpCodes.Stloc, local);
                il.Emit(OpCodes.Ldloc, local);
            }
                
            il.Emit(OpCodes.Ret);
        }

        public static void LoadParameters(this ILGenerator il, MethodInfo method)
        {
            il.Emit(OpCodes.Ldarg_0);
            for (var i = 1; i <= method.GetParameters().Length; i++)
            {
                il.Emit(OpCodes.Ldarg, i);
            }
        }

        public static void Call(this ILGenerator il, MethodInfo method)
        {
            il.Emit(OpCodes.Call, method);
        }

        public static void Call(this ILGenerator il, ConstructorInfo constructor)
        {
            il.Emit(OpCodes.Call, constructor);
        }

        public static void CallVirt(this ILGenerator il, MethodInfo method)
        {
            il.Emit(OpCodes.Callvirt, method);
        }

        public static void New(this ILGenerator il, ConstructorInfo constructor)
        {
            il.Emit(OpCodes.Newobj, constructor);
        }
    }
}