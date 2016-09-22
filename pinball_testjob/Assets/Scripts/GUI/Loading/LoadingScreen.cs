using System;
using DG.Tweening;
using GUI.Base;
using Helpers.Extension;
using UnityEngine;

namespace GUI.Loading
{
    [GUIComponent(100, "GUI/LoadingScreen")]
    public class LoadingScreen : GUIComponent<LoadingScreenInformer>
    {
        private readonly Color _colorA0 = new Color(1f, 1f, 1f, 0f);
        private readonly Color _colorA1 = new Color(1f, 1f, 1f, 1f);

        private const float Time = 0.25f;

        public bool IsLoading { get; private set; }

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
            Informer.Fader.DOColor(_colorA1, Time).OnComplete(() =>
            {
                Informer.Loader.SetActive(true);
                callback.SafeInvoke();
            });
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
            Informer.Fader.DOColor(_colorA0, Time).OnComplete(() =>
            {
                SetActive(false);
                callback.SafeInvoke();
            });
        }

        public override void Dispose()
        {

        }
    }
}
