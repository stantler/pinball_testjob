using System;
using System.Collections;
using GUI.Base;
using Helpers.Extension;
using UnityEngine;
using UnityEngine.UI;

namespace GUI.Loading
{
    [GUIComponent(100, "GUI/LoadingScreen")]
    public class LoadingScreen : GUIComponent<LoadingScreenInformer>
    {
        private readonly Color _colorA0 = new Color(1f, 1f, 1f, 0f);
        private readonly Color _colorA1 = new Color(1f, 1f, 1f, 1f);

        private const float AnimationTime = 0.25f;

        public bool IsLoading { get; private set; }

        private IEnumerator FadeCoroutine(Image image, Color to, float time, Action callback)
        {
            var from = image.color;
            var t = time;
            while (t > 0)
            {
                image.color = Color.Lerp(from, to, 1 - t / time);

                t -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            image.color = to;
            callback.SafeInvoke();
        }

        public LoadingScreen(GUIManager guiManager, GameObject view) : base(guiManager, view)
        {
            IsLoading = false;
            Informer.Fader.color = _colorA0;
            Informer.Loader.SetActive(false);
            SetActive(false);
        }

        public void StartLoading(Action callback)
        {
            if (IsLoading)
            {
                callback.SafeInvoke();
                return;
            }

            IsLoading = true;
            SetActive(true);

            Informer.Loader.SetActive(false);
            Informer.Fader.color = _colorA0;
            Informer.StartCoroutine(FadeCoroutine(Informer.Fader, _colorA1, AnimationTime, () =>
            {
                Informer.Loader.SetActive(true);
                callback.SafeInvoke();
            }));
        }

        public void FinishLoading(Action callback)
        {
            if (!IsLoading)
            {
                callback.SafeInvoke();
                return;
            }

            IsLoading = false;
            Informer.Loader.SetActive(false);
            Informer.Fader.color = _colorA1;
            Informer.StartCoroutine(FadeCoroutine(Informer.Fader, _colorA0, AnimationTime, () =>
            {
                SetActive(false);
                callback.SafeInvoke();
            }));
        }

        public override void Dispose()
        {

        }
    }
}
