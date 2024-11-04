namespace CodeBase.Infrastructure
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private const string PayloadScene = "Game";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        private void EnterLoadLevel() => 
            _stateMachine.Enter<LoadLevelState, string>(PayloadScene);

        public void Enter() => 
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);

        public void Exit()
        {
        }
    }
}