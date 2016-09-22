using System;
using UnityEngine;

namespace GUI.Base
{
    public abstract class GUIComponentBase : IDisposable
    {
        protected readonly GUIManager GUIManager;
        protected readonly GameObject View;

        protected GUIComponentBase(GUIManager guiManager, GameObject view)
        {
            View = view;
            GUIManager = guiManager;
        }

        public abstract void Dispose();

        public void SetActive(bool isActive)
        {
            View.SetActive(isActive);
        }
    }
}