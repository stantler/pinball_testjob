using System;
using System.Linq;
using Board;
using Board.Behaviours;
using GUI.InGame;
using Helpers.Extension;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game
{
    public class GameInstance : IDisposable
    {
        private readonly GameController _controller;
        private readonly BoardInformer _boardInformer;

        private GameObject _ball;
        private BounceBehaviour[] _bounces;
        private InGameScreen _screen;

        private void OnBallFallOff()
        {
            _controller.EndGame();
        }

        public GameInstance(GameController controller, int settingsId, Action loadingCallback)
        {
            _controller = controller;
            _boardInformer = Object.FindObjectOfType<BoardInformer>();

            _boardInformer.Border.OnBallCollides += OnBallFallOff;

            _screen = _.GUIManager.InitializeComponent<InGameScreen>();
            _screen.SetActive(true);

            var settings = _.DataProvider.GameSettings[settingsId];
            _bounces = new BounceBehaviour[settings.Bounces.Length];
            _.FileLoader.LoadAsset<GameObject>(settings.BallRMId, ball =>
            {
                _ball = (GameObject)Object.Instantiate(ball, _boardInformer.BallPoint);
                _ball.transform.localPosition = Vector3.zero;

                var loadedCount = 0;
                for (var index = 0; index < settings.Bounces.Length; index++)
                {
                    var local = index;
                    var param = settings.Bounces[local];
                    _.FileLoader.LoadAsset<GameObject>(param.RMId,
                        obj =>
                        {
                            var bounce = (GameObject)Object.Instantiate(obj, _boardInformer.BounceParent, false);
                            bounce.transform.position = param.Position;
                            bounce.transform.rotation = Quaternion.Euler(param.Rotation);

                            var behaviour = bounce.AddComponent<BounceBehaviour>();
                            behaviour.Force = param.Force;

                            _bounces[local] = behaviour;
                            ++loadedCount;
                            if (loadedCount >= settings.Bounces.Length)
                            {
                                loadingCallback.SafeInvoke();
                            }
                        });
                }
            });
        }

        public void Update()
        {
            _boardInformer.PaddleLeft.UpdateInput(Input.GetKey(KeyCode.A) || _screen.IsLeftPressed);
            _boardInformer.PaddleRight.UpdateInput(Input.GetKey(KeyCode.D) || _screen.IsRightPressed);
            _boardInformer.Pull.UpdateInput(Input.GetKey(KeyCode.Space) || _screen.IsPullPressed);
        }

        public void Dispose()
        {
            _screen.SetActive(false);
            _.GUIManager.DisposeScreen(_screen);
            _screen = null;

            _boardInformer.Border.OnBallCollides -= OnBallFallOff;
            Object.Destroy(_ball);
            Array.ForEach(_bounces, b => Object.Destroy(b.gameObject));
            _bounces = null;
        }
    }
}
