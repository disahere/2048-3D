namespace CodeBase.Infrastructure
{
    public interface IState : IExitState
    {
        void Enter();
    }

    public interface IPayloadState<TPayload> : IExitState
    {
        void Enter(TPayload payload);
    }

    public interface IExitState
    {
        void Exit();
    }
}