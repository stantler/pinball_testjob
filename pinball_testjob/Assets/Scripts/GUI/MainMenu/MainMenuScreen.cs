using System;
using GUI.Base;
using Helpers.Extension;
using UnityEngine;

namespace GUI.MainMenu
{
    [GUIComponent(0, "GUI/MainMenuScreen")]
    public class MainMenuScreen : GUIComponent<MainMenuScreenInformer>
    {
        public event Action OnStart;

        public MainMenuScreen(GUIManager guiManager, GameObject view) : base(guiManager, view)
        {
            Informer.StartGameButton.onClick.AddListener(OnStart.SafeInvoke);
        }

        public override void Dispose()
        {
            Informer.StartGameButton.onClick.RemoveAllListeners();
        }
    }
}