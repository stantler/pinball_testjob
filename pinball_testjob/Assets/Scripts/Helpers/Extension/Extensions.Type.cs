using System;

namespace Helpers.Extension
{
    public static partial class Extensions
    {
        public static bool IsSubclassOfGeneric<TType>(this Type target)
        {
            return IsSubclassOfGeneric(target, typeof(TType));
        }
        public static bool IsSubclassOfGeneric(this Type target, Type baseType)
        {
            while (target != null && target != typeof(object))
            {
                var cur = target.IsGenericType ? target.GetGenericTypeDefinition() : target;
                if (baseType == cur)
                {
                    return true;
                }
                target = target.BaseType;
            }
            return false;
        }
    }
}
