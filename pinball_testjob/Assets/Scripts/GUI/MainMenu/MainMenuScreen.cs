using GUI.Base;
using UnityEngine;

namespace GUI.MainMenu
{
    [GUIComponent(0, "GUI/Global/MainMenuScreen")]
    public class MainMenuScreen : GUIComponent<MainMenuScreenInformer>
    {
        public MainMenuScreen(GUIManager guiManager, GameObject view) : base(guiManager, view)
        {
            //Informer.StartGameButton.onClick.AddListener(_.GameController.StartGame);
            //Informer.LoadGameButton.onClick.AddListener(_.GameController.RestoreSession);

            Informer.LoadingContainer.SetActive(true);
            Informer.StartGameButton.gameObject.SetActive(false);
            Informer.LoadGameButton.gameObject.SetActive(false);

            SetProgress(0f);
        }

        public void SetProgress(float percentage)
        {
            Informer.ProgressBar.value = percentage;

            if (percentage >= 1f)
            {
                Informer.LoadingContainer.SetActive(false);
                Informer.StartGameButton.gameObject.SetActive(true);
            }
        }

        public override void Dispose()
        {
            
        }
    }
}