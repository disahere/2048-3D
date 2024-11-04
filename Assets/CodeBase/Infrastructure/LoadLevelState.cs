using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class LoadLevelState : IPayloadState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }

        private void OnLoaded() => 
            _stateMachine.Enter<GameLoopState>();

        private static GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        private static GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);   
        }

        public void Exit() => 
            _loadingCurtain.Hide();
    }
}