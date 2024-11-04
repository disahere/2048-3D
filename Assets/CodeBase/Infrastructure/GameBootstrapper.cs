using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;
        public LoadingCurtain curtain;

        private void Awake()
        {
            _game = new Game(this, curtain);
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}