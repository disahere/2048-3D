using CodeBase.Logic;

namespace CodeBase.Infrastructure
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly LoadingCurtain _loadingCurtain;

        public GameLoopState(GameStateMachine stateMachine, LoadingCurtain loadingCurtain)
        {
            _stateMachine = stateMachine;
            _loadingCurtain = loadingCurtain;
        }

        public void Exit()
        {
        }

        public void Enter()
        {
        }
    }
}