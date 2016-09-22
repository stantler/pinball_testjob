using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Helpers.Extension
{
    public static partial class Extensions
    {
        private static IEnumerator CounterCoroutine(Text target, int fromCount, int toCount, string format)
        {
            if (fromCount == toCount)
            {
                yield break;
            }

            const float t = 1.5f;
            var te = 0f;
            while (te <= t)
            {
                var currentCount = Mathf.Lerp(fromCount, toCount, Mathf.Sin((te / t) * Mathf.PI * 0.5f));
                target.text = string.Format(format, (int)currentCount);
                te += Time.unscaledDeltaTime;
                yield return new WaitForEndOfFrame();
            }

            target.text = string.Format(format, toCount);
        }

        public static void AnimateCount(this Text target, int fromCount, int toCount, string format)
        {
            ApplicationManagerComponent.Instance.StartCoroutine(CounterCoroutine(target, fromCount, toCount, format));
        }

        public static void AnimateCount(this Text target, int fromCount, int toCount)
        {
            AnimateCount(target, fromCount, toCount, "{0}");
        }
    }
}