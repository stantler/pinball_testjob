using UnityEngine;

namespace GUI.Base
{
    public abstract class GUIComponent<T> : GUIComponentBase
    {
        protected readonly T Informer;

        protected GUIComponent(GUIManager guiManager, GameObject view)
            : base(guiManager, view)
        {
            Informer = View.GetComponent<T>();
        }
    }
}