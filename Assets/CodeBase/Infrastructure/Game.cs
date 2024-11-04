using CodeBase.Logic;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain) => 
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain);
    }
}