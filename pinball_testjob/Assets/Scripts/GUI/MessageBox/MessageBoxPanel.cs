using System;
using GUI.Base;
using Helpers.Extension;
using UnityEngine;

namespace GUI.MessageBox
{
    [GUIComponent(10, "GUI/MessageBoxPanel")]
    public class MessageBoxPanel : GUIComponent<MessageBoxInformer>
    {
        private void OnButtonPressed(Action callback)
        {
            SetActive(false);
            Informer.ButtonYes.onClick.RemoveAllListeners();
            Informer.ButtonNo.onClick.RemoveAllListeners();

            callback.SafeInvoke();
        }

        public MessageBoxPanel(GUIManager guiManager, GameObject view) : base(guiManager, view)
        {
            SetActive(false);
        }

        public override void Dispose()
        {

        }

        public void Show(string text, string positiveText = "", Action positiveCallback = null, string negativeText = "", Action negativeCallback = null)
        {
            Informer.Text.text = text;
            Informer.ButtonYesText.text = positiveText;
            Informer.ButtonNoText.text = negativeText;
            
            Informer.ButtonYes.onClick.AddListener(() => OnButtonPressed(positiveCallback));
            Informer.ButtonNo.onClick.AddListener(() => OnButtonPressed(negativeCallback));

            Informer.ButtonYes.gameObject.SetActive(!string.IsNullOrEmpty(positiveText));
            Informer.ButtonNo.gameObject.SetActive(!string.IsNullOrEmpty(negativeText));

            SetActive(true);
        }
    }
}
