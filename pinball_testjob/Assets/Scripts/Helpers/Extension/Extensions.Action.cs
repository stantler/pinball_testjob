using System;

namespace Helpers.Extension
{
    public static partial class Extensions
    {
        public static void SafeInvoke(this Action target)
        {
            if (target == null)
            {
                return;
            }

            target();
        }

        public static void SafeInvoke<T0>(this Action<T0> target, T0 param0)
        {
            if (target == null)
            {
                return;
            }

            target(param0);
        }

        public static void SafeInvoke<T0, T1>(this Action<T0, T1> target, T0 param0, T1 param1)
        {
            if (target == null)
            {
                return;
            }

            target(param0, param1);
        }
    }
}