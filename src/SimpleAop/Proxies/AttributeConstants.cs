using System.Reflection;

namespace SimpleAop.Proxies
{
    internal static class AttributeConstants
    {
        internal static class TypeAttribute
        {
            public static TypeAttributes Class => TypeAttributes.Class | TypeAttributes.BeforeFieldInit;
            public static TypeAttributes Interface => TypeAttributes.Interface | TypeAttributes.Abstract;
            public static TypeAttributes Delegate => Class | Selaed;
            public static TypeAttributes Selaed => TypeAttributes.Sealed;
            public static TypeAttributes Public => TypeAttributes.Public;
            public static TypeAttributes Static => TypeAttributes.Abstract | TypeAttributes.Sealed;
            public static TypeAttributes Internal => TypeAttributes.NotPublic;
            public static TypeAttributes Abstract => TypeAttributes.Abstract;
            public static TypeAttributes Struct => TypeAttributes.Sealed | TypeAttributes.SequentialLayout;
        }

        internal static class MethodAttribute
        {
            public static MethodAttributes Public => MethodAttributes.Public;
            public static MethodAttributes Private => MethodAttributes.Private;
            public static MethodAttributes Static => MethodAttributes.Static;
            public static MethodAttributes Protected => MethodAttributes.Family;
            public static MethodAttributes Virtual => MethodAttributes.Virtual;
            public static MethodAttributes Internal => MethodAttributes.Assembly;
            public static MethodAttributes Abstract => MethodAttributes.Abstract;
            public static MethodAttributes Constructor => MethodAttributes.SpecialName | MethodAttributes.RTSpecialName;
        }

        internal static class FieldAttribute
        {
            public static FieldAttributes Public => FieldAttributes.Public;
            public static FieldAttributes Internal => FieldAttributes.Assembly;
            public static FieldAttributes Protected => FieldAttributes.Family;
            public static FieldAttributes Private => FieldAttributes.Private;
            public static FieldAttributes Static => FieldAttributes.Static;
            public static FieldAttributes ReadOnly => FieldAttributes.InitOnly;
            public static FieldAttributes EnumItem => FieldAttributes.Public | FieldAttributes.Static | FieldAttributes.Literal;
            public static FieldAttributes EnumStaticDefaultValue => FieldAttributes.Private | FieldAttributes.SpecialName;
        }
    }
}