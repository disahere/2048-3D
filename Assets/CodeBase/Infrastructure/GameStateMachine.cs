using System;
using System.Collections.Generic;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameStateMachine
    {
        private IExitState _activeState;
        private readonly Dictionary<Type, IExitState> _state;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
        {
            _state = new Dictionary<Type, IExitState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingCurtain),
                [typeof(GameLoopState)] = new GameLoopState(this, loadingCurtain),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitState
        {
            _activeState?.Exit();
            Debug.Log($"Current active state: {_activeState}");
            
            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitState =>
            _state[typeof(TState)] as TState;
    }
}