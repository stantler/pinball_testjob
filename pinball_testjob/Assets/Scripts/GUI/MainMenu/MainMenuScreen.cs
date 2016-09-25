using System;
using System.Collections.Generic;
using System.Linq;
using GUI.Base;
using Helpers.Extension;
using UnityEngine;
using UnityEngine.UI;

namespace GUI.MainMenu
{
    [GUIComponent(0, "GUI/MainMenuScreen")]
    public class MainMenuScreen : GUIComponent<MainMenuScreenInformer>
    {
        public event Action<int> OnStart;

        public MainMenuScreen(GUIManager guiManager, GameObject view) : base(guiManager, view)
        {
            Informer.StartGameButton.onClick.AddListener(() =>
            {
                OnStart.SafeInvoke(Informer.SettingsDropdown.value);
            });
        }

        public void Initialize(IEnumerable<string> settings)
        {
            Informer.SettingsDropdown.options = settings.Select(s => new Dropdown.OptionData(s)).ToList();
        }

        public override void Dispose()
        {
            Informer.StartGameButton.onClick.RemoveAllListeners();
        }
    }
}